using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace NetPeakTestAssigment.Controllers
{
    public class HomeController : Controller
    {
        //returns view with SPA
        public IActionResult Index()
        {
            return View();
        }
    }
}