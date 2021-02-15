using System.Security.Claims;
using System.Threading.Tasks;
using FenixAlliance.ABM.Data;
using FenixAlliance.APS.Core.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FenixAlliance.ABS.Portal.UI.ViewComponents
{
    public class LoginViewComponent : ViewComponent
    {
        private AccountUsersHelpers tools;
        private readonly ABMContext _context;

        public LoginViewComponent(ABMContext context)
        {
            _context = context;
            //Add Method Context 
            tools = new AccountUsersHelpers(context);
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
