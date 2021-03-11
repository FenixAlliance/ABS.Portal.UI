using FenixAlliance.ABM.Data;
using FenixAlliance.ABM.Data.Interfaces.Services;
using FenixAlliance.ABM.Models.Holders;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FenixAlliance.ABS.Portal.UI.ViewComponents
{
    public class ChatWindowViewComponent : ViewComponent
    {
        private readonly ABMContext DataContext;
        private IHolderService AccountUsersHelpers;
        public ChatWindowViewComponent(ABMContext DataContext, IHolderService AccountUsersHelpers)
        {
            this.DataContext = DataContext;
            //Add Method Context 
            this.AccountUsersHelpers = AccountUsersHelpers;
        }
#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<IViewComponentResult> InvokeAsync(AccountHolder Tenant, ClaimsPrincipal user)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            ViewData["NameIdentifier"] = AccountUsersHelpers.GetActiveDirectoryNameIdentifier(user);
            return View(Tenant);
        }
    }
}
