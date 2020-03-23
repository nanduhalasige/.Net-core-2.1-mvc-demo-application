using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoApplication.MVC.Views.Shared.Components.NavBar
{
    public class NavBarViewComponent : ViewComponent
    {
        public NavBarViewComponent()
        {

        }
        public IViewComponentResult Invoke() => View("Default");

    }
}
