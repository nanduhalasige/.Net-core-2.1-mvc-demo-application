using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoApplication.MVC.Views.Shared.Components.StatusMessages
{
    public class StatusMessagesViewComponent : ViewComponent
    {
        public StatusMessagesViewComponent()
        {

        }
        public IViewComponentResult Invoke() => View("Default");
    }
}
