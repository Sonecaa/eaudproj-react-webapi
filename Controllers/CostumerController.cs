using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using eaudproj_react_design;
using eaudproj_react_design.Models;

namespace Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CostumerController : ControllerBase
    {
        private readonly CostumerContext _context;

        public CostumerController(CostumerContext context)
        {
            _context = context;
        }

        // GET: api/Costumer
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Costumer>>> GetCostumerItems()
        {
            return await _context.CostumerItems.ToListAsync();
        }

        // GET: api/Costumer/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Costumer>> GetCostumer(int id)
        {
            var costumer = await _context.CostumerItems.FindAsync(id);

            if (costumer == null)
            {
                return NotFound();
            }

            return costumer;
        }

        // PUT: api/Costumer/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCostumer(int id, Costumer costumer)
        {
            if (id != costumer.Id)
            {
                return BadRequest();
            }

            _context.Entry(costumer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CostumerExists(id))
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

        // POST: api/Costumer
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Costumer>> PostCostumer(Costumer costumer)
        {
            _context.CostumerItems.Add(costumer);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCostumer), new { id = costumer.Id }, costumer);
        }

        // DELETE: api/Costumer/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCostumer(int id)
        {
            var costumer = await _context.CostumerItems.FindAsync(id);
            if (costumer == null)
            {
                return NotFound();
            }

            _context.CostumerItems.Remove(costumer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CostumerExists(int id)
        {
            return _context.CostumerItems.Any(e => e.Id == id);
        }
    }
}
