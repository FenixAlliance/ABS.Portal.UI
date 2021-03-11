using FenixAlliance.ABM.Data;
using FenixAlliance.ABM.Data.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FenixAlliance.ABS.Portal.UI.ViewComponents
{
    public class LoginViewComponent : ViewComponent
    {
        private IHolderService tools;
        private readonly ABMContext _context;

        public LoginViewComponent(ABMContext context, IHolderService HolderService)
        {
            _context = context;
            //Add Method Context 
            tools = HolderService;
        }

        public async Task<IViewComponentResult> InvokeAsync(ClaimsPrincipal User)
        {

            if (User.Identity.IsAuthenticated)
            {
                var GUID = tools.GetActiveDirectoryGUID(User);
                var Tenant = await _context.AccountHolder.FirstOrDefaultAsync(c => c.ID == GUID);
                ViewData["Tenant"] = Tenant;
            }

            return View();
        }
    }
}
