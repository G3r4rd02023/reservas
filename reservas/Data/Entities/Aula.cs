using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace reservas.Data.Entities
{
    public class Aula
    {
        public int Id { get; set; }
        
        [Display(Name = "Aula")]
        [MaxLength(100, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Name { get; set; }

        [Display(Name = "Capacidad")]
        public int Capacidad { get; set; }
        public bool Activo { get; set; }

        [JsonIgnore]
        public Edificio Edificio { get; set; }
    }
}
