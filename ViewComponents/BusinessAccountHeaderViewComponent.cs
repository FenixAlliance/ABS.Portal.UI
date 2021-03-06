using FenixAlliance.ABM.Data;
using FenixAlliance.ABM.Data.Access.Helpers;
using FenixAlliance.ABM.Models.Tenants;
using FenixAlliance.APS.Core.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FenixAlliance.ABS.Portal.UI.ViewComponents
{
    public class BusinessAccountHeaderViewComponent : ViewComponent
    {
        private AccountUsersHelpers AccountUsersHelpers { get; set; }
        private  ABMContext DataContext { get; set; }
        private TenantHelpers TenantHelpers { get; set; }

        public BusinessAccountHeaderViewComponent(ABMContext context, TenantHelpers TenantHelpers, AccountUsersHelpers AccountUsersHelpers)
        {
            //Add Method Context 
            this.DataContext = context;
            this.AccountUsersHelpers = AccountUsersHelpers;
            this.TenantHelpers = TenantHelpers;
        }
        public async Task<IViewComponentResult> InvokeAsync(Business Business, bool DisplaySocialHeader)
        {
            Business = await TenantHelpers.GetBusinessWithSocialProfileAsync(Business.ID);
            ViewData["DisplaySocialHeader"] = DisplaySocialHeader;
            return View(Business);
        }
    }
}
