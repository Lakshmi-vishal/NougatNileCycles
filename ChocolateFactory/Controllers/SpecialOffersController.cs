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
    public class SpecialOffersController : Controller
    {
        private readonly AdventureWorks2022Context _context;

        public SpecialOffersController(AdventureWorks2022Context context)
        {
            _context = context;
        }

        // GET: SpecialOffers
        public async Task<IActionResult> Index()
        {
            return View(await _context.SpecialOffers.ToListAsync());
        }

        // GET: SpecialOffers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var specialOffer = await _context.SpecialOffers
                .FirstOrDefaultAsync(m => m.SpecialOfferId == id);
            if (specialOffer == null)
            {
                return NotFound();
            }

            return View(specialOffer);
        }

        // GET: SpecialOffers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SpecialOffers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SpecialOfferId,Description,DiscountPct,Type,Category,StartDate,EndDate,MinQty,MaxQty,Rowguid,ModifiedDate")] SpecialOffer specialOffer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(specialOffer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(specialOffer);
        }

        // GET: SpecialOffers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var specialOffer = await _context.SpecialOffers.FindAsync(id);
            if (specialOffer == null)
            {
                return NotFound();
            }
            return View(specialOffer);
        }

        // POST: SpecialOffers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SpecialOfferId,Description,DiscountPct,Type,Category,StartDate,EndDate,MinQty,MaxQty,Rowguid,ModifiedDate")] SpecialOffer specialOffer)
        {
            if (id != specialOffer.SpecialOfferId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(specialOffer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SpecialOfferExists(specialOffer.SpecialOfferId))
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
            return View(specialOffer);
        }

        // GET: SpecialOffers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var specialOffer = await _context.SpecialOffers
                .FirstOrDefaultAsync(m => m.SpecialOfferId == id);
            if (specialOffer == null)
            {
                return NotFound();
            }

            return View(specialOffer);
        }

        // POST: SpecialOffers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var specialOffer = await _context.SpecialOffers.FindAsync(id);
            if (specialOffer != null)
            {
                _context.SpecialOffers.Remove(specialOffer);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SpecialOfferExists(int id)
        {
            return _context.SpecialOffers.Any(e => e.SpecialOfferId == id);
        }
    }
}
