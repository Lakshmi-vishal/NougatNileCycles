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
    public class ProductInventoriesController : Controller
    {
        private readonly AdventureWorks2022Context _context;

        public ProductInventoriesController(AdventureWorks2022Context context)
        {
            _context = context;
        }

        // GET: ProductInventories
        public async Task<IActionResult> Index()
        {
            var adventureWorks2022Context = _context.ProductInventories.Include(p => p.Location).Include(p => p.Product);
            return View(await adventureWorks2022Context.ToListAsync());
        }

        // GET: ProductInventories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productInventory = await _context.ProductInventories
                .Include(p => p.Location)
                .Include(p => p.Product)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (productInventory == null)
            {
                return NotFound();
            }

            return View(productInventory);
        }

        // GET: ProductInventories/Create
        public IActionResult Create()
        {
            ViewData["LocationId"] = new SelectList(_context.Locations, "LocationId", "LocationId");
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId");
            return View();
        }

        // POST: ProductInventories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,LocationId,Shelf,Bin,Quantity,Rowguid,ModifiedDate")] ProductInventory productInventory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productInventory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LocationId"] = new SelectList(_context.Locations, "LocationId", "LocationId", productInventory.LocationId);
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId", productInventory.ProductId);
            return View(productInventory);
        }

        // GET: ProductInventories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productInventory = await _context.ProductInventories.FindAsync(id);
            if (productInventory == null)
            {
                return NotFound();
            }
            ViewData["LocationId"] = new SelectList(_context.Locations, "LocationId", "LocationId", productInventory.LocationId);
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId", productInventory.ProductId);
            return View(productInventory);
        }

        // POST: ProductInventories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,LocationId,Shelf,Bin,Quantity,Rowguid,ModifiedDate")] ProductInventory productInventory)
        {
            if (id != productInventory.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productInventory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductInventoryExists(productInventory.ProductId))
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
            ViewData["LocationId"] = new SelectList(_context.Locations, "LocationId", "LocationId", productInventory.LocationId);
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId", productInventory.ProductId);
            return View(productInventory);
        }

        // GET: ProductInventories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productInventory = await _context.ProductInventories
                .Include(p => p.Location)
                .Include(p => p.Product)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (productInventory == null)
            {
                return NotFound();
            }

            return View(productInventory);
        }

        // POST: ProductInventories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productInventory = await _context.ProductInventories.FindAsync(id);
            if (productInventory != null)
            {
                _context.ProductInventories.Remove(productInventory);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductInventoryExists(int id)
        {
            return _context.ProductInventories.Any(e => e.ProductId == id);
        }
    }
}
