using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using reservas.Data;
using reservas.Data.Entities;
using reservas.Helpers;
using reservas.Models;
using Vereyon.Web;

namespace reservas.Controllers
{
    public class AulasController : Controller
    {
        private readonly DataContext _context;
        private readonly ICombosHelper _combosHelper;
        private readonly IFlashMessage _flashMessage;

        public AulasController(DataContext context, ICombosHelper combosHelper, IFlashMessage flashMessage)
        {
            _context = context;
            _combosHelper = combosHelper;
            _flashMessage = flashMessage;            
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Aulas
                .Include(p => p.Edificio)
                .ToListAsync());
        }

        public async Task<IActionResult> Create()
        {
            CreateAulaViewModel model = new()
            {
                Edificios = await _combosHelper.GetComboEdificiosAsync(),
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateAulaViewModel model)
        {
            if (ModelState.IsValid)
            {
                var edificio = await _context.Edificios.FindAsync(model.EdificioId);
                if (edificio == null)
                {
                    return NotFound();
                }

                Aula aula = new()
                {
                    Name = model.Name,
                    Capacidad = model.Capacidad,
                    Edificio = edificio,
                    Activo = true,
                };

                try
                {
                    _context.Add(aula);
                    await _context.SaveChangesAsync();
                    _flashMessage.Confirmation("Aula creada exitosamente!.");
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
            model.Edificios = await _combosHelper.GetComboEdificiosAsync();
            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            Aula aula = await _context.Aulas.FindAsync(id);
            if (aula == null)
            {
                return NotFound();
            }

            EditAulaViewModel model = new()
            {

                Id = aula.Id,
                Name = aula.Name,
                Capacidad = aula.Capacidad,
                Activo = aula.Activo

            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CreateAulaViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            try
            {
                Aula aula = await _context.Aulas.FindAsync(model.Id);

                aula.Name = model.Name;
                aula.Capacidad = model.Capacidad;
                aula.Activo = model.Activo;              
                _context.Update(aula);
                await _context.SaveChangesAsync();
                _flashMessage.Confirmation("Registro actualizado exitosamente!");
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                {
                    _flashMessage.Danger("Ya existe un aula con el mismo nombre.");
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

            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            Aula aula = await _context.Aulas
                .Include(a => a.Edificio)
                .FirstOrDefaultAsync(p => p.Id == id);
            if (aula == null)
            {
                return NotFound();
            }
            
            _context.Aulas.Remove(aula);
            await _context.SaveChangesAsync();
            _flashMessage.Info("Registro borrado.");
            return RedirectToAction(nameof(Index));
        }
    }
}
