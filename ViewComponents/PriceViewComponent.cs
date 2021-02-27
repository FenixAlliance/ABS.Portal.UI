using FenixAlliance.ABM.Data;
using FenixAlliance.APS.Core.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace FenixAlliance.ABS.Portal.UI.ViewComponents
{
    public class PriceViewComponent : ViewComponent
    {
        private readonly ABMContext _context;
        private AccountUsersHelpers tools;
        public PriceViewComponent(ABMContext context)
        {
            _context = context;
            //Add Method Context 
            tools = new AccountUsersHelpers(context);
        }
        public async Task<IViewComponentResult> InvokeAsync(double Amount, string Currency, bool PrintUnformatted)
        {
            if (Currency.Length != 3)
            {
                var _currency = await _context.Currency.Include(c => c.Country).Where(c => c.ISOCode == Currency).FirstOrDefaultAsync();
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
