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
    public class SalesOrderHeadersController : Controller
    {
        private readonly AdventureWorks2022Context _context;

        public SalesOrderHeadersController(AdventureWorks2022Context context)
        {
            _context = context;
        }

        // GET: SalesOrderHeaders
        public async Task<IActionResult> Index()
        {
            var adventureWorks2022Context = _context.SalesOrderHeaders.Include(s => s.BillToAddress).Include(s => s.CreditCard).Include(s => s.CurrencyRate).Include(s => s.Customer).Include(s => s.SalesPerson).Include(s => s.ShipMethod).Include(s => s.ShipToAddress).Include(s => s.Territory);
            return View(await adventureWorks2022Context.ToListAsync());
        }

        // GET: SalesOrderHeaders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salesOrderHeader = await _context.SalesOrderHeaders
                .Include(s => s.BillToAddress)
                .Include(s => s.CreditCard)
                .Include(s => s.CurrencyRate)
                .Include(s => s.Customer)
                .Include(s => s.SalesPerson)
                .Include(s => s.ShipMethod)
                .Include(s => s.ShipToAddress)
                .Include(s => s.Territory)
                .FirstOrDefaultAsync(m => m.SalesOrderId == id);
            if (salesOrderHeader == null)
            {
                return NotFound();
            }

            return View(salesOrderHeader);
        }

        // GET: SalesOrderHeaders/Create
        public IActionResult Create()
        {
            ViewData["BillToAddressId"] = new SelectList(_context.Addresses, "AddressId", "AddressId");
            ViewData["CreditCardId"] = new SelectList(_context.CreditCards, "CreditCardId", "CreditCardId");
            ViewData["CurrencyRateId"] = new SelectList(_context.CurrencyRates, "CurrencyRateId", "CurrencyRateId");
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerId");
            ViewData["SalesPersonId"] = new SelectList(_context.SalesPeople, "BusinessEntityId", "BusinessEntityId");
            ViewData["ShipMethodId"] = new SelectList(_context.ShipMethods, "ShipMethodId", "ShipMethodId");
            ViewData["ShipToAddressId"] = new SelectList(_context.Addresses, "AddressId", "AddressId");
            ViewData["TerritoryId"] = new SelectList(_context.SalesTerritories, "TerritoryId", "TerritoryId");
            return View();
        }

        // POST: SalesOrderHeaders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SalesOrderId,RevisionNumber,OrderDate,DueDate,ShipDate,Status,OnlineOrderFlag,SalesOrderNumber,PurchaseOrderNumber,AccountNumber,CustomerId,SalesPersonId,TerritoryId,BillToAddressId,ShipToAddressId,ShipMethodId,CreditCardId,CreditCardApprovalCode,CurrencyRateId,SubTotal,TaxAmt,Freight,TotalDue,Comment,Rowguid,ModifiedDate")] SalesOrderHeader salesOrderHeader)
        {
            if (ModelState.IsValid)
            {
                _context.Add(salesOrderHeader);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BillToAddressId"] = new SelectList(_context.Addresses, "AddressId", "AddressId", salesOrderHeader.BillToAddressId);
            ViewData["CreditCardId"] = new SelectList(_context.CreditCards, "CreditCardId", "CreditCardId", salesOrderHeader.CreditCardId);
            ViewData["CurrencyRateId"] = new SelectList(_context.CurrencyRates, "CurrencyRateId", "CurrencyRateId", salesOrderHeader.CurrencyRateId);
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerId", salesOrderHeader.CustomerId);
            ViewData["SalesPersonId"] = new SelectList(_context.SalesPeople, "BusinessEntityId", "BusinessEntityId", salesOrderHeader.SalesPersonId);
            ViewData["ShipMethodId"] = new SelectList(_context.ShipMethods, "ShipMethodId", "ShipMethodId", salesOrderHeader.ShipMethodId);
            ViewData["ShipToAddressId"] = new SelectList(_context.Addresses, "AddressId", "AddressId", salesOrderHeader.ShipToAddressId);
            ViewData["TerritoryId"] = new SelectList(_context.SalesTerritories, "TerritoryId", "TerritoryId", salesOrderHeader.TerritoryId);
            return View(salesOrderHeader);
        }

        // GET: SalesOrderHeaders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salesOrderHeader = await _context.SalesOrderHeaders.FindAsync(id);
            if (salesOrderHeader == null)
            {
                return NotFound();
            }
            ViewData["BillToAddressId"] = new SelectList(_context.Addresses, "AddressId", "AddressId", salesOrderHeader.BillToAddressId);
            ViewData["CreditCardId"] = new SelectList(_context.CreditCards, "CreditCardId", "CreditCardId", salesOrderHeader.CreditCardId);
            ViewData["CurrencyRateId"] = new SelectList(_context.CurrencyRates, "CurrencyRateId", "CurrencyRateId", salesOrderHeader.CurrencyRateId);
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerId", salesOrderHeader.CustomerId);
            ViewData["SalesPersonId"] = new SelectList(_context.SalesPeople, "BusinessEntityId", "BusinessEntityId", salesOrderHeader.SalesPersonId);
            ViewData["ShipMethodId"] = new SelectList(_context.ShipMethods, "ShipMethodId", "ShipMethodId", salesOrderHeader.ShipMethodId);
            ViewData["ShipToAddressId"] = new SelectList(_context.Addresses, "AddressId", "AddressId", salesOrderHeader.ShipToAddressId);
            ViewData["TerritoryId"] = new SelectList(_context.SalesTerritories, "TerritoryId", "TerritoryId", salesOrderHeader.TerritoryId);
            return View(salesOrderHeader);
        }

        // POST: SalesOrderHeaders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SalesOrderId,RevisionNumber,OrderDate,DueDate,ShipDate,Status,OnlineOrderFlag,SalesOrderNumber,PurchaseOrderNumber,AccountNumber,CustomerId,SalesPersonId,TerritoryId,BillToAddressId,ShipToAddressId,ShipMethodId,CreditCardId,CreditCardApprovalCode,CurrencyRateId,SubTotal,TaxAmt,Freight,TotalDue,Comment,Rowguid,ModifiedDate")] SalesOrderHeader salesOrderHeader)
        {
            if (id != salesOrderHeader.SalesOrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(salesOrderHeader);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SalesOrderHeaderExists(salesOrderHeader.SalesOrderId))
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
            ViewData["BillToAddressId"] = new SelectList(_context.Addresses, "AddressId", "AddressId", salesOrderHeader.BillToAddressId);
            ViewData["CreditCardId"] = new SelectList(_context.CreditCards, "CreditCardId", "CreditCardId", salesOrderHeader.CreditCardId);
            ViewData["CurrencyRateId"] = new SelectList(_context.CurrencyRates, "CurrencyRateId", "CurrencyRateId", salesOrderHeader.CurrencyRateId);
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerId", salesOrderHeader.CustomerId);
            ViewData["SalesPersonId"] = new SelectList(_context.SalesPeople, "BusinessEntityId", "BusinessEntityId", salesOrderHeader.SalesPersonId);
            ViewData["ShipMethodId"] = new SelectList(_context.ShipMethods, "ShipMethodId", "ShipMethodId", salesOrderHeader.ShipMethodId);
            ViewData["ShipToAddressId"] = new SelectList(_context.Addresses, "AddressId", "AddressId", salesOrderHeader.ShipToAddressId);
            ViewData["TerritoryId"] = new SelectList(_context.SalesTerritories, "TerritoryId", "TerritoryId", salesOrderHeader.TerritoryId);
            return View(salesOrderHeader);
        }

        // GET: SalesOrderHeaders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salesOrderHeader = await _context.SalesOrderHeaders
                .Include(s => s.BillToAddress)
                .Include(s => s.CreditCard)
                .Include(s => s.CurrencyRate)
                .Include(s => s.Customer)
                .Include(s => s.SalesPerson)
                .Include(s => s.ShipMethod)
                .Include(s => s.ShipToAddress)
                .Include(s => s.Territory)
                .FirstOrDefaultAsync(m => m.SalesOrderId == id);
            if (salesOrderHeader == null)
            {
                return NotFound();
            }

            return View(salesOrderHeader);
        }

        // POST: SalesOrderHeaders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var salesOrderHeader = await _context.SalesOrderHeaders.FindAsync(id);
            if (salesOrderHeader != null)
            {
                _context.SalesOrderHeaders.Remove(salesOrderHeader);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SalesOrderHeaderExists(int id)
        {
            return _context.SalesOrderHeaders.Any(e => e.SalesOrderId == id);
        }
    }
}
