using System.Threading.Tasks;
using FenixAlliance.ABM.Data;
using FenixAlliance.ABM.Data.Access.Helpers;
using FenixAlliance.ABM.Models.Tenants;
using FenixAlliance.APS.Core.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace FenixAlliance.ABS.Portal.UI.ViewComponents
{
    public class BusinessAccountHeaderViewComponent : ViewComponent
    {
        private AccountUsersHelpers tools;
        private readonly ABMContext _context;
        private TenantHelpers BusinessTools;

        public BusinessAccountHeaderViewComponent(ABMContext context)
        {
            _context = context;
            //Add Method Context 
            tools = new AccountUsersHelpers(context);
            BusinessTools = new TenantHelpers(context);
        }
        public async Task<IViewComponentResult> InvokeAsync(Business Business, bool DisplaySocialHeader)
        {
            Business = await BusinessTools.GetBusinessWithSocialProfileAsync(Business.ID);
            ViewData["DisplaySocialHeader"] = DisplaySocialHeader;
            return View(Business);
        }
    }
}
