using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CentralizedAuth.Api.Models;
using Microsoft.AspNetCore.Authorization;

namespace CentralizedAuth.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RuleApplicationsController : ControllerBase
    {
        private readonly RuleAppDbContext _context;

        public RuleApplicationsController(RuleAppDbContext context)
        {
            _context = context;
        }

        // GET: api/RuleApplications
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RuleApplication>>> GetRuleApp()
        {
            return await _context.RuleApp.ToListAsync();
        }

        // GET: api/RuleApplications/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RuleApplication>> GetRuleApplication(string id)
        {
            var ruleApplication = await _context.RuleApp.FindAsync(id);

            if (ruleApplication == null)
            {
                return NotFound();
            }

            return ruleApplication;
        }

        // PUT: api/RuleApplications/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRuleApplication(string id, RuleApplication ruleApplication)
        {
            if (id != ruleApplication.Id)
            {
                return BadRequest();
            }

            _context.Entry(ruleApplication).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RuleApplicationExists(id))
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

        // POST: api/RuleApplications
        [HttpPost]
        public async Task<ActionResult<RuleApplication>> PostRuleApplication(RuleApplication ruleApplication)
        {
            _context.RuleApp.Add(ruleApplication);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRuleApplication", new { id = ruleApplication.Id }, ruleApplication);
        }

        // DELETE: api/RuleApplications/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<RuleApplication>> DeleteRuleApplication(string id)
        {
            var ruleApplication = await _context.RuleApp.FindAsync(id);
            if (ruleApplication == null)
            {
                return NotFound();
            }

            _context.RuleApp.Remove(ruleApplication);
            await _context.SaveChangesAsync();

            return ruleApplication;
        }

        private bool RuleApplicationExists(string id)
        {
            return _context.RuleApp.Any(e => e.Id == id);
        }
    }
}
