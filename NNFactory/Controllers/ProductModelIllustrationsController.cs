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
    public class ProductModelIllustrationsController : Controller
    {
        private readonly AdventureWorks2022Context _context;

        public ProductModelIllustrationsController(AdventureWorks2022Context context)
        {
            _context = context;
        }

        // GET: ProductModelIllustrations
        public async Task<IActionResult> Index()
        {
            var adventureWorks2022Context = _context.ProductModelIllustrations.Include(p => p.Illustration).Include(p => p.ProductModel);
            return View(await adventureWorks2022Context.ToListAsync());
        }

        // GET: ProductModelIllustrations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productModelIllustration = await _context.ProductModelIllustrations
                .Include(p => p.Illustration)
                .Include(p => p.ProductModel)
                .FirstOrDefaultAsync(m => m.ProductModelId == id);
            if (productModelIllustration == null)
            {
                return NotFound();
            }

            return View(productModelIllustration);
        }

        // GET: ProductModelIllustrations/Create
        public IActionResult Create()
        {
            ViewData["IllustrationId"] = new SelectList(_context.Illustrations, "IllustrationId", "IllustrationId");
            ViewData["ProductModelId"] = new SelectList(_context.ProductModels, "ProductModelId", "ProductModelId");
            return View();
        }

        // POST: ProductModelIllustrations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductModelId,IllustrationId,ModifiedDate")] ProductModelIllustration productModelIllustration)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productModelIllustration);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IllustrationId"] = new SelectList(_context.Illustrations, "IllustrationId", "IllustrationId", productModelIllustration.IllustrationId);
            ViewData["ProductModelId"] = new SelectList(_context.ProductModels, "ProductModelId", "ProductModelId", productModelIllustration.ProductModelId);
            return View(productModelIllustration);
        }

        // GET: ProductModelIllustrations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productModelIllustration = await _context.ProductModelIllustrations.FindAsync(id);
            if (productModelIllustration == null)
            {
                return NotFound();
            }
            ViewData["IllustrationId"] = new SelectList(_context.Illustrations, "IllustrationId", "IllustrationId", productModelIllustration.IllustrationId);
            ViewData["ProductModelId"] = new SelectList(_context.ProductModels, "ProductModelId", "ProductModelId", productModelIllustration.ProductModelId);
            return View(productModelIllustration);
        }

        // POST: ProductModelIllustrations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductModelId,IllustrationId,ModifiedDate")] ProductModelIllustration productModelIllustration)
        {
            if (id != productModelIllustration.ProductModelId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productModelIllustration);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductModelIllustrationExists(productModelIllustration.ProductModelId))
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
            ViewData["IllustrationId"] = new SelectList(_context.Illustrations, "IllustrationId", "IllustrationId", productModelIllustration.IllustrationId);
            ViewData["ProductModelId"] = new SelectList(_context.ProductModels, "ProductModelId", "ProductModelId", productModelIllustration.ProductModelId);
            return View(productModelIllustration);
        }

        // GET: ProductModelIllustrations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productModelIllustration = await _context.ProductModelIllustrations
                .Include(p => p.Illustration)
                .Include(p => p.ProductModel)
                .FirstOrDefaultAsync(m => m.ProductModelId == id);
            if (productModelIllustration == null)
            {
                return NotFound();
            }

            return View(productModelIllustration);
        }

        // POST: ProductModelIllustrations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productModelIllustration = await _context.ProductModelIllustrations.FindAsync(id);
            if (productModelIllustration != null)
            {
                _context.ProductModelIllustrations.Remove(productModelIllustration);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductModelIllustrationExists(int id)
        {
            return _context.ProductModelIllustrations.Any(e => e.ProductModelId == id);
        }
    }
}
