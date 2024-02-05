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
    public class SalesTaxRatesController : Controller
    {
        private readonly AdventureWorks2022Context _context;

        public SalesTaxRatesController(AdventureWorks2022Context context)
        {
            _context = context;
        }

        // GET: SalesTaxRates
        public async Task<IActionResult> Index()
        {
            var adventureWorks2022Context = _context.SalesTaxRates.Include(s => s.StateProvince);
            return View(await adventureWorks2022Context.ToListAsync());
        }

        // GET: SalesTaxRates/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salesTaxRate = await _context.SalesTaxRates
                .Include(s => s.StateProvince)
                .FirstOrDefaultAsync(m => m.SalesTaxRateId == id);
            if (salesTaxRate == null)
            {
                return NotFound();
            }

            return View(salesTaxRate);
        }

        // GET: SalesTaxRates/Create
        public IActionResult Create()
        {
            ViewData["StateProvinceId"] = new SelectList(_context.StateProvinces, "StateProvinceId", "StateProvinceId");
            return View();
        }

        // POST: SalesTaxRates/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SalesTaxRateId,StateProvinceId,TaxType,TaxRate,Name,Rowguid,ModifiedDate")] SalesTaxRate salesTaxRate)
        {
            if (ModelState.IsValid)
            {
                _context.Add(salesTaxRate);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["StateProvinceId"] = new SelectList(_context.StateProvinces, "StateProvinceId", "StateProvinceId", salesTaxRate.StateProvinceId);
            return View(salesTaxRate);
        }

        // GET: SalesTaxRates/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salesTaxRate = await _context.SalesTaxRates.FindAsync(id);
            if (salesTaxRate == null)
            {
                return NotFound();
            }
            ViewData["StateProvinceId"] = new SelectList(_context.StateProvinces, "StateProvinceId", "StateProvinceId", salesTaxRate.StateProvinceId);
            return View(salesTaxRate);
        }

        // POST: SalesTaxRates/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SalesTaxRateId,StateProvinceId,TaxType,TaxRate,Name,Rowguid,ModifiedDate")] SalesTaxRate salesTaxRate)
        {
            if (id != salesTaxRate.SalesTaxRateId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(salesTaxRate);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SalesTaxRateExists(salesTaxRate.SalesTaxRateId))
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
            ViewData["StateProvinceId"] = new SelectList(_context.StateProvinces, "StateProvinceId", "StateProvinceId", salesTaxRate.StateProvinceId);
            return View(salesTaxRate);
        }

        // GET: SalesTaxRates/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salesTaxRate = await _context.SalesTaxRates
                .Include(s => s.StateProvince)
                .FirstOrDefaultAsync(m => m.SalesTaxRateId == id);
            if (salesTaxRate == null)
            {
                return NotFound();
            }

            return View(salesTaxRate);
        }

        // POST: SalesTaxRates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var salesTaxRate = await _context.SalesTaxRates.FindAsync(id);
            if (salesTaxRate != null)
            {
                _context.SalesTaxRates.Remove(salesTaxRate);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SalesTaxRateExists(int id)
        {
            return _context.SalesTaxRates.Any(e => e.SalesTaxRateId == id);
        }
    }
}
