using FenixAlliance.ABM.Data;
using FenixAlliance.ABM.Data.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FenixAlliance.ABS.Portal.UI.ViewComponents
{
    public class InitPortalLayoutViewComponent : ViewComponent
    {
        private readonly ABMContext _context;
        private IHolderService tools;
        public InitPortalLayoutViewComponent(ABMContext context, IHolderService HolderService)
        {
            _context = context;
            //Add Method Context 
            tools = HolderService;
        }
        public async Task<IViewComponentResult> InvokeAsync(ClaimsPrincipal user)
        {
            string GUID = await tools.GetActiveDirectoryGUIDAsync(user);
            var EndUser = await _context.AccountHolder
                .Include(c => c.SocialProfile)
                    .ThenInclude(c => c.Notifications)
                .Include(c => c.AccountHolderCart)
                    .ThenInclude(c => c.ItemCartRecords)
                        .ThenInclude(c => c.Item)
                            .ThenInclude(c => c.ItemImages)
                .Include(c => c.BusinessProfileRecords)
                    .ThenInclude(c => c.Business)
                .Include(c => c.SelectedBusiness)
                    .ThenInclude(c => c.BusinessSocialProfile)
                        .ThenInclude(c => c.Notifications)
                .Include(c => c.SelectedBusiness)
                    .ThenInclude(c => c.BusinessCart)
                        .ThenInclude(c => c.ItemCartRecords)
                            .ThenInclude(c => c.Item)
                                .ThenInclude(c => c.ItemImages)
                .FirstOrDefaultAsync(m => m.ID == GUID);
            return View(EndUser);
        }
    }
}
