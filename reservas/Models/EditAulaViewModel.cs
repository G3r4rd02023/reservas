using System.ComponentModel.DataAnnotations;

namespace reservas.Models
{
    public class EditAulaViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Nombre")]
        [MaxLength(100, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Name { get; set; }

        [Display(Name = "Capacidad")]
        public int Capacidad { get; set; }
        public bool Activo { get; set; }
    }
}
