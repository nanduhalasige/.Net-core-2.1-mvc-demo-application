﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace DemoApplication.MVC.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return PartialView("_Login");
        }
    }
}