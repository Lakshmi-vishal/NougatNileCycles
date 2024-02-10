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
    public class JobCandidatesController : ControllerBase
    {
        private readonly AdventureWorks2022Context _context;

        public JobCandidatesController(AdventureWorks2022Context context)
        {
            _context = context;
        }

        // GET: api/JobCandidates
        [HttpGet]
        public async Task<ActionResult<IEnumerable<JobCandidate>>> GetJobCandidates()
        {
            return await _context.JobCandidates.ToListAsync();
        }

        // GET: api/JobCandidates/5
        [HttpGet("{id}")]
        public async Task<ActionResult<JobCandidate>> GetJobCandidate(int id)
        {
            var jobCandidate = await _context.JobCandidates.FindAsync(id);

            if (jobCandidate == null)
            {
                return NotFound();
            }

            return jobCandidate;
        }

        // PUT: api/JobCandidates/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutJobCandidate(int id, JobCandidate jobCandidate)
        {
            if (id != jobCandidate.JobCandidateId)
            {
                return BadRequest();
            }

            _context.Entry(jobCandidate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JobCandidateExists(id))
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

        // POST: api/JobCandidates
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<JobCandidate>> PostJobCandidate(JobCandidate jobCandidate)
        {
            _context.JobCandidates.Add(jobCandidate);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetJobCandidate", new { id = jobCandidate.JobCandidateId }, jobCandidate);
        }

        // DELETE: api/JobCandidates/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJobCandidate(int id)
        {
            var jobCandidate = await _context.JobCandidates.FindAsync(id);
            if (jobCandidate == null)
            {
                return NotFound();
            }

            _context.JobCandidates.Remove(jobCandidate);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool JobCandidateExists(int id)
        {
            return _context.JobCandidates.Any(e => e.JobCandidateId == id);
        }
    }
}
