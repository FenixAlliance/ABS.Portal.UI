﻿using System.Linq;
using System.Threading.Tasks;
using FenixAlliance.ABM.Data;
using FenixAlliance.ABM.Models.Holders;
using FenixAlliance.APS.Core.DataHelpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FenixAlliance.ABS.Portal.UI.ViewComponents
{
    public class PortalRightSideNavigationViewComponent : ViewComponent
    {
        private readonly ABMContext _context;
        private AccountUsersHelpers tools;
        public PortalRightSideNavigationViewComponent(ABMContext context)
        {
            _context = context;
            //Add Method Context 
            tools = new AccountUsersHelpers(context);
        }
        public async Task<IViewComponentResult> InvokeAsync(AllianceIDHolder Tenant)
        {
            ViewData["TenantsActiveInBusiness"] = await _context.AllianceIDHolder.Include(c => c.SocialProfile).Where(c => c.SelectedBusinessID == Tenant.SelectedBusinessID).ToListAsync();
            return View(Tenant);
        }
    }
}
