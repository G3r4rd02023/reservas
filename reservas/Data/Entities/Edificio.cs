using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace reservas.Data.Entities
{
    public class Edificio
    {
        public int Id { get; set; }
        
        [Display(Name = "Edificio")]
        [MaxLength(100, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Name { get; set; }
        public bool Activo { get; set; }
        public ICollection<Aula> Aulas { get; set; }
    }
}
