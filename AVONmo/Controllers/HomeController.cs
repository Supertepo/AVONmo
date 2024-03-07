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
            List<Perfume> listaPerfumes = new List<Perfume>();
            List<Precio> listaPrecio = new List<Precio>();
            List<Categorium> listaCategorias = new List<Categorium>();
            using (var context = new AvonContext())
            {
                listaPerfumes = context.Perfumes.ToList();
            }
            using (var context = new AvonContext())
            {
                listaPrecio = listaPerfumes.SelectMany(perfume => context.Precios.Where(p => p.IdPrecio == perfume.IdPrecio)).ToList();
            }
            using (var context = new AvonContext())
            {
                listaCategorias = listaPerfumes.SelectMany(perfume => context.Categoria.Where(p => p.IdCategoria == perfume.IdCategoria)).ToList();
            }
            //ViewBag.Categoria = Categoria;
            return View(new Tuple<List<Perfume>, List<Precio>, List<Categorium>>(listaPerfumes, listaPrecio, listaCategorias));
        }

        public IActionResult Electrodomesticos()
        {
            List<Electrodomestico> listaElectrodomesticos = new List<Electrodomestico>();
            List<Precio> listaPrecio = new List<Precio>();
            List<Categorium> listaCategorias = new List<Categorium>();
            using (var context = new AvonContext())
            {
                listaElectrodomesticos = context.Electrodomesticos.ToList();
            }
            using (var context = new AvonContext())
            {
                listaPrecio = listaElectrodomesticos.SelectMany(electrodomestico => context.Precios.Where(p => p.IdPrecio == electrodomestico.IdPrecio)).ToList();
            }
            using (var context = new AvonContext())
            {
                listaCategorias = listaElectrodomesticos.SelectMany(electrodomestico => context.Categoria.Where(p => p.IdCategoria == electrodomestico.IdCategoria)).ToList();
            }
            //ViewBag.Categoria = Categoria;
            return View(new Tuple<List<Electrodomestico>, List<Precio>, List<Categorium>>(listaElectrodomesticos, listaPrecio, listaCategorias));
        }

        public IActionResult Maquillaje()
        {
            List<Tupper> listaMaquillaje = new List<Tupper>();
            List<Precio> listaPrecio = new List<Precio>();
            List<Categorium> listaCategorias = new List<Categorium>();
            using (var context = new AvonContext())
            {
                listaMaquillaje = context.Maquillajes.ToList();
            }
            using (var context = new AvonContext())
            {
                listaPrecio = listaMaquillaje.SelectMany(maquillaje => context.Precios.Where(p => p.IdPrecio == maquillaje.IdPrecio)).ToList();
            }
            using (var context = new AvonContext())
            {
                listaCategorias = listaMaquillaje.SelectMany(crema => context.Categoria.Where(p => p.IdCategoria == crema.IdCategoria)).ToList();
            }
            //ViewBag.Categoria = Categoria;
            return View(new Tuple<List<Tupper>, List<Precio>, List<Categorium>>(listaMaquillaje, listaPrecio, listaCategorias));
        }

        public IActionResult Tuppers()
        {
            List<Tupper> listaTuppers = new List<Tupper>();
            List<Precio> listaPrecio = new List<Precio>();
            List<Categorium> listaCategorias = new List<Categorium>();
            using (var context = new AvonContext())
            {
                listaTuppers = context.Tuppers.ToList();
            }
            using (var context = new AvonContext())
            {
                listaPrecio = listaTuppers.SelectMany(tupper => context.Precios.Where(p => p.IdPrecio == tupper.IdPrecio)).ToList();
            }
            using (var context = new AvonContext())
            {
                listaCategorias = listaTuppers.SelectMany(tupper => context.Categoria.Where(p => p.IdCategoria == tupper.IdCategoria)).ToList();
            }
            //ViewBag.Categoria = Categoria;
            return View(new Tuple<List<Tupper>, List<Precio>, List<Categorium>>(listaTuppers, listaPrecio, listaCategorias));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
