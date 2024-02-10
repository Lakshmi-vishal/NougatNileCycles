using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ChocolateFactory.Models;

namespace ChocolateFactory.Controllers
{
    public class IllustrationsController : Controller
    {
        private readonly AdventureWorks2022Context _context;

        public IllustrationsController(AdventureWorks2022Context context)
        {
            _context = context;
        }

        // GET: Illustrations
        public async Task<IActionResult> Index()
        {
            return View(await _context.Illustrations.ToListAsync());
        }

        // GET: Illustrations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var illustration = await _context.Illustrations
                .FirstOrDefaultAsync(m => m.IllustrationId == id);
            if (illustration == null)
            {
                return NotFound();
            }

            return View(illustration);
        }

        // GET: Illustrations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Illustrations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IllustrationId,Diagram,ModifiedDate")] Illustration illustration)
        {
            if (ModelState.IsValid)
            {
                _context.Add(illustration);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(illustration);
        }

        // GET: Illustrations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var illustration = await _context.Illustrations.FindAsync(id);
            if (illustration == null)
            {
                return NotFound();
            }
            return View(illustration);
        }

        // POST: Illustrations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IllustrationId,Diagram,ModifiedDate")] Illustration illustration)
        {
            if (id != illustration.IllustrationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(illustration);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IllustrationExists(illustration.IllustrationId))
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
            return View(illustration);
        }

        // GET: Illustrations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var illustration = await _context.Illustrations
                .FirstOrDefaultAsync(m => m.IllustrationId == id);
            if (illustration == null)
            {
                return NotFound();
            }

            return View(illustration);
        }

        // POST: Illustrations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var illustration = await _context.Illustrations.FindAsync(id);
            if (illustration != null)
            {
                _context.Illustrations.Remove(illustration);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IllustrationExists(int id)
        {
            return _context.Illustrations.Any(e => e.IllustrationId == id);
        }
    }
}
