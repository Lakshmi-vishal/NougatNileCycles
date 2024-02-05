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
    public class SpecialOfferProductsController : Controller
    {
        private readonly AdventureWorks2022Context _context;

        public SpecialOfferProductsController(AdventureWorks2022Context context)
        {
            _context = context;
        }

        // GET: SpecialOfferProducts
        public async Task<IActionResult> Index()
        {
            var adventureWorks2022Context = _context.SpecialOfferProducts.Include(s => s.Product).Include(s => s.SpecialOffer);
            return View(await adventureWorks2022Context.ToListAsync());
        }

        // GET: SpecialOfferProducts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var specialOfferProduct = await _context.SpecialOfferProducts
                .Include(s => s.Product)
                .Include(s => s.SpecialOffer)
                .FirstOrDefaultAsync(m => m.SpecialOfferId == id);
            if (specialOfferProduct == null)
            {
                return NotFound();
            }

            return View(specialOfferProduct);
        }

        // GET: SpecialOfferProducts/Create
        public IActionResult Create()
        {
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId");
            ViewData["SpecialOfferId"] = new SelectList(_context.SpecialOffers, "SpecialOfferId", "SpecialOfferId");
            return View();
        }

        // POST: SpecialOfferProducts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SpecialOfferId,ProductId,Rowguid,ModifiedDate")] SpecialOfferProduct specialOfferProduct)
        {
            if (ModelState.IsValid)
            {
                _context.Add(specialOfferProduct);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId", specialOfferProduct.ProductId);
            ViewData["SpecialOfferId"] = new SelectList(_context.SpecialOffers, "SpecialOfferId", "SpecialOfferId", specialOfferProduct.SpecialOfferId);
            return View(specialOfferProduct);
        }

        // GET: SpecialOfferProducts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var specialOfferProduct = await _context.SpecialOfferProducts.FindAsync(id);
            if (specialOfferProduct == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId", specialOfferProduct.ProductId);
            ViewData["SpecialOfferId"] = new SelectList(_context.SpecialOffers, "SpecialOfferId", "SpecialOfferId", specialOfferProduct.SpecialOfferId);
            return View(specialOfferProduct);
        }

        // POST: SpecialOfferProducts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SpecialOfferId,ProductId,Rowguid,ModifiedDate")] SpecialOfferProduct specialOfferProduct)
        {
            if (id != specialOfferProduct.SpecialOfferId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(specialOfferProduct);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SpecialOfferProductExists(specialOfferProduct.SpecialOfferId))
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
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId", specialOfferProduct.ProductId);
            ViewData["SpecialOfferId"] = new SelectList(_context.SpecialOffers, "SpecialOfferId", "SpecialOfferId", specialOfferProduct.SpecialOfferId);
            return View(specialOfferProduct);
        }

        // GET: SpecialOfferProducts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var specialOfferProduct = await _context.SpecialOfferProducts
                .Include(s => s.Product)
                .Include(s => s.SpecialOffer)
                .FirstOrDefaultAsync(m => m.SpecialOfferId == id);
            if (specialOfferProduct == null)
            {
                return NotFound();
            }

            return View(specialOfferProduct);
        }

        // POST: SpecialOfferProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var specialOfferProduct = await _context.SpecialOfferProducts.FindAsync(id);
            if (specialOfferProduct != null)
            {
                _context.SpecialOfferProducts.Remove(specialOfferProduct);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SpecialOfferProductExists(int id)
        {
            return _context.SpecialOfferProducts.Any(e => e.SpecialOfferId == id);
        }
    }
}
