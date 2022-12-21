using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using reservas.Data;
using reservas.Data.Entities;

namespace reservas.Controllers
{
    public class EdificiosController : Controller
    {
        private readonly DataContext _context;

        public EdificiosController(DataContext context)
        {
            _context = context;
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
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "Ya existe una edificio con ese nombre.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
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
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "Ya existe una edificio con ese nombre.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
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

            _context.Edificios.Remove(edificio);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            
        }                       
    }
}
