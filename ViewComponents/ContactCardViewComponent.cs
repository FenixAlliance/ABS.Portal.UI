using System.Threading.Tasks;
using FenixAlliance.ABM.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FenixAlliance.ABS.Portal.UI.ViewComponents
{
    public class ContactCardViewComponent : ViewComponent
    {
        private readonly ABMContext _context;

        public ContactCardViewComponent(ABMContext context)
        {
            _context = context;
        }


#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<IViewComponentResult> InvokeAsync()
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            ViewData["CountryID"] = new SelectList(_context.Country, "ISOAlpha3", "Name");
            return View();
        }

    }
}
