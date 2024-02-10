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
    public class ProductCostHistoriesController : Controller
    {
        private readonly AdventureWorks2022Context _context;

        public ProductCostHistoriesController(AdventureWorks2022Context context)
        {
            _context = context;
        }

        // GET: ProductCostHistories
        public async Task<IActionResult> Index()
        {
            var adventureWorks2022Context = _context.ProductCostHistories.Include(p => p.Product);
            return View(await adventureWorks2022Context.ToListAsync());
        }

        // GET: ProductCostHistories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productCostHistory = await _context.ProductCostHistories
                .Include(p => p.Product)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (productCostHistory == null)
            {
                return NotFound();
            }

            return View(productCostHistory);
        }

        // GET: ProductCostHistories/Create
        public IActionResult Create()
        {
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId");
            return View();
        }

        // POST: ProductCostHistories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,StartDate,EndDate,StandardCost,ModifiedDate")] ProductCostHistory productCostHistory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productCostHistory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId", productCostHistory.ProductId);
            return View(productCostHistory);
        }

        // GET: ProductCostHistories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productCostHistory = await _context.ProductCostHistories.FindAsync(id);
            if (productCostHistory == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId", productCostHistory.ProductId);
            return View(productCostHistory);
        }

        // POST: ProductCostHistories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,StartDate,EndDate,StandardCost,ModifiedDate")] ProductCostHistory productCostHistory)
        {
            if (id != productCostHistory.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productCostHistory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductCostHistoryExists(productCostHistory.ProductId))
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
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId", productCostHistory.ProductId);
            return View(productCostHistory);
        }

        // GET: ProductCostHistories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productCostHistory = await _context.ProductCostHistories
                .Include(p => p.Product)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (productCostHistory == null)
            {
                return NotFound();
            }

            return View(productCostHistory);
        }

        // POST: ProductCostHistories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productCostHistory = await _context.ProductCostHistories.FindAsync(id);
            if (productCostHistory != null)
            {
                _context.ProductCostHistories.Remove(productCostHistory);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductCostHistoryExists(int id)
        {
            return _context.ProductCostHistories.Any(e => e.ProductId == id);
        }
    }
}
