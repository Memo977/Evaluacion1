namespace Evaluacion1.Models
{
    public class HomeViewModel
    {
        public string NombreUniversidad { get; set; } = "Universidad Técnica Nacional";
        public string NombreCarrera { get; set; } = "Ingeniería del Software";
        public string NombreCurso { get; set; } = "ISW-712 Ingeniería del Software II";
        public required string CedulaEstudiante { get; set; }
        public required string NombreEstudiante { get; set; }
        public required string ApellidosEstudiante { get; set; }
        public int EdadEstudiante { get; set; }
        public required string DireccionEstudiante { get; set; }

        public required string CedulaEstudiante2 { get; set; }
        public required string NombreEstudiante2 { get; set; }
        public required string ApellidosEstudiante2 { get; set; }
        public int EdadEstudiante2 { get; set; }
        public required string DireccionEstudiante2 { get; set; }
    }
}
