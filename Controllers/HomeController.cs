using DM_cinema.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace DM_cinema.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        [Route("Films/{id}")]
        public IActionResult Films(string id)
        {
            ViewData["id"] = id;
            
            return View();
        }
        [Route("Contact")]
        public IActionResult Contact()
        {
            return View();
        }
        [Route("Cinemas")]
        public IActionResult Cinemas()
        {
            return View();
        }
        [Route("Info Film")]
        public IActionResult InfoFilm()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
