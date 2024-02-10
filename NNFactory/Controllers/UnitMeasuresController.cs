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
    public class UnitMeasuresController : Controller
    {
        private readonly AdventureWorks2022Context _context;

        public UnitMeasuresController(AdventureWorks2022Context context)
        {
            _context = context;
        }

        // GET: UnitMeasures
        public async Task<IActionResult> Index()
        {
            return View(await _context.UnitMeasures.ToListAsync());
        }

        // GET: UnitMeasures/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var unitMeasure = await _context.UnitMeasures
                .FirstOrDefaultAsync(m => m.UnitMeasureCode == id);
            if (unitMeasure == null)
            {
                return NotFound();
            }

            return View(unitMeasure);
        }

        // GET: UnitMeasures/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UnitMeasures/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UnitMeasureCode,Name,ModifiedDate")] UnitMeasure unitMeasure)
        {
            if (ModelState.IsValid)
            {
                _context.Add(unitMeasure);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(unitMeasure);
        }

        // GET: UnitMeasures/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var unitMeasure = await _context.UnitMeasures.FindAsync(id);
            if (unitMeasure == null)
            {
                return NotFound();
            }
            return View(unitMeasure);
        }

        // POST: UnitMeasures/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("UnitMeasureCode,Name,ModifiedDate")] UnitMeasure unitMeasure)
        {
            if (id != unitMeasure.UnitMeasureCode)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(unitMeasure);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UnitMeasureExists(unitMeasure.UnitMeasureCode))
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
            return View(unitMeasure);
        }

        // GET: UnitMeasures/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var unitMeasure = await _context.UnitMeasures
                .FirstOrDefaultAsync(m => m.UnitMeasureCode == id);
            if (unitMeasure == null)
            {
                return NotFound();
            }

            return View(unitMeasure);
        }

        // POST: UnitMeasures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var unitMeasure = await _context.UnitMeasures.FindAsync(id);
            if (unitMeasure != null)
            {
                _context.UnitMeasures.Remove(unitMeasure);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UnitMeasureExists(string id)
        {
            return _context.UnitMeasures.Any(e => e.UnitMeasureCode == id);
        }
    }
}
