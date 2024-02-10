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
    public class BusinessEntityContactsController : Controller
    {
        private readonly AdventureWorks2022Context _context;

        public BusinessEntityContactsController(AdventureWorks2022Context context)
        {
            _context = context;
        }

        // GET: BusinessEntityContacts
        public async Task<IActionResult> Index()
        {
            var adventureWorks2022Context = _context.BusinessEntityContacts.Include(b => b.BusinessEntity).Include(b => b.ContactType).Include(b => b.Person);
            return View(await adventureWorks2022Context.ToListAsync());
        }

        // GET: BusinessEntityContacts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var businessEntityContact = await _context.BusinessEntityContacts
                .Include(b => b.BusinessEntity)
                .Include(b => b.ContactType)
                .Include(b => b.Person)
                .FirstOrDefaultAsync(m => m.BusinessEntityId == id);
            if (businessEntityContact == null)
            {
                return NotFound();
            }

            return View(businessEntityContact);
        }

        // GET: BusinessEntityContacts/Create
        public IActionResult Create()
        {
            ViewData["BusinessEntityId"] = new SelectList(_context.BusinessEntities, "BusinessEntityId", "BusinessEntityId");
            ViewData["ContactTypeId"] = new SelectList(_context.ContactTypes, "ContactTypeId", "ContactTypeId");
            ViewData["PersonId"] = new SelectList(_context.People, "BusinessEntityId", "BusinessEntityId");
            return View();
        }

        // POST: BusinessEntityContacts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BusinessEntityId,PersonId,ContactTypeId,Rowguid,ModifiedDate")] BusinessEntityContact businessEntityContact)
        {
            if (ModelState.IsValid)
            {
                _context.Add(businessEntityContact);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BusinessEntityId"] = new SelectList(_context.BusinessEntities, "BusinessEntityId", "BusinessEntityId", businessEntityContact.BusinessEntityId);
            ViewData["ContactTypeId"] = new SelectList(_context.ContactTypes, "ContactTypeId", "ContactTypeId", businessEntityContact.ContactTypeId);
            ViewData["PersonId"] = new SelectList(_context.People, "BusinessEntityId", "BusinessEntityId", businessEntityContact.PersonId);
            return View(businessEntityContact);
        }

        // GET: BusinessEntityContacts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var businessEntityContact = await _context.BusinessEntityContacts.FindAsync(id);
            if (businessEntityContact == null)
            {
                return NotFound();
            }
            ViewData["BusinessEntityId"] = new SelectList(_context.BusinessEntities, "BusinessEntityId", "BusinessEntityId", businessEntityContact.BusinessEntityId);
            ViewData["ContactTypeId"] = new SelectList(_context.ContactTypes, "ContactTypeId", "ContactTypeId", businessEntityContact.ContactTypeId);
            ViewData["PersonId"] = new SelectList(_context.People, "BusinessEntityId", "BusinessEntityId", businessEntityContact.PersonId);
            return View(businessEntityContact);
        }

        // POST: BusinessEntityContacts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BusinessEntityId,PersonId,ContactTypeId,Rowguid,ModifiedDate")] BusinessEntityContact businessEntityContact)
        {
            if (id != businessEntityContact.BusinessEntityId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(businessEntityContact);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BusinessEntityContactExists(businessEntityContact.BusinessEntityId))
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
            ViewData["BusinessEntityId"] = new SelectList(_context.BusinessEntities, "BusinessEntityId", "BusinessEntityId", businessEntityContact.BusinessEntityId);
            ViewData["ContactTypeId"] = new SelectList(_context.ContactTypes, "ContactTypeId", "ContactTypeId", businessEntityContact.ContactTypeId);
            ViewData["PersonId"] = new SelectList(_context.People, "BusinessEntityId", "BusinessEntityId", businessEntityContact.PersonId);
            return View(businessEntityContact);
        }

        // GET: BusinessEntityContacts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var businessEntityContact = await _context.BusinessEntityContacts
                .Include(b => b.BusinessEntity)
                .Include(b => b.ContactType)
                .Include(b => b.Person)
                .FirstOrDefaultAsync(m => m.BusinessEntityId == id);
            if (businessEntityContact == null)
            {
                return NotFound();
            }

            return View(businessEntityContact);
        }

        // POST: BusinessEntityContacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var businessEntityContact = await _context.BusinessEntityContacts.FindAsync(id);
            if (businessEntityContact != null)
            {
                _context.BusinessEntityContacts.Remove(businessEntityContact);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BusinessEntityContactExists(int id)
        {
            return _context.BusinessEntityContacts.Any(e => e.BusinessEntityId == id);
        }
    }
}
