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
    public class ShipMethodsController : Controller
    {
        private readonly AdventureWorks2022Context _context;

        public ShipMethodsController(AdventureWorks2022Context context)
        {
            _context = context;
        }

        // GET: ShipMethods
        public async Task<IActionResult> Index()
        {
            return View(await _context.ShipMethods.ToListAsync());
        }

        // GET: ShipMethods/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shipMethod = await _context.ShipMethods
                .FirstOrDefaultAsync(m => m.ShipMethodId == id);
            if (shipMethod == null)
            {
                return NotFound();
            }

            return View(shipMethod);
        }

        // GET: ShipMethods/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ShipMethods/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ShipMethodId,Name,ShipBase,ShipRate,Rowguid,ModifiedDate")] ShipMethod shipMethod)
        {
            if (ModelState.IsValid)
            {
                _context.Add(shipMethod);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(shipMethod);
        }

        // GET: ShipMethods/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shipMethod = await _context.ShipMethods.FindAsync(id);
            if (shipMethod == null)
            {
                return NotFound();
            }
            return View(shipMethod);
        }

        // POST: ShipMethods/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ShipMethodId,Name,ShipBase,ShipRate,Rowguid,ModifiedDate")] ShipMethod shipMethod)
        {
            if (id != shipMethod.ShipMethodId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shipMethod);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShipMethodExists(shipMethod.ShipMethodId))
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
            return View(shipMethod);
        }

        // GET: ShipMethods/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shipMethod = await _context.ShipMethods
                .FirstOrDefaultAsync(m => m.ShipMethodId == id);
            if (shipMethod == null)
            {
                return NotFound();
            }

            return View(shipMethod);
        }

        // POST: ShipMethods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var shipMethod = await _context.ShipMethods.FindAsync(id);
            if (shipMethod != null)
            {
                _context.ShipMethods.Remove(shipMethod);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShipMethodExists(int id)
        {
            return _context.ShipMethods.Any(e => e.ShipMethodId == id);
        }
    }
}
