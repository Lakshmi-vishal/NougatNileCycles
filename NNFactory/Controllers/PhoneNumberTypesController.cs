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
    public class PhoneNumberTypesController : Controller
    {
        private readonly AdventureWorks2022Context _context;

        public PhoneNumberTypesController(AdventureWorks2022Context context)
        {
            _context = context;
        }

        // GET: PhoneNumberTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.PhoneNumberTypes.ToListAsync());
        }

        // GET: PhoneNumberTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phoneNumberType = await _context.PhoneNumberTypes
                .FirstOrDefaultAsync(m => m.PhoneNumberTypeId == id);
            if (phoneNumberType == null)
            {
                return NotFound();
            }

            return View(phoneNumberType);
        }

        // GET: PhoneNumberTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PhoneNumberTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PhoneNumberTypeId,Name,ModifiedDate")] PhoneNumberType phoneNumberType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(phoneNumberType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(phoneNumberType);
        }

        // GET: PhoneNumberTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phoneNumberType = await _context.PhoneNumberTypes.FindAsync(id);
            if (phoneNumberType == null)
            {
                return NotFound();
            }
            return View(phoneNumberType);
        }

        // POST: PhoneNumberTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PhoneNumberTypeId,Name,ModifiedDate")] PhoneNumberType phoneNumberType)
        {
            if (id != phoneNumberType.PhoneNumberTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(phoneNumberType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PhoneNumberTypeExists(phoneNumberType.PhoneNumberTypeId))
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
            return View(phoneNumberType);
        }

        // GET: PhoneNumberTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phoneNumberType = await _context.PhoneNumberTypes
                .FirstOrDefaultAsync(m => m.PhoneNumberTypeId == id);
            if (phoneNumberType == null)
            {
                return NotFound();
            }

            return View(phoneNumberType);
        }

        // POST: PhoneNumberTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var phoneNumberType = await _context.PhoneNumberTypes.FindAsync(id);
            if (phoneNumberType != null)
            {
                _context.PhoneNumberTypes.Remove(phoneNumberType);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PhoneNumberTypeExists(int id)
        {
            return _context.PhoneNumberTypes.Any(e => e.PhoneNumberTypeId == id);
        }
    }
}
