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
    public class BusinessEntityAddressesController : Controller
    {
        private readonly AdventureWorks2022Context _context;

        public BusinessEntityAddressesController(AdventureWorks2022Context context)
        {
            _context = context;
        }

        // GET: BusinessEntityAddresses
        public async Task<IActionResult> Index()
        {
            var adventureWorks2022Context = _context.BusinessEntityAddresses.Include(b => b.Address).Include(b => b.AddressType).Include(b => b.BusinessEntity);
            return View(await adventureWorks2022Context.ToListAsync());
        }

        // GET: BusinessEntityAddresses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var businessEntityAddress = await _context.BusinessEntityAddresses
                .Include(b => b.Address)
                .Include(b => b.AddressType)
                .Include(b => b.BusinessEntity)
                .FirstOrDefaultAsync(m => m.BusinessEntityId == id);
            if (businessEntityAddress == null)
            {
                return NotFound();
            }

            return View(businessEntityAddress);
        }

        // GET: BusinessEntityAddresses/Create
        public IActionResult Create()
        {
            ViewData["AddressId"] = new SelectList(_context.Addresses, "AddressId", "AddressId");
            ViewData["AddressTypeId"] = new SelectList(_context.AddressTypes, "AddressTypeId", "AddressTypeId");
            ViewData["BusinessEntityId"] = new SelectList(_context.BusinessEntities, "BusinessEntityId", "BusinessEntityId");
            return View();
        }

        // POST: BusinessEntityAddresses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BusinessEntityId,AddressId,AddressTypeId,Rowguid,ModifiedDate")] BusinessEntityAddress businessEntityAddress)
        {
            if (ModelState.IsValid)
            {
                _context.Add(businessEntityAddress);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AddressId"] = new SelectList(_context.Addresses, "AddressId", "AddressId", businessEntityAddress.AddressId);
            ViewData["AddressTypeId"] = new SelectList(_context.AddressTypes, "AddressTypeId", "AddressTypeId", businessEntityAddress.AddressTypeId);
            ViewData["BusinessEntityId"] = new SelectList(_context.BusinessEntities, "BusinessEntityId", "BusinessEntityId", businessEntityAddress.BusinessEntityId);
            return View(businessEntityAddress);
        }

        // GET: BusinessEntityAddresses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var businessEntityAddress = await _context.BusinessEntityAddresses.FindAsync(id);
            if (businessEntityAddress == null)
            {
                return NotFound();
            }
            ViewData["AddressId"] = new SelectList(_context.Addresses, "AddressId", "AddressId", businessEntityAddress.AddressId);
            ViewData["AddressTypeId"] = new SelectList(_context.AddressTypes, "AddressTypeId", "AddressTypeId", businessEntityAddress.AddressTypeId);
            ViewData["BusinessEntityId"] = new SelectList(_context.BusinessEntities, "BusinessEntityId", "BusinessEntityId", businessEntityAddress.BusinessEntityId);
            return View(businessEntityAddress);
        }

        // POST: BusinessEntityAddresses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BusinessEntityId,AddressId,AddressTypeId,Rowguid,ModifiedDate")] BusinessEntityAddress businessEntityAddress)
        {
            if (id != businessEntityAddress.BusinessEntityId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(businessEntityAddress);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BusinessEntityAddressExists(businessEntityAddress.BusinessEntityId))
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
            ViewData["AddressId"] = new SelectList(_context.Addresses, "AddressId", "AddressId", businessEntityAddress.AddressId);
            ViewData["AddressTypeId"] = new SelectList(_context.AddressTypes, "AddressTypeId", "AddressTypeId", businessEntityAddress.AddressTypeId);
            ViewData["BusinessEntityId"] = new SelectList(_context.BusinessEntities, "BusinessEntityId", "BusinessEntityId", businessEntityAddress.BusinessEntityId);
            return View(businessEntityAddress);
        }

        // GET: BusinessEntityAddresses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var businessEntityAddress = await _context.BusinessEntityAddresses
                .Include(b => b.Address)
                .Include(b => b.AddressType)
                .Include(b => b.BusinessEntity)
                .FirstOrDefaultAsync(m => m.BusinessEntityId == id);
            if (businessEntityAddress == null)
            {
                return NotFound();
            }

            return View(businessEntityAddress);
        }

        // POST: BusinessEntityAddresses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var businessEntityAddress = await _context.BusinessEntityAddresses.FindAsync(id);
            if (businessEntityAddress != null)
            {
                _context.BusinessEntityAddresses.Remove(businessEntityAddress);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BusinessEntityAddressExists(int id)
        {
            return _context.BusinessEntityAddresses.Any(e => e.BusinessEntityId == id);
        }
    }
}
