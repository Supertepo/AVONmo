using AVONmo.Models;
using AVONmo.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;

namespace AVONmo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AvonContext _context;

        public HomeController(ILogger<HomeController> logger, AvonContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Categoria.ToListAsync());
        }
        //listas
        public async Task<IActionResult> Cremas()
        {
            List<Crema> listaCremas;
            List<Precio> listaPrecio;
            List<Categorium> listaCategorias;
            using (var context = _context)
            {
                listaCremas = await context.Cremas.ToListAsync();
                listaPrecio = await context.Cremas.SelectMany(crema => context.Precios.Where(p => p.IdPrecio == crema.IdPrecio)).ToListAsync();
                listaCategorias = await context.Cremas.SelectMany(crema => context.Categoria.Where(p => p.IdCategoria == crema.IdCategoria)).ToListAsync();
            }
            //ViewBag.Categoria = Categoria;
            return View(new Tuple<List<Crema>, List<Precio>, List<Categorium>>(listaCremas, listaPrecio, listaCategorias));
        }

        public async Task<IActionResult> Perfumes()
        {
            List<Perfume> listaPerfumes = new List<Perfume>();
            List<Precio> listaPrecio = new List<Precio>();
            List<Categorium> listaCategorias = new List<Categorium>();
            using (var context = _context)
            {
                listaPerfumes = await context.Perfumes.ToListAsync();
                listaPrecio = await context.Perfumes.SelectMany(perfume => context.Precios.Where(p => p.IdPrecio == perfume.IdPrecio)).ToListAsync();
                listaCategorias = await context.Perfumes.SelectMany(perfume => context.Categoria.Where(p => p.IdCategoria == perfume.IdCategoria)).ToListAsync();
            }
            //ViewBag.Categoria = Categoria;
            return View(new Tuple<List<Perfume>, List<Precio>, List<Categorium>>(listaPerfumes, listaPrecio, listaCategorias));
        }

        public async Task<IActionResult> Electrodomesticos()
        {
            List<Electrodomestico> listaElectrodomesticos = new List<Electrodomestico>();
            List<Precio> listaPrecio = new List<Precio>();
            List<Categorium> listaCategorias = new List<Categorium>();
            using (var context = _context)
            {
                listaElectrodomesticos = await context.Electrodomesticos.ToListAsync();
                listaPrecio = await context.Electrodomesticos.SelectMany(electrodomestico => context.Precios.Where(p => p.IdPrecio == electrodomestico.IdPrecio)).ToListAsync();
                listaCategorias = await context.Electrodomesticos.SelectMany(electrodomestico => context.Categoria.Where(p => p.IdCategoria == electrodomestico.IdCategoria)).ToListAsync();
            }
            //ViewBag.Categoria = Categoria;
            return View(new Tuple<List<Electrodomestico>, List<Precio>, List<Categorium>>(listaElectrodomesticos, listaPrecio, listaCategorias));
        }

        public async Task<IActionResult> Maquillaje()
        {
            List<Maquillaje> listaMaquillaje = new List<Maquillaje>();
            List<Precio> listaPrecio = new List<Precio>();
            List<Categorium> listaCategorias = new List<Categorium>();
            using (var context = _context)
            {
                listaMaquillaje = await context.Maquillajes.ToListAsync();
                listaPrecio = await context.Maquillajes.SelectMany(maquillaje => context.Precios.Where(p => p.IdPrecio == maquillaje.IdPrecio)).ToListAsync();
                listaCategorias = await context.Maquillajes.SelectMany(crema => context.Categoria.Where(p => p.IdCategoria == crema.IdCategoria)).ToListAsync();
            }
            //ViewBag.Categoria = Categoria;
            return View(new Tuple<List<Maquillaje>, List<Precio>, List<Categorium>>(listaMaquillaje, listaPrecio, listaCategorias));
        }

        public async Task<IActionResult> Tuppers()
        {
            List<Tupper> listaTuppers = new List<Tupper>();
            List<Precio> listaPrecio = new List<Precio>();
            List<Categorium> listaCategorias = new List<Categorium>();
            using (var context = _context)
            {
                listaTuppers = await context.Tuppers.ToListAsync();
                listaPrecio = await context.Tuppers.SelectMany(tupper => context.Precios.Where(p => p.IdPrecio == tupper.IdPrecio)).ToListAsync();
                listaCategorias = await context.Tuppers.SelectMany(tupper => context.Categoria.Where(p => p.IdCategoria == tupper.IdCategoria)).ToListAsync();
            }
            //ViewBag.Categoria = Categoria;
            return View(new Tuple<List<Tupper>, List<Precio>, List<Categorium>>(listaTuppers, listaPrecio, listaCategorias));
        }


        //Creacion de productos
        public async Task<IActionResult> CrearCrema()
        {
            return View(await _context.Cremas.OrderBy(e => e.IdProducto).LastAsync());
        }
        [HttpPost]
        public async Task<IActionResult> CrearCrema(Crema crema, float Precio)
        {
            var CremaExistente = await _context.Cremas.FirstOrDefaultAsync(c => c.IdProducto == crema.IdProducto);
            if (CremaExistente != null)
            {
                ViewBag.Mensaje = "No cambiaste el ID de producto por el siguiente";
                return View(crema);
            }
            else
            {
                // Verificar si el precio ya existe
                var precioExistente = await _context.Precios.FirstOrDefaultAsync(p => p.Cantidad == Precio);
                crema.IdCategoria = "C-000";
                if (precioExistente == null)
                {
                    // Si no existe, crear y agregar un nuevo precio
                    Precio precio = new Precio { Cantidad = Precio };
                    _context.Precios.Add(precio); // Cambiado de AddAsync a Add
                    await _context.SaveChangesAsync();

                    // Asignar el ID del nuevo precio al modelo crema
                    crema.IdPrecio = precio.IdPrecio;
                }
                else
                {
                    // Si el precio ya existe, usar el ID existente
                    crema.IdPrecio = precioExistente.IdPrecio;

                }


                // Agregar la crema al contexto y guardar los cambios
                _context.Cremas.Add(crema);
                var result = await _context.SaveChangesAsync();

                // Verificar que se haya guardado correctamente
                if (result > 0)
                {
                    // Redirigir a la vista 'Cremas' si la operación fue exitosa
                    return RedirectToAction("Cremas");
                }
                else
                {
                    return View(crema);
                }


            }


        }

        public async Task<IActionResult> CrearPerfume()
        {
            return View(await _context.Perfumes.OrderBy(e => e.IdProducto).LastAsync());
        }
        [HttpPost]
        public async Task<IActionResult> CrearPerfume(Perfume perfume, float Precio)
        {
            var PerfumeExistente = await _context.Perfumes.FirstOrDefaultAsync(c => c.IdProducto == perfume.IdProducto);
            if (perfume != null)
            {
                ViewBag.Mensaje = "No cambiaste el ID de producto por el siguiente";
                return View(perfume);
            }
            else
            {
                // Verificar si el precio ya existe
                var precioExistente = await _context.Precios.FirstOrDefaultAsync(p => p.Cantidad == Precio);
                perfume.IdCategoria = "P-000";
                if (precioExistente == null)
                {
                    // Si no existe, crear y agregar un nuevo precio
                    Precio precio = new Precio { Cantidad = Precio };
                    _context.Precios.Add(precio); // Cambiado de AddAsync a Add
                    await _context.SaveChangesAsync();

                    // Asignar el ID del nuevo precio al modelo crema
                    perfume.IdPrecio = precio.IdPrecio;
                }
                else
                {
                    // Si el precio ya existe, usar el ID existente
                    perfume.IdPrecio = precioExistente.IdPrecio;

                }


                // Agregar la crema al contexto y guardar los cambios
                _context.Perfumes.Add(perfume);
                var result = await _context.SaveChangesAsync();

                // Verificar que se haya guardado correctamente
                if (result > 0)
                {
                    // Redirigir a la vista 'Cremas' si la operación fue exitosa
                    return RedirectToAction("Perfume");
                }
                else
                {
                    return View(perfume);
                }


            }
        }

        public async Task<IActionResult> CrearMaquillaje()
        {
            return View(await _context.Maquillajes.OrderBy(e => e.IdProducto).LastAsync());
        }
        [HttpPost]
        public async Task<IActionResult> CrearMaquillaje(Maquillaje maquillaje, float Precio)
        {
            var MaquillajeExistente = await _context.Maquillajes.FirstOrDefaultAsync(c => c.IdProducto == maquillaje.IdProducto);
            if (maquillaje != null)
            {
                ViewBag.Mensaje = "No cambiaste el ID de producto por el siguiente";
                return View(maquillaje);
            }
            else
            {
                // Verificar si el precio ya existe
                var precioExistente = await _context.Precios.FirstOrDefaultAsync(p => p.Cantidad == Precio);
                maquillaje.IdCategoria = "M-000";
                if (precioExistente == null)
                {
                    // Si no existe, crear y agregar un nuevo precio
                    Precio precio = new Precio { Cantidad = Precio };
                    _context.Precios.Add(precio); // Cambiado de AddAsync a Add
                    await _context.SaveChangesAsync();

                    // Asignar el ID del nuevo precio al modelo crema
                    maquillaje.IdPrecio = precio.IdPrecio;
                }
                else
                {
                    // Si el precio ya existe, usar el ID existente
                    maquillaje.IdPrecio = precioExistente.IdPrecio;

                }


                // Agregar la crema al contexto y guardar los cambios
                _context.Maquillajes.Add(maquillaje);
                var result = await _context.SaveChangesAsync();

                // Verificar que se haya guardado correctamente
                if (result > 0)
                {
                    // Redirigir a la vista 'Cremas' si la operación fue exitosa
                    return RedirectToAction("Maquillaje");
                }
                else
                {
                    return View(maquillaje);
                }


            }
        }


        public async Task<IActionResult> CrearTupper()
        {
            return View(await _context.Tuppers.OrderBy(e => e.IdProducto).LastAsync());
        }
        [HttpPost]
        public async Task<IActionResult> CrearTupper(Tupper tupper, float Precio)
        {
            var TupperExistente = await _context.Perfumes.FirstOrDefaultAsync(c => c.IdProducto == tupper.IdProducto);
            if (tupper != null)
            {
                ViewBag.Mensaje = "No cambiaste el ID de producto por el siguiente";
                return View(tupper);
            }
            else
            {
                // Verificar si el precio ya existe
                var precioExistente = await _context.Precios.FirstOrDefaultAsync(p => p.Cantidad == Precio);
                tupper.IdCategoria = "T-000";
                if (precioExistente == null)
                {
                    // Si no existe, crear y agregar un nuevo precio
                    Precio precio = new Precio { Cantidad = Precio };
                    _context.Precios.Add(precio); // Cambiado de AddAsync a Add
                    await _context.SaveChangesAsync();

                    // Asignar el ID del nuevo precio al modelo crema
                    tupper.IdPrecio = precio.IdPrecio;
                }
                else
                {
                    // Si el precio ya existe, usar el ID existente
                    tupper.IdPrecio = precioExistente.IdPrecio;

                }


                // Agregar la crema al contexto y guardar los cambios
                _context.Tuppers.Add(tupper);
                var result = await _context.SaveChangesAsync();

                // Verificar que se haya guardado correctamente
                if (result > 0)
                {
                    return RedirectToAction("Tuppers");
                }
                else
                {
                    return View(tupper);
                }


            }
        }

        public async Task<IActionResult> CrearElectrodomestico()
        {
            return View(await _context.Electrodomesticos.OrderBy(e => e.IdProducto).LastAsync());
        }
        [HttpPost]
        public async Task<IActionResult> CrearElectrodomestico(Electrodomestico electrodomestico, float Precio)
        {
            var ElectrodomesticoExistente = await _context.Perfumes.FirstOrDefaultAsync(c => c.IdProducto == electrodomestico.IdProducto);
            if (electrodomestico != null)
            {
                ViewBag.Mensaje = "No cambiaste el ID de producto por el siguiente";
                return View(electrodomestico);
            }
            else
            {
                // Verificar si el precio ya existe
                var precioExistente = await _context.Precios.FirstOrDefaultAsync(p => p.Cantidad == Precio);
                electrodomestico.IdCategoria = "E-000";
                if (precioExistente == null)
                {
                    // Si no existe, crear y agregar un nuevo precio
                    Precio precio = new Precio { Cantidad = Precio };
                    _context.Precios.Add(precio); // Cambiado de AddAsync a Add
                    await _context.SaveChangesAsync();

                    // Asignar el ID del nuevo precio al modelo crema
                    electrodomestico.IdPrecio = precio.IdPrecio;
                }
                else
                {
                    // Si el precio ya existe, usar el ID existente
                    electrodomestico.IdPrecio = precioExistente.IdPrecio;

                }


                // Agregar la crema al contexto y guardar los cambios
                _context.Electrodomesticos.Add(electrodomestico);
                var result = await _context.SaveChangesAsync();

                // Verificar que se haya guardado correctamente
                if (result > 0)
                {
                    return RedirectToAction("Electrodomesticos");
                }
                else
                {
                    return View(electrodomestico);
                }


            }
        }

        public async Task<IActionResult> UpdateCrema(string id)
        {
            Crema crema = new Crema();
            Precio precio = new Precio();

            using (var context = _context)
            {
                var cremaEncontrada = await context.Cremas.FindAsync(id);
                if (cremaEncontrada != null)
                {
                    crema.IdCategoria = cremaEncontrada.IdCategoria;
                    crema.IdProducto = cremaEncontrada.IdProducto;
                    crema.Descripcion = cremaEncontrada.Descripcion;
                    crema.IdPrecio = cremaEncontrada.IdPrecio;

                    // Suponiendo que IdPrecio e IdCategoria son claves foráneas para los modelos Precio y Categorium
                    precio = await context.Precios.FindAsync(cremaEncontrada.IdPrecio);
                }
            }
            // Crear un ViewModel si es necesario para pasar múltiples modelos a la vista
            var viewModel = new CremaViewModel
            {
                Crema = crema,
                Precio = precio,
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCrema(CremaViewModel model)
        {
            if (ModelState.IsValid)
            {
                var cremaOriginal = await _context.Cremas.FindAsync(model.Crema.IdProducto);
                var precioOriginal = await _context.Precios.FindAsync(model.Precio.IdPrecio);
                if (cremaOriginal != null && precioOriginal != null)
                {
                    // Actualiza solo los campos que han cambiado
                    if (cremaOriginal.Descripcion != model.Crema.Descripcion)
                    {
                        cremaOriginal.Descripcion = model.Crema.Descripcion;
                    }
                    if (precioOriginal.Cantidad != model.Precio.Cantidad)
                    {
                        precioOriginal.Cantidad = model.Precio.Cantidad;
                    }


                    await _context.SaveChangesAsync();
                }
                ViewBag.Seccion = "Cremas";
                return View("ActualizacionCompleta", ViewBag.Seccion);
            }

            return View(model);
        }

        public async Task<IActionResult> DeleteCrema(string id)
        {
            {
                Crema crema = new Crema();
                Precio precio = new Precio();

                using (var context = _context)
                {
                    var cremaEncontrada = await context.Cremas.FindAsync(id);
                    if (cremaEncontrada != null)
                    {
                        crema.IdCategoria = cremaEncontrada.IdCategoria;
                        crema.IdProducto = cremaEncontrada.IdProducto;
                        crema.Descripcion = cremaEncontrada.Descripcion;
                        crema.IdPrecio = cremaEncontrada.IdPrecio;

                        // Suponiendo que IdPrecio e IdCategoria son claves foráneas para los modelos Precio y Categorium
                        precio = await context.Precios.FindAsync(cremaEncontrada.IdPrecio);
                    }
                }
                // Crear un ViewModel si es necesario para pasar múltiples modelos a la vista
                var viewModel = new CremaViewModel
                {
                    Crema = crema,
                    Precio = precio,
                };

                return View(viewModel);
            }
        }
        [HttpPost]
        public async Task<IActionResult> DeleteCrema(CremaViewModel model)
        {
            var Crema = await _context.Cremas.FindAsync(model.Crema.IdProducto);
            var Precio = await _context.Precios.FindAsync(model.Precio.IdPrecio);

            if (Crema != null && Precio != null)
            {
                _context.Cremas.Remove(Crema);
                _context.Precios.Remove(Precio);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Cremas");

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
