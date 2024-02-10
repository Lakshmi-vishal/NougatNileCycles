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
    public class CountryRegionsController : Controller
    {
        private readonly AdventureWorks2022Context _context;

        public CountryRegionsController(AdventureWorks2022Context context)
        {
            _context = context;
        }

        // GET: CountryRegions
        public async Task<IActionResult> Index()
        {
            return View(await _context.CountryRegions.ToListAsync());
        }

        // GET: CountryRegions/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var countryRegion = await _context.CountryRegions
                .FirstOrDefaultAsync(m => m.CountryRegionCode == id);
            if (countryRegion == null)
            {
                return NotFound();
            }

            return View(countryRegion);
        }

        // GET: CountryRegions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CountryRegions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CountryRegionCode,Name,ModifiedDate")] CountryRegion countryRegion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(countryRegion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(countryRegion);
        }

        // GET: CountryRegions/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var countryRegion = await _context.CountryRegions.FindAsync(id);
            if (countryRegion == null)
            {
                return NotFound();
            }
            return View(countryRegion);
        }

        // POST: CountryRegions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("CountryRegionCode,Name,ModifiedDate")] CountryRegion countryRegion)
        {
            if (id != countryRegion.CountryRegionCode)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(countryRegion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CountryRegionExists(countryRegion.CountryRegionCode))
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
            return View(countryRegion);
        }

        // GET: CountryRegions/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var countryRegion = await _context.CountryRegions
                .FirstOrDefaultAsync(m => m.CountryRegionCode == id);
            if (countryRegion == null)
            {
                return NotFound();
            }

            return View(countryRegion);
        }

        // POST: CountryRegions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var countryRegion = await _context.CountryRegions.FindAsync(id);
            if (countryRegion != null)
            {
                _context.CountryRegions.Remove(countryRegion);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CountryRegionExists(string id)
        {
            return _context.CountryRegions.Any(e => e.CountryRegionCode == id);
        }
    }
}
