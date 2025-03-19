using Evaluacion1.Helpers;
using Evaluacion1.Models.Enum;
using Evaluacion1.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Evaluacion1.Controllers
{
    public class MatriculasController : Controller
    {
        private readonly IMatriculaService _matriculaService;
        private readonly IEstudianteService _estudianteService;

        public MatriculasController(IMatriculaService matriculaService, IEstudianteService estudianteService)
        {
            _matriculaService = matriculaService;
            _estudianteService = estudianteService;
        }

        // GET: Matriculas
        public async Task<IActionResult> Index()
        {
            var matriculas = await _matriculaService.GetAllAsync();
            return View(matriculas);
        }

        // GET: Matriculas/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var matricula = await _matriculaService.GetByIdAsync(id);
            if (matricula == null)
            {
                return NotFound();
            }

            return View(matricula);
        }

        // GET: Matriculas/Create
        public async Task<IActionResult> Create()
        {
            // Obtener solo los estudiantes no matriculados
            var estudiantes = await _estudianteService.GetAllAsync();
            var estudiantesNoMatriculados = estudiantes.Where(e => e.Estado == EstadoEstudiante.NoMatriculado).ToList();

            // Si no hay estudiantes no matriculados, mostrar mensaje
            if (!estudiantesNoMatriculados.Any())
            {
                TempData["ErrorMessage"] = "No hay estudiantes disponibles para matricular.";
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Estudiantes = new SelectList(estudiantesNoMatriculados, "Id", "NombreCompleto");
            return View();
        }

        // POST: Matriculas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int estudianteId)
        {
            if (ModelState.IsValid)
            {
                // Verificar si el estudiante puede ser matriculado
                bool canMatricular = await _matriculaService.CanMatricularAsync(estudianteId);
                if (!canMatricular)
                {
                    TempData["ErrorMessage"] = "El estudiante ya está matriculado o no existe.";
                    return RedirectToAction(nameof(Create));
                }

                // Crear la matrícula
                var matricula = await _matriculaService.CreateAsync(estudianteId);
                if (matricula != null)
                {
                    TempData["SuccessMessage"] = MessageHelper.GenerarMensajeConfirmacion("creación", "matrícula", true);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["ErrorMessage"] = MessageHelper.GenerarMensajeConfirmacion("creación", "matrícula", false);
                }
            }

            // Si hay error, volver a cargar la lista de estudiantes
            var estudiantes = await _estudianteService.GetAllAsync();
            var estudiantesNoMatriculados = estudiantes.Where(e => e.Estado == EstadoEstudiante.NoMatriculado).ToList();
            ViewBag.Estudiantes = new SelectList(estudiantesNoMatriculados, "Id", "NombreCompleto");

            return View();
        }

        // GET: Matriculas/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var matricula = await _matriculaService.GetByIdAsync(id);
            if (matricula == null)
            {
                return NotFound();
            }

            return View(matricula);
        }

        // POST: Matriculas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            bool result = await _matriculaService.DeleteAsync(id);
            if (result)
            {
                TempData["SuccessMessage"] = MessageHelper.GenerarMensajeConfirmacion("eliminación", "matrícula", true);
            }
            else
            {
                TempData["ErrorMessage"] = MessageHelper.GenerarMensajeConfirmacion("eliminación", "matrícula", false);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
