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
    public class BillOfMaterialsController : Controller
    {
        private readonly AdventureWorks2022Context _context;

        public BillOfMaterialsController(AdventureWorks2022Context context)
        {
            _context = context;
        }

        // GET: BillOfMaterials
        public async Task<IActionResult> Index()
        {
            var adventureWorks2022Context = _context.BillOfMaterials.Include(b => b.Component).Include(b => b.ProductAssembly).Include(b => b.UnitMeasureCodeNavigation);
            return View(await adventureWorks2022Context.ToListAsync());
        }

        // GET: BillOfMaterials/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var billOfMaterial = await _context.BillOfMaterials
                .Include(b => b.Component)
                .Include(b => b.ProductAssembly)
                .Include(b => b.UnitMeasureCodeNavigation)
                .FirstOrDefaultAsync(m => m.BillOfMaterialsId == id);
            if (billOfMaterial == null)
            {
                return NotFound();
            }

            return View(billOfMaterial);
        }

        // GET: BillOfMaterials/Create
        public IActionResult Create()
        {
            ViewData["ComponentId"] = new SelectList(_context.Products, "ProductId", "ProductId");
            ViewData["ProductAssemblyId"] = new SelectList(_context.Products, "ProductId", "ProductId");
            ViewData["UnitMeasureCode"] = new SelectList(_context.UnitMeasures, "UnitMeasureCode", "UnitMeasureCode");
            return View();
        }

        // POST: BillOfMaterials/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BillOfMaterialsId,ProductAssemblyId,ComponentId,StartDate,EndDate,UnitMeasureCode,Bomlevel,PerAssemblyQty,ModifiedDate")] BillOfMaterial billOfMaterial)
        {
            if (ModelState.IsValid)
            {
                _context.Add(billOfMaterial);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ComponentId"] = new SelectList(_context.Products, "ProductId", "ProductId", billOfMaterial.ComponentId);
            ViewData["ProductAssemblyId"] = new SelectList(_context.Products, "ProductId", "ProductId", billOfMaterial.ProductAssemblyId);
            ViewData["UnitMeasureCode"] = new SelectList(_context.UnitMeasures, "UnitMeasureCode", "UnitMeasureCode", billOfMaterial.UnitMeasureCode);
            return View(billOfMaterial);
        }

        // GET: BillOfMaterials/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var billOfMaterial = await _context.BillOfMaterials.FindAsync(id);
            if (billOfMaterial == null)
            {
                return NotFound();
            }
            ViewData["ComponentId"] = new SelectList(_context.Products, "ProductId", "ProductId", billOfMaterial.ComponentId);
            ViewData["ProductAssemblyId"] = new SelectList(_context.Products, "ProductId", "ProductId", billOfMaterial.ProductAssemblyId);
            ViewData["UnitMeasureCode"] = new SelectList(_context.UnitMeasures, "UnitMeasureCode", "UnitMeasureCode", billOfMaterial.UnitMeasureCode);
            return View(billOfMaterial);
        }

        // POST: BillOfMaterials/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BillOfMaterialsId,ProductAssemblyId,ComponentId,StartDate,EndDate,UnitMeasureCode,Bomlevel,PerAssemblyQty,ModifiedDate")] BillOfMaterial billOfMaterial)
        {
            if (id != billOfMaterial.BillOfMaterialsId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(billOfMaterial);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BillOfMaterialExists(billOfMaterial.BillOfMaterialsId))
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
            ViewData["ComponentId"] = new SelectList(_context.Products, "ProductId", "ProductId", billOfMaterial.ComponentId);
            ViewData["ProductAssemblyId"] = new SelectList(_context.Products, "ProductId", "ProductId", billOfMaterial.ProductAssemblyId);
            ViewData["UnitMeasureCode"] = new SelectList(_context.UnitMeasures, "UnitMeasureCode", "UnitMeasureCode", billOfMaterial.UnitMeasureCode);
            return View(billOfMaterial);
        }

        // GET: BillOfMaterials/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var billOfMaterial = await _context.BillOfMaterials
                .Include(b => b.Component)
                .Include(b => b.ProductAssembly)
                .Include(b => b.UnitMeasureCodeNavigation)
                .FirstOrDefaultAsync(m => m.BillOfMaterialsId == id);
            if (billOfMaterial == null)
            {
                return NotFound();
            }

            return View(billOfMaterial);
        }

        // POST: BillOfMaterials/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var billOfMaterial = await _context.BillOfMaterials.FindAsync(id);
            if (billOfMaterial != null)
            {
                _context.BillOfMaterials.Remove(billOfMaterial);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BillOfMaterialExists(int id)
        {
            return _context.BillOfMaterials.Any(e => e.BillOfMaterialsId == id);
        }
    }
}
