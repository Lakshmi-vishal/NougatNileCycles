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
    public class PersonPhonesController : Controller
    {
        private readonly AdventureWorks2022Context _context;

        public PersonPhonesController(AdventureWorks2022Context context)
        {
            _context = context;
        }

        // GET: PersonPhones
        public async Task<IActionResult> Index()
        {
            var adventureWorks2022Context = _context.PersonPhones.Include(p => p.BusinessEntity).Include(p => p.PhoneNumberType);
            return View(await adventureWorks2022Context.ToListAsync());
        }

        // GET: PersonPhones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personPhone = await _context.PersonPhones
                .Include(p => p.BusinessEntity)
                .Include(p => p.PhoneNumberType)
                .FirstOrDefaultAsync(m => m.BusinessEntityId == id);
            if (personPhone == null)
            {
                return NotFound();
            }

            return View(personPhone);
        }

        // GET: PersonPhones/Create
        public IActionResult Create()
        {
            ViewData["BusinessEntityId"] = new SelectList(_context.People, "BusinessEntityId", "BusinessEntityId");
            ViewData["PhoneNumberTypeId"] = new SelectList(_context.PhoneNumberTypes, "PhoneNumberTypeId", "PhoneNumberTypeId");
            return View();
        }

        // POST: PersonPhones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BusinessEntityId,PhoneNumber,PhoneNumberTypeId,ModifiedDate")] PersonPhone personPhone)
        {
            if (ModelState.IsValid)
            {
                _context.Add(personPhone);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BusinessEntityId"] = new SelectList(_context.People, "BusinessEntityId", "BusinessEntityId", personPhone.BusinessEntityId);
            ViewData["PhoneNumberTypeId"] = new SelectList(_context.PhoneNumberTypes, "PhoneNumberTypeId", "PhoneNumberTypeId", personPhone.PhoneNumberTypeId);
            return View(personPhone);
        }

        // GET: PersonPhones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personPhone = await _context.PersonPhones.FindAsync(id);
            if (personPhone == null)
            {
                return NotFound();
            }
            ViewData["BusinessEntityId"] = new SelectList(_context.People, "BusinessEntityId", "BusinessEntityId", personPhone.BusinessEntityId);
            ViewData["PhoneNumberTypeId"] = new SelectList(_context.PhoneNumberTypes, "PhoneNumberTypeId", "PhoneNumberTypeId", personPhone.PhoneNumberTypeId);
            return View(personPhone);
        }

        // POST: PersonPhones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BusinessEntityId,PhoneNumber,PhoneNumberTypeId,ModifiedDate")] PersonPhone personPhone)
        {
            if (id != personPhone.BusinessEntityId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(personPhone);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonPhoneExists(personPhone.BusinessEntityId))
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
            ViewData["BusinessEntityId"] = new SelectList(_context.People, "BusinessEntityId", "BusinessEntityId", personPhone.BusinessEntityId);
            ViewData["PhoneNumberTypeId"] = new SelectList(_context.PhoneNumberTypes, "PhoneNumberTypeId", "PhoneNumberTypeId", personPhone.PhoneNumberTypeId);
            return View(personPhone);
        }

        // GET: PersonPhones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personPhone = await _context.PersonPhones
                .Include(p => p.BusinessEntity)
                .Include(p => p.PhoneNumberType)
                .FirstOrDefaultAsync(m => m.BusinessEntityId == id);
            if (personPhone == null)
            {
                return NotFound();
            }

            return View(personPhone);
        }

        // POST: PersonPhones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var personPhone = await _context.PersonPhones.FindAsync(id);
            if (personPhone != null)
            {
                _context.PersonPhones.Remove(personPhone);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonPhoneExists(int id)
        {
            return _context.PersonPhones.Any(e => e.BusinessEntityId == id);
        }
    }
}
