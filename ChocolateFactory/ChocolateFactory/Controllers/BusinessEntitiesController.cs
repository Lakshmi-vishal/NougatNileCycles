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
    public class BusinessEntitiesController : Controller
    {
        private readonly AdventureWorks2022Context _context;

        public BusinessEntitiesController(AdventureWorks2022Context context)
        {
            _context = context;
        }

        // GET: BusinessEntities
        public async Task<IActionResult> Index()
        {
            return View(await _context.BusinessEntities.ToListAsync());
        }

        // GET: BusinessEntities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var businessEntity = await _context.BusinessEntities
                .FirstOrDefaultAsync(m => m.BusinessEntityId == id);
            if (businessEntity == null)
            {
                return NotFound();
            }

            return View(businessEntity);
        }

        // GET: BusinessEntities/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BusinessEntities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BusinessEntityId,Rowguid,ModifiedDate")] BusinessEntity businessEntity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(businessEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(businessEntity);
        }

        // GET: BusinessEntities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var businessEntity = await _context.BusinessEntities.FindAsync(id);
            if (businessEntity == null)
            {
                return NotFound();
            }
            return View(businessEntity);
        }

        // POST: BusinessEntities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BusinessEntityId,Rowguid,ModifiedDate")] BusinessEntity businessEntity)
        {
            if (id != businessEntity.BusinessEntityId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(businessEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BusinessEntityExists(businessEntity.BusinessEntityId))
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
            return View(businessEntity);
        }

        // GET: BusinessEntities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var businessEntity = await _context.BusinessEntities
                .FirstOrDefaultAsync(m => m.BusinessEntityId == id);
            if (businessEntity == null)
            {
                return NotFound();
            }

            return View(businessEntity);
        }

        // POST: BusinessEntities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var businessEntity = await _context.BusinessEntities.FindAsync(id);
            if (businessEntity != null)
            {
                _context.BusinessEntities.Remove(businessEntity);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BusinessEntityExists(int id)
        {
            return _context.BusinessEntities.Any(e => e.BusinessEntityId == id);
        }
    }
}
