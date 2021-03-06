using FenixAlliance.ABM.Data;
using FenixAlliance.ABM.Models.Contacts;
using FenixAlliance.APS.Core.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FenixAlliance.ABS.Portal.UI.ViewComponents
{
    public class ContactAccountHeaderViewComponent : ViewComponent
    {
        private readonly ABMContext DataContext;
        private AccountUsersHelpers AccountTools;
        public ContactAccountHeaderViewComponent(ABMContext context)
        {
            DataContext = context;
            //Add Method Context 
            AccountTools = new AccountUsersHelpers(context);
        }
        public async Task<IViewComponentResult> InvokeAsync(Contact Contact)
        {
            // Get Alliance ID Social Profile from DB
            return View(Contact);
        }
    }
}
