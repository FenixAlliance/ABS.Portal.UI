using FenixAlliance.ABM.Data;
using FenixAlliance.ABM.Data.Clients.OpenCurrencyExchange;
using FenixAlliance.ABM.Models.Global.Carts;
using FenixAlliance.ABM.Models.Global.Carts.CartRecords;
using FenixAlliance.ABM.Models.Global.Carts.CartScopes;
using FenixAlliance.APS.Core.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FenixAlliance.ABS.Portal.UI.ViewComponents
{
    public class ForexRatesViewComponent : ViewComponent
    {
        private  ABMContext DataContext { get; set; }
        private  IHostEnvironment Environment { get; set; }
        private  AccountUsersHelpers AccountUsersHelpers { get; set; }
        private  OpenExchangeRatesClient OpenExchangeRatesClient { get; set; }

        public ForexRatesViewComponent(ABMContext context, IHostEnvironment _hostingEnv, OpenExchangeRatesClient openExchangeRatesClient, AccountUsersHelpers AccountUsersHelpers)
        {
            this.DataContext = context;
            this.Environment = _hostingEnv;
            this.OpenExchangeRatesClient = openExchangeRatesClient;
            this.AccountUsersHelpers = AccountUsersHelpers;
        }

        public async Task<IViewComponentResult> InvokeAsync(ClaimsPrincipal CurrentUser, string CurrentUserIP)
        {

            Cart TempCart = null;
            GuestCart IPBasedCart = null;

            var CurrentTenantGUID = "";
            var Settings = await DataContext.Settings.FirstOrDefaultAsync(m => m.ID == "General");

            if (!GuestCartExist(CurrentUserIP))
            {
                IPBasedCart = new GuestCart
                {
                    IP = CurrentUserIP,
                    CurrencyID = "USD.USA"
                };
                DataContext.Add(IPBasedCart);
                await DataContext.SaveChangesAsync();
            }
            else
            {
                IPBasedCart = await DataContext.GuestCart.FirstOrDefaultAsync(c => c.IP == CurrentUserIP);
            }

            if (CurrentUser.Identity.IsAuthenticated)
            {
                var Tenant = await AccountUsersHelpers.GetCurrentHolder(CurrentUser);

                if (Tenant.SelectedBusiness != null || !string.IsNullOrEmpty(Tenant.SelectedBusinessID))
                {
                    var BusinessCart = await DataContext.BusinessCart
                        .Include(c => c.Currency).ThenInclude(c => c.Country)
                        .FirstOrDefaultAsync(c => c.BusinessID == Tenant.SelectedBusinessID);

                    TempCart = Tenant.SelectedBusiness?.BusinessCart;
                }
                else
                {
                    // Iterate over every ItemCartRecord and delete from IP cart after copy to Holder cart
                    (await DataContext.ItemCartRecord.AsNoTracking()
                        .Where(c => c.CartID == IPBasedCart.ID)?.ToListAsync())
                        .ForEach(ItemCartRecord =>
                        {
                            var newItemCartRecord = new FenixAlliance.ABM.Models.Global.Carts.CartRecords.ItemRecords.ItemCartRecord()
                            {
                                CartID = Tenant.AccountHolderCart.ID,
                                Quantity = ItemCartRecord.Quantity,
                                ItemID = ItemCartRecord.ItemID
                            };

                            DataContext.Add(newItemCartRecord);
                            DataContext.ItemCartRecord.Remove(ItemCartRecord);
                        });

                    await DataContext.SaveChangesAsync();
                    TempCart = Tenant.AccountHolderCart;
                }
            }
            else
            {
                TempCart = IPBasedCart;
            }

            if (Environment.IsProduction() || Settings.OpenCurrencyExchangeRates == null)
            {
                if (DateTime.Compare(DateTime.Now, Settings.ExchangeRatesUpdatedTimestamp.AddHours(1)) > 0)
                {
                    Settings.OpenCurrencyExchangeRates = await OpenExchangeRatesClient.GetCurrencyRatesAsync();
                    Settings.ExchangeRatesUpdatedTimestamp = DateTime.Now;
                    try
                    {
                        DataContext.Update(Settings);
                        await DataContext.SaveChangesAsync();
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
            ViewData["Currency"] = await DataContext.Currency.AsNoTracking().FirstOrDefaultAsync(c => c.ID == TempCart.CurrencyID);

            return View(TempCart);
        }

        private bool GuestCartExist(string IP)
        {
            return DataContext.GuestCart.Any(c => c.IP == IP);
        }
    }
}
