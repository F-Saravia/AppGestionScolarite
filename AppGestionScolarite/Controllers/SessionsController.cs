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
    public class SessionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SessionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Sessions
        public async Task<IActionResult> Index()
        {
            //var sessions = await _context.Sessions.ToListAsync();
            //foreach (var s in sessions) {
            //    var p = await _context.Parcours.FindAsync(s.ParcoursId);
            //    //if p==null......
            //    s.Parcour = p;
            //}
              return 
                
                _context.Sessions != null ? 
                          View(await _context.Sessions.Include(s => s.Parcour).Include(s => s.Parcour.Modules).ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Sessions'  is null.");
        }

        // GET: Sessions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Sessions == null)
            {
                return NotFound();
            }

            var session = await _context.Sessions
                .Include(s => s.Parcour)
                .Include(s => s.Parcour.Modules)
                .FirstOrDefaultAsync(m => m.Id == id);
            
            if (session == null)
            {
                return NotFound();
            }
            //var p = await _context.Parcours.FindAsync(session.ParcoursId);
            ////if p==null......
            //session.Parcour = p;
            return View(session);
        }

        // GET: Sessions/Create
        public IActionResult Create()
        {
            ViewData["ParcoursId"] = new SelectList(_context.Parcours, "Id", "Nom");
            return View();
        }

        // POST: Sessions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DateDebut,DateFin,Intitule,ParcoursId")] Session session)
        {
            if (ModelState.IsValid)
            {
                var p = await _context.Parcours.FindAsync(session.ParcoursId);
                //if p==null......
                session.Parcour = p;

                _context.Add(session);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ParcoursId"] = new SelectList(_context.Parcours, "Id", "Id", session.ParcoursId);
            return View(session);
        }

        /*
         * 
         * public IActionResult Create()
        {
            ViewData["ParcoursId"] = new SelectList(_context.Parcours, "Id", "Nom");
            return View();
        }
         */

        // GET: Sessions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Sessions == null)
            {
                return NotFound();
            }

            var session = await _context.Sessions
                .FindAsync(id);
            if (session == null)
            {
                return NotFound();
            }
            ViewData["ParcoursId"] = new SelectList(_context.Parcours, "Id", "Nom");
            return View(session);
        }

        // POST: Sessions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DateDebut,DateFin,Intitule,ParcoursId")] Session session)
        {
            if (id != session.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var p = await _context.Parcours.FindAsync(session.ParcoursId);
                    //if p==null......
                    session.Parcour = p;
                    _context.Update(session);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SessionExists(session.Id))
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

            ViewData["ParcoursId"] = new SelectList(_context.Parcours, "Id", "Id", session.ParcoursId);
            return View(session);
        }

        // GET: Sessions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Sessions == null)
            {
                return NotFound();
            }

            var session = await _context.Sessions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (session == null)
            {
                return NotFound();
            }

            return View(session);
        }

        // POST: Sessions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Sessions == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Sessions'  is null.");
            }
            var session = await _context.Sessions.FindAsync(id);
            if (session != null)
            {
                _context.Sessions.Remove(session);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SessionExists(int id)
        {
          return (_context.Sessions?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
