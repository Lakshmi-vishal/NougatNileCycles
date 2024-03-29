﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ChocolateFactory.Models;

namespace ChocolateFactory.Controllers.APIController
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkOrdersControllerAPI : ControllerBase
    {
        private readonly AdventureWorks2022Context _context;

        public WorkOrdersControllerAPI(AdventureWorks2022Context context)
        {
            _context = context;
        }

        // GET: api/WorkOrdersControllerAPI
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkOrder>>> GetWorkOrders()
        {
            return await _context.WorkOrders.ToListAsync();
        }

        // GET: api/WorkOrdersControllerAPI/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WorkOrder>> GetWorkOrder(int id)
        {
            var workOrder = await _context.WorkOrders.FindAsync(id);

            if (workOrder == null)
            {
                return NotFound();
            }

            return workOrder;
        }

        // PUT: api/WorkOrdersControllerAPI/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWorkOrder(int id, WorkOrder workOrder)
        {
            if (id != workOrder.WorkOrderId)
            {
                return BadRequest();
            }

            _context.Entry(workOrder).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkOrderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/WorkOrdersControllerAPI
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<WorkOrder>> PostWorkOrder(WorkOrder workOrder)
        {
            _context.WorkOrders.Add(workOrder);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWorkOrder", new { id = workOrder.WorkOrderId }, workOrder);
        }

        // DELETE: api/WorkOrdersControllerAPI/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkOrder(int id)
        {
            var workOrder = await _context.WorkOrders.FindAsync(id);
            if (workOrder == null)
            {
                return NotFound();
            }

            _context.WorkOrders.Remove(workOrder);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool WorkOrderExists(int id)
        {
            return _context.WorkOrders.Any(e => e.WorkOrderId == id);
        }
    }
}
