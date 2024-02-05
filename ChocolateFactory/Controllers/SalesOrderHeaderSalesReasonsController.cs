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
    public class SalesOrderHeaderSalesReasonsController : Controller
    {
        private readonly AdventureWorks2022Context _context;

        public SalesOrderHeaderSalesReasonsController(AdventureWorks2022Context context)
        {
            _context = context;
        }

        // GET: SalesOrderHeaderSalesReasons
        public async Task<IActionResult> Index()
        {
            var adventureWorks2022Context = _context.SalesOrderHeaderSalesReasons.Include(s => s.SalesOrder).Include(s => s.SalesReason);
            return View(await adventureWorks2022Context.ToListAsync());
        }

        // GET: SalesOrderHeaderSalesReasons/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salesOrderHeaderSalesReason = await _context.SalesOrderHeaderSalesReasons
                .Include(s => s.SalesOrder)
                .Include(s => s.SalesReason)
                .FirstOrDefaultAsync(m => m.SalesOrderId == id);
            if (salesOrderHeaderSalesReason == null)
            {
                return NotFound();
            }

            return View(salesOrderHeaderSalesReason);
        }

        // GET: SalesOrderHeaderSalesReasons/Create
        public IActionResult Create()
        {
            ViewData["SalesOrderId"] = new SelectList(_context.SalesOrderHeaders, "SalesOrderId", "SalesOrderId");
            ViewData["SalesReasonId"] = new SelectList(_context.SalesReasons, "SalesReasonId", "SalesReasonId");
            return View();
        }

        // POST: SalesOrderHeaderSalesReasons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SalesOrderId,SalesReasonId,ModifiedDate")] SalesOrderHeaderSalesReason salesOrderHeaderSalesReason)
        {
            if (ModelState.IsValid)
            {
                _context.Add(salesOrderHeaderSalesReason);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SalesOrderId"] = new SelectList(_context.SalesOrderHeaders, "SalesOrderId", "SalesOrderId", salesOrderHeaderSalesReason.SalesOrderId);
            ViewData["SalesReasonId"] = new SelectList(_context.SalesReasons, "SalesReasonId", "SalesReasonId", salesOrderHeaderSalesReason.SalesReasonId);
            return View(salesOrderHeaderSalesReason);
        }

        // GET: SalesOrderHeaderSalesReasons/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salesOrderHeaderSalesReason = await _context.SalesOrderHeaderSalesReasons.FindAsync(id);
            if (salesOrderHeaderSalesReason == null)
            {
                return NotFound();
            }
            ViewData["SalesOrderId"] = new SelectList(_context.SalesOrderHeaders, "SalesOrderId", "SalesOrderId", salesOrderHeaderSalesReason.SalesOrderId);
            ViewData["SalesReasonId"] = new SelectList(_context.SalesReasons, "SalesReasonId", "SalesReasonId", salesOrderHeaderSalesReason.SalesReasonId);
            return View(salesOrderHeaderSalesReason);
        }

        // POST: SalesOrderHeaderSalesReasons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SalesOrderId,SalesReasonId,ModifiedDate")] SalesOrderHeaderSalesReason salesOrderHeaderSalesReason)
        {
            if (id != salesOrderHeaderSalesReason.SalesOrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(salesOrderHeaderSalesReason);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SalesOrderHeaderSalesReasonExists(salesOrderHeaderSalesReason.SalesOrderId))
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
            ViewData["SalesOrderId"] = new SelectList(_context.SalesOrderHeaders, "SalesOrderId", "SalesOrderId", salesOrderHeaderSalesReason.SalesOrderId);
            ViewData["SalesReasonId"] = new SelectList(_context.SalesReasons, "SalesReasonId", "SalesReasonId", salesOrderHeaderSalesReason.SalesReasonId);
            return View(salesOrderHeaderSalesReason);
        }

        // GET: SalesOrderHeaderSalesReasons/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salesOrderHeaderSalesReason = await _context.SalesOrderHeaderSalesReasons
                .Include(s => s.SalesOrder)
                .Include(s => s.SalesReason)
                .FirstOrDefaultAsync(m => m.SalesOrderId == id);
            if (salesOrderHeaderSalesReason == null)
            {
                return NotFound();
            }

            return View(salesOrderHeaderSalesReason);
        }

        // POST: SalesOrderHeaderSalesReasons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var salesOrderHeaderSalesReason = await _context.SalesOrderHeaderSalesReasons.FindAsync(id);
            if (salesOrderHeaderSalesReason != null)
            {
                _context.SalesOrderHeaderSalesReasons.Remove(salesOrderHeaderSalesReason);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SalesOrderHeaderSalesReasonExists(int id)
        {
            return _context.SalesOrderHeaderSalesReasons.Any(e => e.SalesOrderId == id);
        }
    }
}
