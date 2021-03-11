using FenixAlliance.ABM.Data;
using FenixAlliance.ABM.Data.Access.Helpers;
using FenixAlliance.ABM.Data.Interfaces.Services;
using FenixAlliance.ABM.Models.Tenants;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FenixAlliance.ABS.Portal.UI.ViewComponents
{
    public class BusinessAccountHeaderViewComponent : ViewComponent
    {
        private  ABMContext DataContext { get; set; }
        private ITenantService TenantHelpers { get; set; }
        private IHolderService AccountUsersHelpers { get; set; }

        public BusinessAccountHeaderViewComponent(ABMContext context, ITenantService TenantHelpers, IHolderService AccountUsersHelpers)
        {
            //Add Method Context 
            this.DataContext = context;
            this.TenantHelpers = TenantHelpers;
            this.AccountUsersHelpers = AccountUsersHelpers;
        }

        public async Task<IViewComponentResult> InvokeAsync(Business Business, bool DisplaySocialHeader)
        {
            Business = await TenantHelpers.GetBusinessWithSocialProfileAsync(Business.ID);
            ViewData["DisplaySocialHeader"] = DisplaySocialHeader;
            return View(Business);
        }
    }
}
