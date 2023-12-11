using Microsoft.AspNetCore.Mvc;
using SistemaSublimas.AccesoDatos.Repositorio.IRepositorio;
using SistemaSublimas.Modelos;
using SistemaSublimas.Utilidades;


namespace SistemaSublimas.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TiendaController : Controller
    {

        private readonly IUnidadTrabajo _unidadTrabajo;
       

        public TiendaController(IUnidadTrabajo unidadTrabajo)
        {
            _unidadTrabajo = unidadTrabajo;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            Tienda tienda = new Tienda();

            if (id == null)
            {
                // Crear una nueva Tienda
                tienda.Estado = true;
                return View(tienda);
            }
            // Actualizamos Tienda
            tienda = await _unidadTrabajo.Tienda.Obtener(id.GetValueOrDefault());
            if (tienda == null)
            {
                return NotFound();
            }
            return View(tienda);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(Tienda tienda)
        {
            if (ModelState.IsValid)
            {
                if (tienda.Id == 0)
                {
                    await _unidadTrabajo.Tienda.Agregar(tienda);
                    TempData[DS.Exitosa] = "Tienda creada Exitosamente";
                }
                else
                {
                    _unidadTrabajo.Tienda.Actualizar(tienda);
                    TempData[DS.Exitosa] = "Tienda actualizada Exitosamente";
                }
                await _unidadTrabajo.Guardar();
                return RedirectToAction(nameof(Index));
            }
            TempData[DS.Error] = "Error al grabar Tienda";
            return View(tienda);
        }


        #region API

        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var todos = await _unidadTrabajo.Tienda.ObtenerTodos();
            return Json(new {data = todos});
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var tiendaDb = await _unidadTrabajo.Tienda.Obtener(id);
            if (tiendaDb == null)
            {
                return Json(new { success = false, message = "Error al borrar Tienda" });
            }
            _unidadTrabajo.Tienda.Remover(tiendaDb);
            await _unidadTrabajo.Guardar();
            return Json(new { success = true, message = "Tienda borrada exitosamente" });
        }

        [ActionName("ValidarNombre")]
        public async Task<IActionResult> ValidarNombre(string nombre, int id = 0)
        {
            bool valor = false;
            var lista = await _unidadTrabajo.Tienda.ObtenerTodos();
            if (id == 0)
            {
                valor = lista.Any(b => b.Nombre.ToLower().Trim() == nombre.ToLower().Trim());
            }
            else
            {
                valor = lista.Any(b => b.Nombre.ToLower().Trim() == nombre.ToLower().Trim() && b.Id != id);
            }
            if (valor)
            {
                return Json(new { data = true });
            }
            return Json(new { data = false });

        }
        #endregion

    }
}
