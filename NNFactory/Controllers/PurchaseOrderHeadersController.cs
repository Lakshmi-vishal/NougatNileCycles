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
    public class PurchaseOrderHeadersController : Controller
    {
        private readonly AdventureWorks2022Context _context;

        public PurchaseOrderHeadersController(AdventureWorks2022Context context)
        {
            _context = context;
        }

        // GET: PurchaseOrderHeaders
        public async Task<IActionResult> Index()
        {
            var adventureWorks2022Context = _context.PurchaseOrderHeaders.Include(p => p.Employee).Include(p => p.ShipMethod).Include(p => p.Vendor);
            return View(await adventureWorks2022Context.ToListAsync());
        }

        // GET: PurchaseOrderHeaders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchaseOrderHeader = await _context.PurchaseOrderHeaders
                .Include(p => p.Employee)
                .Include(p => p.ShipMethod)
                .Include(p => p.Vendor)
                .FirstOrDefaultAsync(m => m.PurchaseOrderId == id);
            if (purchaseOrderHeader == null)
            {
                return NotFound();
            }

            return View(purchaseOrderHeader);
        }

        // GET: PurchaseOrderHeaders/Create
        public IActionResult Create()
        {
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "BusinessEntityId", "BusinessEntityId");
            ViewData["ShipMethodId"] = new SelectList(_context.ShipMethods, "ShipMethodId", "ShipMethodId");
            ViewData["VendorId"] = new SelectList(_context.Vendors, "BusinessEntityId", "BusinessEntityId");
            return View();
        }

        // POST: PurchaseOrderHeaders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PurchaseOrderId,RevisionNumber,Status,EmployeeId,VendorId,ShipMethodId,OrderDate,ShipDate,SubTotal,TaxAmt,Freight,TotalDue,ModifiedDate")] PurchaseOrderHeader purchaseOrderHeader)
        {
            if (ModelState.IsValid)
            {
                _context.Add(purchaseOrderHeader);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "BusinessEntityId", "BusinessEntityId", purchaseOrderHeader.EmployeeId);
            ViewData["ShipMethodId"] = new SelectList(_context.ShipMethods, "ShipMethodId", "ShipMethodId", purchaseOrderHeader.ShipMethodId);
            ViewData["VendorId"] = new SelectList(_context.Vendors, "BusinessEntityId", "BusinessEntityId", purchaseOrderHeader.VendorId);
            return View(purchaseOrderHeader);
        }

        // GET: PurchaseOrderHeaders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchaseOrderHeader = await _context.PurchaseOrderHeaders.FindAsync(id);
            if (purchaseOrderHeader == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "BusinessEntityId", "BusinessEntityId", purchaseOrderHeader.EmployeeId);
            ViewData["ShipMethodId"] = new SelectList(_context.ShipMethods, "ShipMethodId", "ShipMethodId", purchaseOrderHeader.ShipMethodId);
            ViewData["VendorId"] = new SelectList(_context.Vendors, "BusinessEntityId", "BusinessEntityId", purchaseOrderHeader.VendorId);
            return View(purchaseOrderHeader);
        }

        // POST: PurchaseOrderHeaders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PurchaseOrderId,RevisionNumber,Status,EmployeeId,VendorId,ShipMethodId,OrderDate,ShipDate,SubTotal,TaxAmt,Freight,TotalDue,ModifiedDate")] PurchaseOrderHeader purchaseOrderHeader)
        {
            if (id != purchaseOrderHeader.PurchaseOrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(purchaseOrderHeader);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PurchaseOrderHeaderExists(purchaseOrderHeader.PurchaseOrderId))
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
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "BusinessEntityId", "BusinessEntityId", purchaseOrderHeader.EmployeeId);
            ViewData["ShipMethodId"] = new SelectList(_context.ShipMethods, "ShipMethodId", "ShipMethodId", purchaseOrderHeader.ShipMethodId);
            ViewData["VendorId"] = new SelectList(_context.Vendors, "BusinessEntityId", "BusinessEntityId", purchaseOrderHeader.VendorId);
            return View(purchaseOrderHeader);
        }

        // GET: PurchaseOrderHeaders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchaseOrderHeader = await _context.PurchaseOrderHeaders
                .Include(p => p.Employee)
                .Include(p => p.ShipMethod)
                .Include(p => p.Vendor)
                .FirstOrDefaultAsync(m => m.PurchaseOrderId == id);
            if (purchaseOrderHeader == null)
            {
                return NotFound();
            }

            return View(purchaseOrderHeader);
        }

        // POST: PurchaseOrderHeaders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var purchaseOrderHeader = await _context.PurchaseOrderHeaders.FindAsync(id);
            if (purchaseOrderHeader != null)
            {
                _context.PurchaseOrderHeaders.Remove(purchaseOrderHeader);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PurchaseOrderHeaderExists(int id)
        {
            return _context.PurchaseOrderHeaders.Any(e => e.PurchaseOrderId == id);
        }
    }
}
