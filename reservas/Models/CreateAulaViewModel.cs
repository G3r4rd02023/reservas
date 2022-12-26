using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace reservas.Models
{
    public class CreateAulaViewModel : EditAulaViewModel
    {
        [Display(Name = "Edificio")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar un edificio.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int EdificioId { get; set; }
        public IEnumerable<SelectListItem> Edificios { get; set; }
    }
}
