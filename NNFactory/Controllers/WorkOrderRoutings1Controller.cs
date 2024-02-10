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
    public class WorkOrderRoutings1Controller : Controller
    {
        private readonly AdventureWorks2022Context _context;

        public WorkOrderRoutings1Controller(AdventureWorks2022Context context)
        {
            _context = context;
        }

        // GET: WorkOrderRoutings1
        public async Task<IActionResult> Index()
        {
            var adventureWorks2022Context = _context.WorkOrderRoutings.Include(w => w.Location).Include(w => w.WorkOrder);
            return View(await adventureWorks2022Context.ToListAsync());
        }

        // GET: WorkOrderRoutings1/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workOrderRouting = await _context.WorkOrderRoutings
                .Include(w => w.Location)
                .Include(w => w.WorkOrder)
                .FirstOrDefaultAsync(m => m.WorkOrderId == id);
            if (workOrderRouting == null)
            {
                return NotFound();
            }

            return View(workOrderRouting);
        }

        // GET: WorkOrderRoutings1/Create
        public IActionResult Create()
        {
            ViewData["LocationId"] = new SelectList(_context.Locations, "LocationId", "LocationId");
            ViewData["WorkOrderId"] = new SelectList(_context.WorkOrders, "WorkOrderId", "WorkOrderId");
            return View();
        }

        // POST: WorkOrderRoutings1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WorkOrderId,ProductId,OperationSequence,LocationId,ScheduledStartDate,ScheduledEndDate,ActualStartDate,ActualEndDate,ActualResourceHrs,PlannedCost,ActualCost,ModifiedDate")] WorkOrderRouting workOrderRouting)
        {
            if (ModelState.IsValid)
            {
                _context.Add(workOrderRouting);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LocationId"] = new SelectList(_context.Locations, "LocationId", "LocationId", workOrderRouting.LocationId);
            ViewData["WorkOrderId"] = new SelectList(_context.WorkOrders, "WorkOrderId", "WorkOrderId", workOrderRouting.WorkOrderId);
            return View(workOrderRouting);
        }

        // GET: WorkOrderRoutings1/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workOrderRouting = await _context.WorkOrderRoutings.FindAsync(id);
            if (workOrderRouting == null)
            {
                return NotFound();
            }
            ViewData["LocationId"] = new SelectList(_context.Locations, "LocationId", "LocationId", workOrderRouting.LocationId);
            ViewData["WorkOrderId"] = new SelectList(_context.WorkOrders, "WorkOrderId", "WorkOrderId", workOrderRouting.WorkOrderId);
            return View(workOrderRouting);
        }

        // POST: WorkOrderRoutings1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("WorkOrderId,ProductId,OperationSequence,LocationId,ScheduledStartDate,ScheduledEndDate,ActualStartDate,ActualEndDate,ActualResourceHrs,PlannedCost,ActualCost,ModifiedDate")] WorkOrderRouting workOrderRouting)
        {
            if (id != workOrderRouting.WorkOrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(workOrderRouting);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WorkOrderRoutingExists(workOrderRouting.WorkOrderId))
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
            ViewData["LocationId"] = new SelectList(_context.Locations, "LocationId", "LocationId", workOrderRouting.LocationId);
            ViewData["WorkOrderId"] = new SelectList(_context.WorkOrders, "WorkOrderId", "WorkOrderId", workOrderRouting.WorkOrderId);
            return View(workOrderRouting);
        }

        // GET: WorkOrderRoutings1/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workOrderRouting = await _context.WorkOrderRoutings
                .Include(w => w.Location)
                .Include(w => w.WorkOrder)
                .FirstOrDefaultAsync(m => m.WorkOrderId == id);
            if (workOrderRouting == null)
            {
                return NotFound();
            }

            return View(workOrderRouting);
        }

        // POST: WorkOrderRoutings1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var workOrderRouting = await _context.WorkOrderRoutings.FindAsync(id);
            if (workOrderRouting != null)
            {
                _context.WorkOrderRoutings.Remove(workOrderRouting);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WorkOrderRoutingExists(int id)
        {
            return _context.WorkOrderRoutings.Any(e => e.WorkOrderId == id);
        }
    }
}
