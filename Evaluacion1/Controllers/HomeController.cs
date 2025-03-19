using Evaluacion1.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Evaluacion1.Controllers
{
    public class HomeController : Controller
    {
        // Constructor vac�o (podr�amos inyectar servicios si se necesitara)
        public HomeController()
        {
        }

        // Acci�n Index que muestra la p�gina de inicio
        public IActionResult Index()
        {
            // Crear el ViewModel con la informaci�n requerida
            var viewModel = new HomeViewModel
            {
                NombreUniversidad = "Universidad T�cnica Nacional",
                NombreCarrera = "Ingenier�a del Software",
                NombreCurso = "ISW-712 Ingenier�a del Software II",
                CedulaEstudiante = "207730527",
                NombreEstudiante = "Guillermo",
                ApellidosEstudiante = "Sol�rzano Ochoa",
                EdadEstudiante = 27,
                DireccionEstudiante = "Ciudad Quesada, Costa Rica",

                CedulaEstudiante2 = "208490353",
                NombreEstudiante2 = "Irella", 
                ApellidosEstudiante2 = "Le�n Vargas", 
                EdadEstudiante2 = 25, 
                DireccionEstudiante2 = "Direcci�n" 
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
