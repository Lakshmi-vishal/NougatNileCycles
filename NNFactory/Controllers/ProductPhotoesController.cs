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
    public class ProductPhotoesController : Controller
    {
        private readonly AdventureWorks2022Context _context;

        public ProductPhotoesController(AdventureWorks2022Context context)
        {
            _context = context;
        }

        // GET: ProductPhotoes
        public async Task<IActionResult> Index()
        {
            return View(await _context.ProductPhotos.ToListAsync());
        }

        // GET: ProductPhotoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productPhoto = await _context.ProductPhotos
                .FirstOrDefaultAsync(m => m.ProductPhotoId == id);
            if (productPhoto == null)
            {
                return NotFound();
            }

            return View(productPhoto);
        }

        // GET: ProductPhotoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProductPhotoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductPhotoId,ThumbNailPhoto,ThumbnailPhotoFileName,LargePhoto,LargePhotoFileName,ModifiedDate")] ProductPhoto productPhoto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productPhoto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(productPhoto);
        }

        // GET: ProductPhotoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productPhoto = await _context.ProductPhotos.FindAsync(id);
            if (productPhoto == null)
            {
                return NotFound();
            }
            return View(productPhoto);
        }

        // POST: ProductPhotoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductPhotoId,ThumbNailPhoto,ThumbnailPhotoFileName,LargePhoto,LargePhotoFileName,ModifiedDate")] ProductPhoto productPhoto)
        {
            if (id != productPhoto.ProductPhotoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productPhoto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductPhotoExists(productPhoto.ProductPhotoId))
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
            return View(productPhoto);
        }

        // GET: ProductPhotoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productPhoto = await _context.ProductPhotos
                .FirstOrDefaultAsync(m => m.ProductPhotoId == id);
            if (productPhoto == null)
            {
                return NotFound();
            }

            return View(productPhoto);
        }

        // POST: ProductPhotoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productPhoto = await _context.ProductPhotos.FindAsync(id);
            if (productPhoto != null)
            {
                _context.ProductPhotos.Remove(productPhoto);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductPhotoExists(int id)
        {
            return _context.ProductPhotos.Any(e => e.ProductPhotoId == id);
        }
    }
}
