using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SakhaSite.Controllers
{
    public class HomeController : Controller
    {
        private AppSettings settings;

        public HomeController(AppSettings settings)
        {
            this.settings = settings;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["SiteName"] = settings.SiteName;
            return View();
        }
    }
}