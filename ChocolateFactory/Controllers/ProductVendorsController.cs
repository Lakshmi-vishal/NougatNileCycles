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
    public class ProductVendorsController : Controller
    {
        private readonly AdventureWorks2022Context _context;

        public ProductVendorsController(AdventureWorks2022Context context)
        {
            _context = context;
        }

        // GET: ProductVendors
        public async Task<IActionResult> Index()
        {
            var adventureWorks2022Context = _context.ProductVendors.Include(p => p.BusinessEntity).Include(p => p.Product).Include(p => p.UnitMeasureCodeNavigation);
            return View(await adventureWorks2022Context.ToListAsync());
        }

        // GET: ProductVendors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productVendor = await _context.ProductVendors
                .Include(p => p.BusinessEntity)
                .Include(p => p.Product)
                .Include(p => p.UnitMeasureCodeNavigation)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (productVendor == null)
            {
                return NotFound();
            }

            return View(productVendor);
        }

        // GET: ProductVendors/Create
        public IActionResult Create()
        {
            ViewData["BusinessEntityId"] = new SelectList(_context.Vendors, "BusinessEntityId", "BusinessEntityId");
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId");
            ViewData["UnitMeasureCode"] = new SelectList(_context.UnitMeasures, "UnitMeasureCode", "UnitMeasureCode");
            return View();
        }

        // POST: ProductVendors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,BusinessEntityId,AverageLeadTime,StandardPrice,LastReceiptCost,LastReceiptDate,MinOrderQty,MaxOrderQty,OnOrderQty,UnitMeasureCode,ModifiedDate")] ProductVendor productVendor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productVendor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BusinessEntityId"] = new SelectList(_context.Vendors, "BusinessEntityId", "BusinessEntityId", productVendor.BusinessEntityId);
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId", productVendor.ProductId);
            ViewData["UnitMeasureCode"] = new SelectList(_context.UnitMeasures, "UnitMeasureCode", "UnitMeasureCode", productVendor.UnitMeasureCode);
            return View(productVendor);
        }

        // GET: ProductVendors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productVendor = await _context.ProductVendors.FindAsync(id);
            if (productVendor == null)
            {
                return NotFound();
            }
            ViewData["BusinessEntityId"] = new SelectList(_context.Vendors, "BusinessEntityId", "BusinessEntityId", productVendor.BusinessEntityId);
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId", productVendor.ProductId);
            ViewData["UnitMeasureCode"] = new SelectList(_context.UnitMeasures, "UnitMeasureCode", "UnitMeasureCode", productVendor.UnitMeasureCode);
            return View(productVendor);
        }

        // POST: ProductVendors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,BusinessEntityId,AverageLeadTime,StandardPrice,LastReceiptCost,LastReceiptDate,MinOrderQty,MaxOrderQty,OnOrderQty,UnitMeasureCode,ModifiedDate")] ProductVendor productVendor)
        {
            if (id != productVendor.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productVendor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductVendorExists(productVendor.ProductId))
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
            ViewData["BusinessEntityId"] = new SelectList(_context.Vendors, "BusinessEntityId", "BusinessEntityId", productVendor.BusinessEntityId);
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId", productVendor.ProductId);
            ViewData["UnitMeasureCode"] = new SelectList(_context.UnitMeasures, "UnitMeasureCode", "UnitMeasureCode", productVendor.UnitMeasureCode);
            return View(productVendor);
        }

        // GET: ProductVendors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productVendor = await _context.ProductVendors
                .Include(p => p.BusinessEntity)
                .Include(p => p.Product)
                .Include(p => p.UnitMeasureCodeNavigation)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (productVendor == null)
            {
                return NotFound();
            }

            return View(productVendor);
        }

        // POST: ProductVendors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productVendor = await _context.ProductVendors.FindAsync(id);
            if (productVendor != null)
            {
                _context.ProductVendors.Remove(productVendor);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductVendorExists(int id)
        {
            return _context.ProductVendors.Any(e => e.ProductId == id);
        }
    }
}
