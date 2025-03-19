using Evaluacion1.Models.Enum;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Evaluacion1.Models
{
    public abstract class Persona
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "La cédula es obligatoria")]
        [Display(Name = "Cédula")]
        public required string Cedula { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [Display(Name = "Nombre")]
        public required string Nombre { get; set; }

        [Required(ErrorMessage = "El primer apellido es obligatorio")]
        [Display(Name = "Primer Apellido")]
        public required string Apellido1 { get; set; }

        [Required(ErrorMessage = "El segundo apellido es obligatorio")]
        [Display(Name = "Segundo Apellido")]
        public required string Apellido2 { get; set; }

        [Required(ErrorMessage = "El sexo es obligatorio")]
        [Display(Name = "Sexo")]
        public Sexo Sexo { get; set; }

        [NotMapped]
        public string NombreCompleto => $"{Nombre} {Apellido1} {Apellido2}";
    }
}
