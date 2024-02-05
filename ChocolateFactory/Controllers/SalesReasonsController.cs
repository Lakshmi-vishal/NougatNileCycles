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
    public class SalesReasonsController : Controller
    {
        private readonly AdventureWorks2022Context _context;

        public SalesReasonsController(AdventureWorks2022Context context)
        {
            _context = context;
        }

        // GET: SalesReasons
        public async Task<IActionResult> Index()
        {
            return View(await _context.SalesReasons.ToListAsync());
        }

        // GET: SalesReasons/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salesReason = await _context.SalesReasons
                .FirstOrDefaultAsync(m => m.SalesReasonId == id);
            if (salesReason == null)
            {
                return NotFound();
            }

            return View(salesReason);
        }

        // GET: SalesReasons/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SalesReasons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SalesReasonId,Name,ReasonType,ModifiedDate")] SalesReason salesReason)
        {
            if (ModelState.IsValid)
            {
                _context.Add(salesReason);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(salesReason);
        }

        // GET: SalesReasons/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salesReason = await _context.SalesReasons.FindAsync(id);
            if (salesReason == null)
            {
                return NotFound();
            }
            return View(salesReason);
        }

        // POST: SalesReasons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SalesReasonId,Name,ReasonType,ModifiedDate")] SalesReason salesReason)
        {
            if (id != salesReason.SalesReasonId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(salesReason);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SalesReasonExists(salesReason.SalesReasonId))
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
            return View(salesReason);
        }

        // GET: SalesReasons/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salesReason = await _context.SalesReasons
                .FirstOrDefaultAsync(m => m.SalesReasonId == id);
            if (salesReason == null)
            {
                return NotFound();
            }

            return View(salesReason);
        }

        // POST: SalesReasons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var salesReason = await _context.SalesReasons.FindAsync(id);
            if (salesReason != null)
            {
                _context.SalesReasons.Remove(salesReason);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SalesReasonExists(int id)
        {
            return _context.SalesReasons.Any(e => e.SalesReasonId == id);
        }
    }
}
