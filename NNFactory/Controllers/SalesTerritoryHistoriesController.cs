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
    public class SalesTerritoryHistoriesController : Controller
    {
        private readonly AdventureWorks2022Context _context;

        public SalesTerritoryHistoriesController(AdventureWorks2022Context context)
        {
            _context = context;
        }

        // GET: SalesTerritoryHistories
        public async Task<IActionResult> Index()
        {
            var adventureWorks2022Context = _context.SalesTerritoryHistories.Include(s => s.BusinessEntity).Include(s => s.Territory);
            return View(await adventureWorks2022Context.ToListAsync());
        }

        // GET: SalesTerritoryHistories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salesTerritoryHistory = await _context.SalesTerritoryHistories
                .Include(s => s.BusinessEntity)
                .Include(s => s.Territory)
                .FirstOrDefaultAsync(m => m.BusinessEntityId == id);
            if (salesTerritoryHistory == null)
            {
                return NotFound();
            }

            return View(salesTerritoryHistory);
        }

        // GET: SalesTerritoryHistories/Create
        public IActionResult Create()
        {
            ViewData["BusinessEntityId"] = new SelectList(_context.SalesPeople, "BusinessEntityId", "BusinessEntityId");
            ViewData["TerritoryId"] = new SelectList(_context.SalesTerritories, "TerritoryId", "TerritoryId");
            return View();
        }

        // POST: SalesTerritoryHistories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BusinessEntityId,TerritoryId,StartDate,EndDate,Rowguid,ModifiedDate")] SalesTerritoryHistory salesTerritoryHistory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(salesTerritoryHistory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BusinessEntityId"] = new SelectList(_context.SalesPeople, "BusinessEntityId", "BusinessEntityId", salesTerritoryHistory.BusinessEntityId);
            ViewData["TerritoryId"] = new SelectList(_context.SalesTerritories, "TerritoryId", "TerritoryId", salesTerritoryHistory.TerritoryId);
            return View(salesTerritoryHistory);
        }

        // GET: SalesTerritoryHistories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salesTerritoryHistory = await _context.SalesTerritoryHistories.FindAsync(id);
            if (salesTerritoryHistory == null)
            {
                return NotFound();
            }
            ViewData["BusinessEntityId"] = new SelectList(_context.SalesPeople, "BusinessEntityId", "BusinessEntityId", salesTerritoryHistory.BusinessEntityId);
            ViewData["TerritoryId"] = new SelectList(_context.SalesTerritories, "TerritoryId", "TerritoryId", salesTerritoryHistory.TerritoryId);
            return View(salesTerritoryHistory);
        }

        // POST: SalesTerritoryHistories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BusinessEntityId,TerritoryId,StartDate,EndDate,Rowguid,ModifiedDate")] SalesTerritoryHistory salesTerritoryHistory)
        {
            if (id != salesTerritoryHistory.BusinessEntityId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(salesTerritoryHistory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SalesTerritoryHistoryExists(salesTerritoryHistory.BusinessEntityId))
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
            ViewData["BusinessEntityId"] = new SelectList(_context.SalesPeople, "BusinessEntityId", "BusinessEntityId", salesTerritoryHistory.BusinessEntityId);
            ViewData["TerritoryId"] = new SelectList(_context.SalesTerritories, "TerritoryId", "TerritoryId", salesTerritoryHistory.TerritoryId);
            return View(salesTerritoryHistory);
        }

        // GET: SalesTerritoryHistories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salesTerritoryHistory = await _context.SalesTerritoryHistories
                .Include(s => s.BusinessEntity)
                .Include(s => s.Territory)
                .FirstOrDefaultAsync(m => m.BusinessEntityId == id);
            if (salesTerritoryHistory == null)
            {
                return NotFound();
            }

            return View(salesTerritoryHistory);
        }

        // POST: SalesTerritoryHistories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var salesTerritoryHistory = await _context.SalesTerritoryHistories.FindAsync(id);
            if (salesTerritoryHistory != null)
            {
                _context.SalesTerritoryHistories.Remove(salesTerritoryHistory);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SalesTerritoryHistoryExists(int id)
        {
            return _context.SalesTerritoryHistories.Any(e => e.BusinessEntityId == id);
        }
    }
}
