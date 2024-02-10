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
    public class ProductModelProductDescriptionCulturesController : Controller
    {
        private readonly AdventureWorks2022Context _context;

        public ProductModelProductDescriptionCulturesController(AdventureWorks2022Context context)
        {
            _context = context;
        }

        // GET: ProductModelProductDescriptionCultures
        public async Task<IActionResult> Index()
        {
            var adventureWorks2022Context = _context.ProductModelProductDescriptionCultures.Include(p => p.Culture).Include(p => p.ProductDescription).Include(p => p.ProductModel);
            return View(await adventureWorks2022Context.ToListAsync());
        }

        // GET: ProductModelProductDescriptionCultures/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productModelProductDescriptionCulture = await _context.ProductModelProductDescriptionCultures
                .Include(p => p.Culture)
                .Include(p => p.ProductDescription)
                .Include(p => p.ProductModel)
                .FirstOrDefaultAsync(m => m.ProductModelId == id);
            if (productModelProductDescriptionCulture == null)
            {
                return NotFound();
            }

            return View(productModelProductDescriptionCulture);
        }

        // GET: ProductModelProductDescriptionCultures/Create
        public IActionResult Create()
        {
            ViewData["CultureId"] = new SelectList(_context.Cultures, "CultureId", "CultureId");
            ViewData["ProductDescriptionId"] = new SelectList(_context.ProductDescriptions, "ProductDescriptionId", "ProductDescriptionId");
            ViewData["ProductModelId"] = new SelectList(_context.ProductModels, "ProductModelId", "ProductModelId");
            return View();
        }

        // POST: ProductModelProductDescriptionCultures/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductModelId,ProductDescriptionId,CultureId,ModifiedDate")] ProductModelProductDescriptionCulture productModelProductDescriptionCulture)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productModelProductDescriptionCulture);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CultureId"] = new SelectList(_context.Cultures, "CultureId", "CultureId", productModelProductDescriptionCulture.CultureId);
            ViewData["ProductDescriptionId"] = new SelectList(_context.ProductDescriptions, "ProductDescriptionId", "ProductDescriptionId", productModelProductDescriptionCulture.ProductDescriptionId);
            ViewData["ProductModelId"] = new SelectList(_context.ProductModels, "ProductModelId", "ProductModelId", productModelProductDescriptionCulture.ProductModelId);
            return View(productModelProductDescriptionCulture);
        }

        // GET: ProductModelProductDescriptionCultures/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productModelProductDescriptionCulture = await _context.ProductModelProductDescriptionCultures.FindAsync(id);
            if (productModelProductDescriptionCulture == null)
            {
                return NotFound();
            }
            ViewData["CultureId"] = new SelectList(_context.Cultures, "CultureId", "CultureId", productModelProductDescriptionCulture.CultureId);
            ViewData["ProductDescriptionId"] = new SelectList(_context.ProductDescriptions, "ProductDescriptionId", "ProductDescriptionId", productModelProductDescriptionCulture.ProductDescriptionId);
            ViewData["ProductModelId"] = new SelectList(_context.ProductModels, "ProductModelId", "ProductModelId", productModelProductDescriptionCulture.ProductModelId);
            return View(productModelProductDescriptionCulture);
        }

        // POST: ProductModelProductDescriptionCultures/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductModelId,ProductDescriptionId,CultureId,ModifiedDate")] ProductModelProductDescriptionCulture productModelProductDescriptionCulture)
        {
            if (id != productModelProductDescriptionCulture.ProductModelId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productModelProductDescriptionCulture);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductModelProductDescriptionCultureExists(productModelProductDescriptionCulture.ProductModelId))
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
            ViewData["CultureId"] = new SelectList(_context.Cultures, "CultureId", "CultureId", productModelProductDescriptionCulture.CultureId);
            ViewData["ProductDescriptionId"] = new SelectList(_context.ProductDescriptions, "ProductDescriptionId", "ProductDescriptionId", productModelProductDescriptionCulture.ProductDescriptionId);
            ViewData["ProductModelId"] = new SelectList(_context.ProductModels, "ProductModelId", "ProductModelId", productModelProductDescriptionCulture.ProductModelId);
            return View(productModelProductDescriptionCulture);
        }

        // GET: ProductModelProductDescriptionCultures/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productModelProductDescriptionCulture = await _context.ProductModelProductDescriptionCultures
                .Include(p => p.Culture)
                .Include(p => p.ProductDescription)
                .Include(p => p.ProductModel)
                .FirstOrDefaultAsync(m => m.ProductModelId == id);
            if (productModelProductDescriptionCulture == null)
            {
                return NotFound();
            }

            return View(productModelProductDescriptionCulture);
        }

        // POST: ProductModelProductDescriptionCultures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productModelProductDescriptionCulture = await _context.ProductModelProductDescriptionCultures.FindAsync(id);
            if (productModelProductDescriptionCulture != null)
            {
                _context.ProductModelProductDescriptionCultures.Remove(productModelProductDescriptionCulture);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductModelProductDescriptionCultureExists(int id)
        {
            return _context.ProductModelProductDescriptionCultures.Any(e => e.ProductModelId == id);
        }
    }
}
