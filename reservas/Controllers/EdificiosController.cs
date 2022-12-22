using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using reservas.Data;
using reservas.Data.Entities;
using Vereyon.Web;

namespace reservas.Controllers
{
    public class EdificiosController : Controller
    {
        private readonly DataContext _context;
        private readonly IFlashMessage _flashMessage;

        public EdificiosController(DataContext context, IFlashMessage flashMessage)
        {
            _context = context;
            _flashMessage = flashMessage;
        }

        
        public async Task<IActionResult> Index()
        {
            return View(await _context.Edificios
                .ToListAsync());
        }

        
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Edificios == null)
            {
                return NotFound();
            }

            var edificio = await _context.Edificios
                .FirstOrDefaultAsync(m => m.Id == id);
            if (edificio == null)
            {
                return NotFound();
            }

            return View(edificio);
        }

        
        public IActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Edificio edificio)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(edificio);
                    await _context.SaveChangesAsync();
                    _flashMessage.Info("Edficio creado exitosamente!");
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        _flashMessage.Danger("Ya existe un edificio con el mismo nombre.");
                    }
                    else
                    {
                        _flashMessage.Danger(dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception exception)
                {
                    _flashMessage.Danger(exception.Message);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(edificio);
        }

        
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Edificios == null)
            {
                return NotFound();
            }

            var edificio = await _context.Edificios.FindAsync(id);
            if (edificio == null)
            {
                return NotFound();
            }
            return View(edificio);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,  Edificio edificio)
        {
            if (id != edificio.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(edificio);                    
                    await _context.SaveChangesAsync();
                    _flashMessage.Info("Edficio actualizado exitosamente!");
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        _flashMessage.Danger("Ya existe una edificio con el mismo nombre.");
                    }
                    else
                    {
                        _flashMessage.Danger(dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception exception)
                {
                    _flashMessage.Danger(exception.Message);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(edificio);
        }

        
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Edificios == null)
            {
                return NotFound();
            }

            var edificio = await _context.Edificios
                .FirstOrDefaultAsync(m => m.Id == id);
            if (edificio == null)
            {
                return NotFound();
            }

            try
            {
                _context.Edificios.Remove(edificio);
                await _context.SaveChangesAsync();
                _flashMessage.Info("Registro borrado.");
                
            }
            catch 
            {
                _flashMessage.Danger("No se puede borrar la categoría porque tiene registros relacionados.");
            }

            return RedirectToAction(nameof(Index));
        }                       
    }
}
