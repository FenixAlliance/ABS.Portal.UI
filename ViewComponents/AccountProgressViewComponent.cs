﻿using System.Threading.Tasks;
using FenixAlliance.ABM.Data;
using FenixAlliance.ABM.Models.Holders;
using FenixAlliance.APS.Core.DataHelpers;
using Microsoft.AspNetCore.Mvc;

namespace FenixAlliance.ABS.Portal.UI.ViewComponents
{
    public class AccountProgressViewComponent : ViewComponent
    {
        private readonly ABMContext _context;
        private AccountUsersHelpers tools;
        public AccountProgressViewComponent(ABMContext context)
        {
            _context = context;
            //Add Method Context 
            tools = new AccountUsersHelpers(context);
        }
#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<IViewComponentResult> InvokeAsync(AllianceIDHolder Tenant)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            return View(Tenant);
        }
    }
}
