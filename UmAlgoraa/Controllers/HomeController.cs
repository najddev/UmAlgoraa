﻿using Microsoft.AspNetCore.Mvc;

namespace UmAlgoraa.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ContactUs()
        {
            return View();
        }
    }
}
