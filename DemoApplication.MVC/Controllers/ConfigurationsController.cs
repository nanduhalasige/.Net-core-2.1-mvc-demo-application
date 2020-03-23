using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace DemoApplication.MVC.Controllers
{
    public class ConfigurationsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}