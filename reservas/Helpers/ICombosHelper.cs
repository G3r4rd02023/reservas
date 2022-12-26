using Microsoft.AspNetCore.Mvc.Rendering;

namespace reservas.Helpers
{
    public interface ICombosHelper
    {
        Task<IEnumerable<SelectListItem>> GetComboEdificiosAsync();
    }
}
