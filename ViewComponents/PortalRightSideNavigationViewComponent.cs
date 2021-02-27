using FenixAlliance.ABM.Data;
using FenixAlliance.ABM.Models.Holders;
using FenixAlliance.APS.Core.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace FenixAlliance.ABS.Portal.UI.ViewComponents
{
    public class PortalRightSideNavigationViewComponent : ViewComponent
    {
        private readonly ABMContext _context;
        private AccountUsersHelpers tools;
        public PortalRightSideNavigationViewComponent(ABMContext context)
        {
            _context = context;
            //Add Method Context 
            tools = new AccountUsersHelpers(context);
        }
        public async Task<IViewComponentResult> InvokeAsync(AccountHolder Tenant)
        {
            ViewData["TenantsActiveInBusiness"] = await _context.AccountHolder.Include(c => c.SocialProfile).Where(c => c.SelectedBusinessID == Tenant.SelectedBusinessID).ToListAsync();
            return View(Tenant);
        }
    }
}
