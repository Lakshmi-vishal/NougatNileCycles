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
    public class StateProvincesController : Controller
    {
        private readonly AdventureWorks2022Context _context;

        public StateProvincesController(AdventureWorks2022Context context)
        {
            _context = context;
        }

        // GET: StateProvinces
        public async Task<IActionResult> Index()
        {
            var adventureWorks2022Context = _context.StateProvinces.Include(s => s.CountryRegionCodeNavigation).Include(s => s.Territory);
            return View(await adventureWorks2022Context.ToListAsync());
        }

        // GET: StateProvinces/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stateProvince = await _context.StateProvinces
                .Include(s => s.CountryRegionCodeNavigation)
                .Include(s => s.Territory)
                .FirstOrDefaultAsync(m => m.StateProvinceId == id);
            if (stateProvince == null)
            {
                return NotFound();
            }

            return View(stateProvince);
        }

        // GET: StateProvinces/Create
        public IActionResult Create()
        {
            ViewData["CountryRegionCode"] = new SelectList(_context.CountryRegions, "CountryRegionCode", "CountryRegionCode");
            ViewData["TerritoryId"] = new SelectList(_context.SalesTerritories, "TerritoryId", "TerritoryId");
            return View();
        }

        // POST: StateProvinces/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StateProvinceId,StateProvinceCode,CountryRegionCode,IsOnlyStateProvinceFlag,Name,TerritoryId,Rowguid,ModifiedDate")] StateProvince stateProvince)
        {
            if (ModelState.IsValid)
            {
                _context.Add(stateProvince);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CountryRegionCode"] = new SelectList(_context.CountryRegions, "CountryRegionCode", "CountryRegionCode", stateProvince.CountryRegionCode);
            ViewData["TerritoryId"] = new SelectList(_context.SalesTerritories, "TerritoryId", "TerritoryId", stateProvince.TerritoryId);
            return View(stateProvince);
        }

        // GET: StateProvinces/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stateProvince = await _context.StateProvinces.FindAsync(id);
            if (stateProvince == null)
            {
                return NotFound();
            }
            ViewData["CountryRegionCode"] = new SelectList(_context.CountryRegions, "CountryRegionCode", "CountryRegionCode", stateProvince.CountryRegionCode);
            ViewData["TerritoryId"] = new SelectList(_context.SalesTerritories, "TerritoryId", "TerritoryId", stateProvince.TerritoryId);
            return View(stateProvince);
        }

        // POST: StateProvinces/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StateProvinceId,StateProvinceCode,CountryRegionCode,IsOnlyStateProvinceFlag,Name,TerritoryId,Rowguid,ModifiedDate")] StateProvince stateProvince)
        {
            if (id != stateProvince.StateProvinceId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(stateProvince);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StateProvinceExists(stateProvince.StateProvinceId))
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
            ViewData["CountryRegionCode"] = new SelectList(_context.CountryRegions, "CountryRegionCode", "CountryRegionCode", stateProvince.CountryRegionCode);
            ViewData["TerritoryId"] = new SelectList(_context.SalesTerritories, "TerritoryId", "TerritoryId", stateProvince.TerritoryId);
            return View(stateProvince);
        }

        // GET: StateProvinces/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stateProvince = await _context.StateProvinces
                .Include(s => s.CountryRegionCodeNavigation)
                .Include(s => s.Territory)
                .FirstOrDefaultAsync(m => m.StateProvinceId == id);
            if (stateProvince == null)
            {
                return NotFound();
            }

            return View(stateProvince);
        }

        // POST: StateProvinces/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var stateProvince = await _context.StateProvinces.FindAsync(id);
            if (stateProvince != null)
            {
                _context.StateProvinces.Remove(stateProvince);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StateProvinceExists(int id)
        {
            return _context.StateProvinces.Any(e => e.StateProvinceId == id);
        }
    }
}
