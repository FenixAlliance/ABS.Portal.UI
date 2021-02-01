using System.Threading.Tasks;
using FenixAlliance.ABM.Data;
using FenixAlliance.ABM.Models.Holders;
using FenixAlliance.APS.Core.DataHelpers;
using Microsoft.AspNetCore.Mvc;

namespace FenixAlliance.ABS.Portal.UI.ViewComponents
{
    public class AccountHeaderViewComponent : ViewComponent
    {
        private readonly ABMContext _context;
        private AccountUsersHelpers AccountTools;
        public AccountHeaderViewComponent(ABMContext context)
        {
            _context = context;
            //Add Method Context 
            AccountTools = new AccountUsersHelpers(context);
        }
        public async Task<IViewComponentResult> InvokeAsync(AccountHolder Holder, bool DisplaySocialHeader)
        {
            // Get Alliance ID Social Profile from DB
            ViewData["DisplaySocialHeader"] = DisplaySocialHeader;
            return View(await AccountTools.GetTenantSocialProfileAsync(Holder.ID));
        }
    }
}
