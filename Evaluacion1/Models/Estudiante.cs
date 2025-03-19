using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Evaluacion1.Models.Enum;

namespace Evaluacion1.Models
{
    public class Estudiante : Persona
    {
        [Display(Name = "Estado")]
        public EstadoEstudiante Estado { get; set; } = EstadoEstudiante.NoMatriculado;

        // Relación con Matrícula
        public virtual ICollection<Matricula> Matriculas { get; set; } = new List<Matricula>();
    }
}
