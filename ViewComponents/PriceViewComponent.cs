using FenixAlliance.ABM.Data;
using FenixAlliance.ABM.Data.Access.Helpers;
using FenixAlliance.APS.Core.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace FenixAlliance.ABS.Portal.UI.ViewComponents
{
    public class PriceViewComponent : ViewComponent
    {
        private AccountUsersHelpers AccountUsersHelpers { get; set; }
        private ABMContext DataContext { get; set; }
        private TenantHelpers TenantHelpers { get; set; }

        public PriceViewComponent(ABMContext DataContext, TenantHelpers TenantHelpers, AccountUsersHelpers AccountUsersHelpers)
        {
            //Add Method Context 
            this.DataContext = DataContext;
            this.AccountUsersHelpers = AccountUsersHelpers;
            this.TenantHelpers = TenantHelpers;
        }

        public async Task<IViewComponentResult> InvokeAsync(double Amount, string Currency, bool PrintUnformatted)
        {
            if (Currency.Length != 3)
            {
                var _currency = await DataContext.Currency.Include(c => c.Country).Where(c => c.ISOCode == Currency).FirstOrDefaultAsync();
                ViewData["CurrencyCode"] = _currency.ISOCode;
            }
            else
            {
                ViewData["CurrencyCode"] = Currency;
            }

            ViewData["PrintUnformatted"] = PrintUnformatted;
            ViewData["Amount"] = Amount;

            return View();
        }
    }
}
