using System.Threading.Tasks;
using FenixAlliance.ABM.Data;
using FenixAlliance.ABM.Models.Contacts;
using FenixAlliance.APS.Core.DataHelpers;
using Microsoft.AspNetCore.Mvc;

namespace FenixAlliance.ABS.Portal.UI.ViewComponents
{
    public class ContactAccountHeaderViewComponent : ViewComponent
    {
        private readonly ABMContext _context;
        private AccountUsersHelpers AccountTools;
        public ContactAccountHeaderViewComponent(ABMContext context)
        {
            _context = context;
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
