﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace FenixAlliance.ABS.Portal.UI.ViewComponents
{
    public class WebCommonsViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            // TODO: Add async query responses to use in every form enabled view
            return View();
        }
    }
}