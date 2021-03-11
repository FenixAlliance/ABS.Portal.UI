using FenixAlliance.ABM.Data;
using FenixAlliance.ABM.Data.Access.Services.Auth;
using FenixAlliance.ABM.Data.Interfaces.Services;
using FenixAlliance.ABM.Models.Holders;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FenixAlliance.ABS.Portal.UI.ViewComponents
{
    public class AccountHeaderViewComponent : ViewComponent
    {
        private readonly ABMContext _context;
        private IHolderService AccountTools;
        public AccountHeaderViewComponent(ABMContext context, IHolderService HolderService)
        {
            _context = context;
            //Add Method Context 
            AccountTools = HolderService;
        }

        public async Task<IViewComponentResult> InvokeAsync(AccountHolder Holder, bool DisplaySocialHeader)
        {
            // Get Alliance ID Social Profile from DB
            ViewData["DisplaySocialHeader"] = DisplaySocialHeader;
            return View(await AccountTools.GetTenantSocialProfileAsync(Holder.ID));
        }
    }
}
