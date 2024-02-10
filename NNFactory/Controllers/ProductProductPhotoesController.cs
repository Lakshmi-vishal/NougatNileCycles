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
    public class ProductProductPhotoesController : Controller
    {
        private readonly AdventureWorks2022Context _context;

        public ProductProductPhotoesController(AdventureWorks2022Context context)
        {
            _context = context;
        }

        // GET: ProductProductPhotoes
        public async Task<IActionResult> Index()
        {
            var adventureWorks2022Context = _context.ProductProductPhotos.Include(p => p.Product).Include(p => p.ProductPhoto);
            return View(await adventureWorks2022Context.ToListAsync());
        }

        // GET: ProductProductPhotoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productProductPhoto = await _context.ProductProductPhotos
                .Include(p => p.Product)
                .Include(p => p.ProductPhoto)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (productProductPhoto == null)
            {
                return NotFound();
            }

            return View(productProductPhoto);
        }

        // GET: ProductProductPhotoes/Create
        public IActionResult Create()
        {
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId");
            ViewData["ProductPhotoId"] = new SelectList(_context.ProductPhotos, "ProductPhotoId", "ProductPhotoId");
            return View();
        }

        // POST: ProductProductPhotoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,ProductPhotoId,Primary,ModifiedDate")] ProductProductPhoto productProductPhoto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productProductPhoto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId", productProductPhoto.ProductId);
            ViewData["ProductPhotoId"] = new SelectList(_context.ProductPhotos, "ProductPhotoId", "ProductPhotoId", productProductPhoto.ProductPhotoId);
            return View(productProductPhoto);
        }

        // GET: ProductProductPhotoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productProductPhoto = await _context.ProductProductPhotos.FindAsync(id);
            if (productProductPhoto == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId", productProductPhoto.ProductId);
            ViewData["ProductPhotoId"] = new SelectList(_context.ProductPhotos, "ProductPhotoId", "ProductPhotoId", productProductPhoto.ProductPhotoId);
            return View(productProductPhoto);
        }

        // POST: ProductProductPhotoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,ProductPhotoId,Primary,ModifiedDate")] ProductProductPhoto productProductPhoto)
        {
            if (id != productProductPhoto.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productProductPhoto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductProductPhotoExists(productProductPhoto.ProductId))
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
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId", productProductPhoto.ProductId);
            ViewData["ProductPhotoId"] = new SelectList(_context.ProductPhotos, "ProductPhotoId", "ProductPhotoId", productProductPhoto.ProductPhotoId);
            return View(productProductPhoto);
        }

        // GET: ProductProductPhotoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productProductPhoto = await _context.ProductProductPhotos
                .Include(p => p.Product)
                .Include(p => p.ProductPhoto)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (productProductPhoto == null)
            {
                return NotFound();
            }

            return View(productProductPhoto);
        }

        // POST: ProductProductPhotoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productProductPhoto = await _context.ProductProductPhotos.FindAsync(id);
            if (productProductPhoto != null)
            {
                _context.ProductProductPhotos.Remove(productProductPhoto);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductProductPhotoExists(int id)
        {
            return _context.ProductProductPhotos.Any(e => e.ProductId == id);
        }
    }
}
