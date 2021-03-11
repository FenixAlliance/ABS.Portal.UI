using FenixAlliance.ABM.Data;
using FenixAlliance.ABM.Data.Interfaces.Services;
using FenixAlliance.ABM.Models.Holders;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace FenixAlliance.ABS.Portal.UI.ViewComponents
{
    public class PortalRightSideNavigationViewComponent : ViewComponent
    {
        private readonly ABMContext DataContext;
        private IHolderService HolderService;
        public PortalRightSideNavigationViewComponent(ABMContext DataContext, IHolderService HolderService)
        {
            this.DataContext = DataContext;
            //Add Method Context 
            this.HolderService = HolderService;
        }
        public async Task<IViewComponentResult> InvokeAsync(AccountHolder Tenant)
        {
            ViewData["TenantsActiveInBusiness"] = await DataContext.AccountHolder.Include(c => c.SocialProfile).Where(c => c.SelectedBusinessID == Tenant.SelectedBusinessID).ToListAsync();
            return View(Tenant);
        }
    }
}
