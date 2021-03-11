using FenixAlliance.ABM.Data;
using FenixAlliance.ABM.Data.Interfaces.Services;
using FenixAlliance.ABM.Models.Contacts;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FenixAlliance.ABS.Portal.UI.ViewComponents
{
    public class ContactAccountHeaderViewComponent : ViewComponent
    {
        private readonly ABMContext DataContext;
        private IHolderService AccountTools;
        public ContactAccountHeaderViewComponent(ABMContext context, IHolderService HolderService)
        {
            DataContext = context;
            //Add Method Context 
            AccountTools = HolderService;
        }
        public async Task<IViewComponentResult> InvokeAsync(Contact Contact)
        {
            // Get Alliance ID Social Profile from DB
            return View(Contact);
        }
    }
}
