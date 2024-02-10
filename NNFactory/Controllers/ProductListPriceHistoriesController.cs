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
    public class ProductListPriceHistoriesController : Controller
    {
        private readonly AdventureWorks2022Context _context;

        public ProductListPriceHistoriesController(AdventureWorks2022Context context)
        {
            _context = context;
        }

        // GET: ProductListPriceHistories
        public async Task<IActionResult> Index()
        {
            var adventureWorks2022Context = _context.ProductListPriceHistories.Include(p => p.Product);
            return View(await adventureWorks2022Context.ToListAsync());
        }

        // GET: ProductListPriceHistories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productListPriceHistory = await _context.ProductListPriceHistories
                .Include(p => p.Product)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (productListPriceHistory == null)
            {
                return NotFound();
            }

            return View(productListPriceHistory);
        }

        // GET: ProductListPriceHistories/Create
        public IActionResult Create()
        {
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId");
            return View();
        }

        // POST: ProductListPriceHistories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,StartDate,EndDate,ListPrice,ModifiedDate")] ProductListPriceHistory productListPriceHistory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productListPriceHistory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId", productListPriceHistory.ProductId);
            return View(productListPriceHistory);
        }

        // GET: ProductListPriceHistories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productListPriceHistory = await _context.ProductListPriceHistories.FindAsync(id);
            if (productListPriceHistory == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId", productListPriceHistory.ProductId);
            return View(productListPriceHistory);
        }

        // POST: ProductListPriceHistories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,StartDate,EndDate,ListPrice,ModifiedDate")] ProductListPriceHistory productListPriceHistory)
        {
            if (id != productListPriceHistory.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productListPriceHistory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductListPriceHistoryExists(productListPriceHistory.ProductId))
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
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId", productListPriceHistory.ProductId);
            return View(productListPriceHistory);
        }

        // GET: ProductListPriceHistories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productListPriceHistory = await _context.ProductListPriceHistories
                .Include(p => p.Product)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (productListPriceHistory == null)
            {
                return NotFound();
            }

            return View(productListPriceHistory);
        }

        // POST: ProductListPriceHistories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productListPriceHistory = await _context.ProductListPriceHistories.FindAsync(id);
            if (productListPriceHistory != null)
            {
                _context.ProductListPriceHistories.Remove(productListPriceHistory);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductListPriceHistoryExists(int id)
        {
            return _context.ProductListPriceHistories.Any(e => e.ProductId == id);
        }
    }
}
