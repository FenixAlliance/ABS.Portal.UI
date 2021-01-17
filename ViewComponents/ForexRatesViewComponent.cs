using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FenixAlliance.ABM.Data;
using FenixAlliance.ABM.Models.Global.Carts;
using FenixAlliance.ABM.Models.Global.Carts.CartRecords;
using FenixAlliance.ABM.Models.Global.Carts.CartScopes;
using FenixAlliance.ABP.API.REST.Core.Controllers.Global.Currencies.Client;
using FenixAlliance.APS.Core.DataHelpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace FenixAlliance.ABS.Portal.UI.ViewComponents
{
    public class ForexRatesViewComponent : ViewComponent
    {
        private readonly IHostEnvironment hostingEnv;
        private readonly AccountUsersHelpers _accountTools;
        private readonly ABMContext _context;
        private readonly OpenExchangeRatesClient OpenExchangeRatesClient;

        public ForexRatesViewComponent(ABMContext context, IHostEnvironment _hostingEnv, OpenExchangeRatesClient openExchangeRatesClient)
        {
            _context = context;
            hostingEnv = _hostingEnv;
            OpenExchangeRatesClient = openExchangeRatesClient;
            _accountTools = new AccountUsersHelpers(context);
        }

        public async Task<IViewComponentResult> InvokeAsync(ClaimsPrincipal CurrentUser, string CurrentUserIP)
        {

            Cart TempCart = null;
            GuestCart IPBasedCart = null;

            var CurrentTenantGUID = "";
            var Settings = await _context.Settings.FirstOrDefaultAsync(m => m.SettingsPK == "General");

            if (!GuestCartExist(CurrentUserIP))
            {
                IPBasedCart = new GuestCart
                {
                    IP = CurrentUserIP,
                    CurrencyID = "USD.USA"
                };
                _context.Add(IPBasedCart);
                await _context.SaveChangesAsync();
            }
            else
            {
                IPBasedCart = await _context.GuestCart.FirstOrDefaultAsync(c => c.IP == CurrentUserIP);
            }

            if (CurrentUser.Identity.IsAuthenticated)
            {
                var Tenant = await _accountTools.GetCurrentHolder(CurrentUser);

                if (Tenant.SelectedBusiness != null || !string.IsNullOrEmpty(Tenant.SelectedBusinessID))
                {
                    var BusinessCart = await _context.BusinessCart
                        .Include(c => c.Currency).ThenInclude(c => c.Country)
                        .FirstOrDefaultAsync(c => c.BusinessID == Tenant.SelectedBusinessID);

                    TempCart = Tenant.SelectedBusiness?.BusinessCart;
                }
                else
                {
                    // Iterate over every ItemCartRecord and delete from IP cart after copy to Holder cart
                    (await _context.ItemCartRecord.AsNoTracking()
                        .Where(c => c.CartID == IPBasedCart.ID)?.ToListAsync())
                        .ForEach(ItemCartRecord =>
                        {
                            var newItemCartRecord = new ItemCartRecord
                            {
                                CartID = Tenant.AllianceIDHolderCart.ID,
                                Quantity = ItemCartRecord.Quantity,
                                ItemID = ItemCartRecord.ItemID
                            };

                            _context.Add(newItemCartRecord);
                            _context.ItemCartRecord.Remove(ItemCartRecord);
                        });

                    await _context.SaveChangesAsync();
                    TempCart = Tenant.AllianceIDHolderCart;
                }
            }
            else
            {
                TempCart = IPBasedCart;
            }

            if (hostingEnv.IsProduction() || Settings.OpenCurrencyExchangeRates == null)
            {
                if (DateTime.Compare(DateTime.Now, Settings.ExchangeRatesUpdatedTimestamp.AddHours(1)) > 0)
                {
                    Settings.OpenCurrencyExchangeRates = await OpenExchangeRatesClient.GetCurrencyRatesAsync();
                    Settings.ExchangeRatesUpdatedTimestamp = DateTime.Now;
                    try
                    {
                        _context.Update(Settings);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        throw;
                    }
                }
            }


            ViewData["CartID"] = TempCart.ID;
            ViewData["CurrentUserIP"] = CurrentUserIP;
            ViewData["CurrentHolderGUID"] = CurrentTenantGUID;
            ViewData["ExchangeRates"] = Settings.OpenCurrencyExchangeRates;
            ViewData["Currency"] = await _context.Currency.AsNoTracking().FirstOrDefaultAsync(c => c.ID == TempCart.CurrencyID);

            return View(TempCart);
        }

        private bool GuestCartExist(string IP)
        {
            return _context.GuestCart.Any(c => c.IP == IP);
        }
    }
}
