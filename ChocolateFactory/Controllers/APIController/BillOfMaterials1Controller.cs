using System;
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
    public class BillOfMaterials1Controller : ControllerBase
    {
        private readonly AdventureWorks2022Context _context;

        public BillOfMaterials1Controller(AdventureWorks2022Context context)
        {
            _context = context;
        }

        // GET: api/BillOfMaterials1
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BillOfMaterial>>> GetBillOfMaterials()
        {
            return await _context.BillOfMaterials.ToListAsync();
        }

        // GET: api/BillOfMaterials1/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BillOfMaterial>> GetBillOfMaterial(int id)
        {
            var billOfMaterial = await _context.BillOfMaterials.FindAsync(id);

            if (billOfMaterial == null)
            {
                return NotFound();
            }

            return billOfMaterial;
        }

        // PUT: api/BillOfMaterials1/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBillOfMaterial(int id, BillOfMaterial billOfMaterial)
        {
            if (id != billOfMaterial.BillOfMaterialsId)
            {
                return BadRequest();
            }

            _context.Entry(billOfMaterial).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BillOfMaterialExists(id))
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

        // POST: api/BillOfMaterials1
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BillOfMaterial>> PostBillOfMaterial(BillOfMaterial billOfMaterial)
        {
            _context.BillOfMaterials.Add(billOfMaterial);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBillOfMaterial", new { id = billOfMaterial.BillOfMaterialsId }, billOfMaterial);
        }

        // DELETE: api/BillOfMaterials1/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBillOfMaterial(int id)
        {
            var billOfMaterial = await _context.BillOfMaterials.FindAsync(id);
            if (billOfMaterial == null)
            {
                return NotFound();
            }

            _context.BillOfMaterials.Remove(billOfMaterial);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BillOfMaterialExists(int id)
        {
            return _context.BillOfMaterials.Any(e => e.BillOfMaterialsId == id);
        }
    }
}
