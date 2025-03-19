using Evaluacion1.Helpers;
using Evaluacion1.Models;
using Evaluacion1.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Evaluacion1.Controllers
{
    public class EstudiantesController : Controller
    {
        private readonly IEstudianteService _estudianteService;
        private readonly IValidationService _validationService;

        public EstudiantesController(IEstudianteService estudianteService, IValidationService validationService)
        {
            _estudianteService = estudianteService;
            _validationService = validationService;
        }

        // GET: Estudiantes
        public async Task<IActionResult> Index()
        {
            var estudiantes = await _estudianteService.GetAllAsync();
            return View(estudiantes);
        }

        // GET: Estudiantes/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var estudiante = await _estudianteService.GetByIdAsync(id);
            if (estudiante == null)
            {
                return NotFound();
            }

            return View(estudiante);
        }

        // GET: Estudiantes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Estudiantes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Cedula,Nombre,Apellido1,Apellido2,Sexo,Estado")] Estudiante estudiante)
        {
            if (ModelState.IsValid)
            {
                // Usar el servicio de validación en lugar de la validación directa
                Dictionary<string, string> errores;
                if (!_validationService.ValidarEstudiante(estudiante, out errores))
                {
                    foreach (var error in errores)
                    {
                        ModelState.AddModelError(error.Key, error.Value);
                    }
                    return View(estudiante);
                }

                await _estudianteService.CreateAsync(estudiante);
                TempData["SuccessMessage"] = MessageHelper.GenerarMensajeConfirmacion("creación", "estudiante", true);
                return RedirectToAction(nameof(Index));
            }
            return View(estudiante);
        }

        // GET: Estudiantes/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var estudiante = await _estudianteService.GetByIdAsync(id);
            if (estudiante == null)
            {
                return NotFound();
            }
            return View(estudiante);
        }

        // POST: Estudiantes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Cedula,Nombre,Apellido1,Apellido2,Sexo,Estado")] Estudiante estudiante)
        {
            if (id != estudiante.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                // Usar el servicio de validación
                Dictionary<string, string> errores;
                if (!_validationService.ValidarEstudiante(estudiante, out errores))
                {
                    foreach (var error in errores)
                    {
                        ModelState.AddModelError(error.Key, error.Value);
                    }
                    return View(estudiante);
                }

                var result = await _estudianteService.UpdateAsync(estudiante);
                if (result != null)
                {
                    TempData["SuccessMessage"] = MessageHelper.GenerarMensajeConfirmacion("actualización", "estudiante", true);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["ErrorMessage"] = MessageHelper.GenerarMensajeConfirmacion("actualización", "estudiante", false);
                }
            }
            return View(estudiante);
        }

        // GET: Estudiantes/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var estudiante = await _estudianteService.GetByIdAsync(id);
            if (estudiante == null)
            {
                return NotFound();
            }

            // Verificar si el estudiante puede ser eliminado
            ViewBag.CanDelete = await _estudianteService.CanDeleteAsync(id);

            return View(estudiante);
        }

        // POST: Estudiantes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // Verificar si el estudiante puede ser eliminado
            bool canDelete = await _estudianteService.CanDeleteAsync(id);
            if (!canDelete)
            {
                TempData["ErrorMessage"] = "No se puede eliminar un estudiante matriculado.";
                return RedirectToAction(nameof(Index));
            }

            bool result = await _estudianteService.DeleteAsync(id);
            if (result)
            {
                TempData["SuccessMessage"] = MessageHelper.GenerarMensajeConfirmacion("eliminación", "estudiante", true);
            }
            else
            {
                TempData["ErrorMessage"] = MessageHelper.GenerarMensajeConfirmacion("eliminación", "estudiante", false);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}