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
    public class ScrapReasonsController : Controller
    {
        private readonly AdventureWorks2022Context _context;

        public ScrapReasonsController(AdventureWorks2022Context context)
        {
            _context = context;
        }

        // GET: ScrapReasons
        public async Task<IActionResult> Index()
        {
            return View(await _context.ScrapReasons.ToListAsync());
        }

        // GET: ScrapReasons/Details/5
        public async Task<IActionResult> Details(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var scrapReason = await _context.ScrapReasons
                .FirstOrDefaultAsync(m => m.ScrapReasonId == id);
            if (scrapReason == null)
            {
                return NotFound();
            }

            return View(scrapReason);
        }

        // GET: ScrapReasons/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ScrapReasons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ScrapReasonId,Name,ModifiedDate")] ScrapReason scrapReason)
        {
            if (ModelState.IsValid)
            {
                _context.Add(scrapReason);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(scrapReason);
        }

        // GET: ScrapReasons/Edit/5
        public async Task<IActionResult> Edit(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var scrapReason = await _context.ScrapReasons.FindAsync(id);
            if (scrapReason == null)
            {
                return NotFound();
            }
            return View(scrapReason);
        }

        // POST: ScrapReasons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(short id, [Bind("ScrapReasonId,Name,ModifiedDate")] ScrapReason scrapReason)
        {
            if (id != scrapReason.ScrapReasonId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(scrapReason);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ScrapReasonExists(scrapReason.ScrapReasonId))
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
            return View(scrapReason);
        }

        // GET: ScrapReasons/Delete/5
        public async Task<IActionResult> Delete(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var scrapReason = await _context.ScrapReasons
                .FirstOrDefaultAsync(m => m.ScrapReasonId == id);
            if (scrapReason == null)
            {
                return NotFound();
            }

            return View(scrapReason);
        }

        // POST: ScrapReasons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(short id)
        {
            var scrapReason = await _context.ScrapReasons.FindAsync(id);
            if (scrapReason != null)
            {
                _context.ScrapReasons.Remove(scrapReason);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ScrapReasonExists(short id)
        {
            return _context.ScrapReasons.Any(e => e.ScrapReasonId == id);
        }
    }
}
