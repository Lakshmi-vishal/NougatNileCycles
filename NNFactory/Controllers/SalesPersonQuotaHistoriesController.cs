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
    public class SalesPersonQuotaHistoriesController : Controller
    {
        private readonly AdventureWorks2022Context _context;

        public SalesPersonQuotaHistoriesController(AdventureWorks2022Context context)
        {
            _context = context;
        }

        // GET: SalesPersonQuotaHistories
        public async Task<IActionResult> Index()
        {
            var adventureWorks2022Context = _context.SalesPersonQuotaHistories.Include(s => s.BusinessEntity);
            return View(await adventureWorks2022Context.ToListAsync());
        }

        // GET: SalesPersonQuotaHistories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salesPersonQuotaHistory = await _context.SalesPersonQuotaHistories
                .Include(s => s.BusinessEntity)
                .FirstOrDefaultAsync(m => m.BusinessEntityId == id);
            if (salesPersonQuotaHistory == null)
            {
                return NotFound();
            }

            return View(salesPersonQuotaHistory);
        }

        // GET: SalesPersonQuotaHistories/Create
        public IActionResult Create()
        {
            ViewData["BusinessEntityId"] = new SelectList(_context.SalesPeople, "BusinessEntityId", "BusinessEntityId");
            return View();
        }

        // POST: SalesPersonQuotaHistories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BusinessEntityId,QuotaDate,SalesQuota,Rowguid,ModifiedDate")] SalesPersonQuotaHistory salesPersonQuotaHistory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(salesPersonQuotaHistory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BusinessEntityId"] = new SelectList(_context.SalesPeople, "BusinessEntityId", "BusinessEntityId", salesPersonQuotaHistory.BusinessEntityId);
            return View(salesPersonQuotaHistory);
        }

        // GET: SalesPersonQuotaHistories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salesPersonQuotaHistory = await _context.SalesPersonQuotaHistories.FindAsync(id);
            if (salesPersonQuotaHistory == null)
            {
                return NotFound();
            }
            ViewData["BusinessEntityId"] = new SelectList(_context.SalesPeople, "BusinessEntityId", "BusinessEntityId", salesPersonQuotaHistory.BusinessEntityId);
            return View(salesPersonQuotaHistory);
        }

        // POST: SalesPersonQuotaHistories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BusinessEntityId,QuotaDate,SalesQuota,Rowguid,ModifiedDate")] SalesPersonQuotaHistory salesPersonQuotaHistory)
        {
            if (id != salesPersonQuotaHistory.BusinessEntityId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(salesPersonQuotaHistory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SalesPersonQuotaHistoryExists(salesPersonQuotaHistory.BusinessEntityId))
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
            ViewData["BusinessEntityId"] = new SelectList(_context.SalesPeople, "BusinessEntityId", "BusinessEntityId", salesPersonQuotaHistory.BusinessEntityId);
            return View(salesPersonQuotaHistory);
        }

        // GET: SalesPersonQuotaHistories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salesPersonQuotaHistory = await _context.SalesPersonQuotaHistories
                .Include(s => s.BusinessEntity)
                .FirstOrDefaultAsync(m => m.BusinessEntityId == id);
            if (salesPersonQuotaHistory == null)
            {
                return NotFound();
            }

            return View(salesPersonQuotaHistory);
        }

        // POST: SalesPersonQuotaHistories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var salesPersonQuotaHistory = await _context.SalesPersonQuotaHistories.FindAsync(id);
            if (salesPersonQuotaHistory != null)
            {
                _context.SalesPersonQuotaHistories.Remove(salesPersonQuotaHistory);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SalesPersonQuotaHistoryExists(int id)
        {
            return _context.SalesPersonQuotaHistories.Any(e => e.BusinessEntityId == id);
        }
    }
}
