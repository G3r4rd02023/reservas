using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using reservas.Data;

namespace reservas.Helpers
{
    public class CombosHelper : ICombosHelper
    {
        private readonly DataContext _context;

        public CombosHelper(DataContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<SelectListItem>> GetComboEdificiosAsync()
        {
            List<SelectListItem> list = await _context.Edificios.Where(e => e.Activo == true)
               .Select(e => new SelectListItem
               {
                   Text = e.Name,
                   Value = e.Id.ToString()
               })
               .OrderBy(e => e.Text)
               .ToListAsync();

            list.Insert(0, new SelectListItem { Text = "[Seleccione un edificio...", Value = "0" });
            return list;
        }
    }
}
