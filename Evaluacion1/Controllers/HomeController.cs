using Evaluacion1.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Evaluacion1.Controllers
{
    public class HomeController : Controller
    {
        // Constructor vacío (podríamos inyectar servicios si se necesitara)
        public HomeController()
        {
        }

        // Acción Index que muestra la página de inicio
        public IActionResult Index()
        {
            // Crear el ViewModel con la información requerida
            var viewModel = new HomeViewModel
            {
                NombreUniversidad = "Universidad Técnica Nacional",
                NombreCarrera = "Ingeniería del Software",
                NombreCurso = "ISW-712 Ingeniería del Software II",
                CedulaEstudiante = "207730527",
                NombreEstudiante = "Guillermo",
                ApellidosEstudiante = "Solórzano Ochoa",
                EdadEstudiante = 27,
                DireccionEstudiante = "Ciudad Quesada, Costa Rica",

                CedulaEstudiante2 = "208490353",
                NombreEstudiante2 = "Irella", 
                ApellidosEstudiante2 = "León Vargas", 
                EdadEstudiante2 = 25, 
                DireccionEstudiante2 = "Dirección" 
            };

            return View(viewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
