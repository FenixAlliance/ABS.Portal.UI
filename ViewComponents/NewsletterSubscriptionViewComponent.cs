using FenixAlliance.ABM.Data;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FenixAlliance.ABS.Portal.UI.ViewComponents
{
    public class NewsletterSubscriptionViewComponent : ViewComponent
    {
        private readonly ABMContext _context;

        public NewsletterSubscriptionViewComponent(ABMContext context)
        {
            //Add Method Context 
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(ClaimsPrincipal user)
        {
            return View();
        }
    }
}




