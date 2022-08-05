using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AppGestionScolarite.Data;
using AppGestionScolarite.Models;

namespace AppGestionScolarite.Controllers
{
    public class ModulesController : Controller
    {
        private IWebHostEnvironment _webHostEnvironment;
        private readonly ApplicationDbContext _context;

        public ModulesController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Modules
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Modules.Include(module => module.UnitePedagogique)
                .Include(module => module.Parcours);
            var res = await applicationDbContext.ToListAsync();
            return View(res);
        }

        // GET: Modules/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Modules == null)
            {
                return NotFound();
            }

            var @module = await _context.Modules
                .Include(module => module.UnitePedagogique)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@module == null)
            {
                return NotFound();
            }

            return View(@module);
        }

        // GET: Modules/Create
        public IActionResult Create()
        {
            ViewData["UnitePedagogiqueId"] = new SelectList(_context.UnitePedagogique, "Id", "Designation");
            ViewData["ParcoursId"] = new SelectList(_context.Parcours, "Id", "Nom");
            return View();
        }

        // POST: Modules/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nom,Resume,Infos,Logo,ParcoursId,UnitePedagogiqueId")] Module @module, IFormFile? Logo)
        {
            if (ModelState.IsValid)
            {
                if (Logo != null)
                {
                    string rootPath = _webHostEnvironment.WebRootPath;
                    string fileName = Path.GetFileNameWithoutExtension(Logo.FileName) + "_" +
                               Guid.NewGuid() +
                               Path.GetExtension(Logo.FileName);
                    string path = Path.Combine(rootPath + "/photoLogoModule/", fileName);
                    
                    var fileStream = new FileStream(path, FileMode.Create);
                    await Logo.CopyToAsync(fileStream);
                    fileStream.Close();
                    @module.Logo = fileName;
                }
                var parcoursToAdd = await _context.Parcours.FindAsync(@module.ParcoursId);
                //if parcoursToAdd  null....
                var unitePedagogiqueToAdd = await _context.UnitePedagogique.FindAsync(@module.UnitePedagogiqueId);
                //if unitePedagogiqueToAdd  null....
                if (@module.Parcours == null)
                    @module.Parcours = new List<Parcour>();
                @module.Parcours.Add(parcoursToAdd);
                @module.UnitePedagogique = unitePedagogiqueToAdd;

                _context.Add(@module);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ParcoursId"] = new SelectList(_context.Parcours, "Id", "Id", @module.ParcoursId);
            ViewData["UnitePedagogiqueId"] = new SelectList(_context.UnitePedagogique, "Id", "Id", @module.UnitePedagogiqueId);
            return View(@module);
        }

        // GET: Modules/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Modules == null)
            {
                return NotFound();
            }

            var @module = await _context.Modules.FindAsync(id);
            if (@module == null)
            {
                return NotFound();
            }
            ViewData["ParcoursId"] = new SelectList(_context.Parcours, "Id", "Nom");
            ViewData["UnitePedagogiqueId"] = new SelectList(_context.Set<UnitePedagogique>(), "Id", "Designation", @module.UnitePedagogiqueId);
            return View(@module);
        }

        // POST: Modules/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nom,Resume,Infos,Logo,ParcoursId,UnitePedagogiqueId")] Module @module, IFormFile? Logo)
        {
            if (id != @module.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (Logo != null)
                    {
                        string rootPath = _webHostEnvironment.WebRootPath;
                        string fileName = Path.GetFileNameWithoutExtension(Logo.FileName) + "_" +
                                   Guid.NewGuid() +
                                   Path.GetExtension(Logo.FileName);
                        string path = Path.Combine(rootPath + "/photoLogoModule/", fileName);

                        var fileStream = new FileStream(path, FileMode.Create);
                        await Logo.CopyToAsync(fileStream);
                        fileStream.Close();
                        module.Logo = fileName;
                    }
                    _context.Update(@module);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ModuleExists(@module.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["UnitePedagogiqueId"] = new SelectList(_context.Set<UnitePedagogique>(), "Id", "Id", @module.UnitePedagogiqueId);
            ViewData["ParcoursId"] = new SelectList(_context.Parcours, "Id", "Id", @module.ParcoursId);
            return View(@module);
        }

        // GET: Modules/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Modules == null)
            {
                return NotFound();
            }

            var @module = await _context.Modules
                .Include(module =>  module.UnitePedagogique)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@module == null)
            {
                return NotFound();
            }

            return View(@module);
        }

        // POST: Modules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Modules == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Modules'  is null.");
            }
            var @module = await _context.Modules.FindAsync(id);
            if (@module != null)
            {
                _context.Modules.Remove(@module);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ModuleExists(int id)
        {
          return (_context.Modules?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
