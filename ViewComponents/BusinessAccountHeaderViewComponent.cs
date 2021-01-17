using System.Threading.Tasks;
using FenixAlliance.ABM.Data;
using FenixAlliance.ABM.Models.Tenants;
using FenixAlliance.APS.Core.DataHelpers;
using Microsoft.AspNetCore.Mvc;

namespace FenixAlliance.ABS.Portal.UI.ViewComponents
{
    public class BusinessAccountHeaderViewComponent : ViewComponent
    {
        private AccountUsersHelpers tools;
        private readonly ABMContext _context;
        private BusinessHelpers BusinessTools;

        public BusinessAccountHeaderViewComponent(ABMContext context)
        {
            _context = context;
            //Add Method Context 
            tools = new AccountUsersHelpers(context);
            BusinessTools = new BusinessHelpers(context);
        }
        public async Task<IViewComponentResult> InvokeAsync(Business Business, bool DisplaySocialHeader)
        {
            Business = await BusinessTools.GetBusinessWithSocialProfileAsync(Business.ID);
            ViewData["DisplaySocialHeader"] = DisplaySocialHeader;
            return View(Business);
        }
    }
}
