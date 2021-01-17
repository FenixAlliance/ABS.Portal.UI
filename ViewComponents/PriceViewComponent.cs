using System.Linq;
using System.Threading.Tasks;
using FenixAlliance.ABM.Data;
using FenixAlliance.APS.Core.DataHelpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public async Task<IViewComponentResult> InvokeAsync(double Ammount, string Currency, bool PrintUnformatted)
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
            ViewData["Ammount"] = Ammount;

            return View();
        }
    }
}
