using AVONmo.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AVONmo.Controllers
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

        public IActionResult Cremas()
        {
            return View();
        }

        public IActionResult Perfumes()
        {
            return View();
        }

        public IActionResult Electrodomesticos()
        {
            return View();
        }

        public IActionResult Maquillaje()
        {
            return View();
        }

        public IActionResult Tuppers()
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
