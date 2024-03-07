using AVONmo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            List<Categorium> listaCategorias = new List<Categorium>();
            using (var context = new AvonContext()) 
            {
                listaCategorias = context.Categoria.ToList();
            }
            return View(listaCategorias);
        }

        public IActionResult Cremas()
        {
            List<Crema> listaCremas = new List<Crema>();
            List<Precio> listaPrecio = new List<Precio>();
            List<Categorium> listaCategorias = new List<Categorium>();
            using (var context = new AvonContext())
            {
                listaCremas = context.Cremas.ToList();
            }
            using (var context = new AvonContext())
            {
                listaPrecio = listaCremas.SelectMany(crema => context.Precios.Where(p => p.IdPrecio == crema.IdPrecio)).ToList();
            }
            using (var context = new AvonContext())
            {
              listaCategorias = listaCremas.SelectMany(crema => context.Categoria.Where(p => p.IdCategoria == crema.IdCategoria)).ToList();
            }
            //ViewBag.Categoria = Categoria;
            return View(new Tuple<List<Crema>,List<Precio>,List<Categorium>>(listaCremas,listaPrecio,listaCategorias));
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
