using FenixAlliance.ABM.Data;
using FenixAlliance.ABM.Models.Holders;
using FenixAlliance.APS.Core.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FenixAlliance.ABS.Portal.UI.ViewComponents
{
    public class BusinessNotificationsViewComponent : ViewComponent
    {
        private ABMContext DataContext { get; set; }
        private AccountUsersHelpers AccountUsersHelpers { get; set; }
        public BusinessNotificationsViewComponent(ABMContext DataContext, AccountUsersHelpers AccountUsersHelpers)
        {
            this.DataContext = DataContext;
            //Add Method Context 
            this.AccountUsersHelpers = AccountUsersHelpers;
        }
#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<IViewComponentResult> InvokeAsync(AccountHolder Tenant)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            return View(Tenant.SelectedBusiness);
        }
    }
}
