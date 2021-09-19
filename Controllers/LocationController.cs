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
    public class LocationController : ControllerBase
    {
        private readonly LocationContext _context;

        public LocationController(LocationContext context)
        {
            _context = context;
        }

        // GET: api/Location
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Location>>> GetLocationItems()
        {
            return await _context.LocationItems.ToListAsync();
        }

        // GET: api/Location/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Location>> GetLocation(int id)
        {
            var location = await _context.LocationItems.FindAsync(id);

            if (location == null)
            {
                return NotFound();
            }

            return location;
        }

        // PUT: api/Location/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLocation(int id, Location location)
        {
            if (id != location.Id)
            {
                return BadRequest();
            }

            _context.Entry(location).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LocationExists(id))
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

        // POST: api/Location
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Location>> PostLocation(Location location)
        {
            _context.LocationItems.Add(location);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetLocation), new { id = location.Id }, location);
        }

        // DELETE: api/Location/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLocation(int id)
        {
            var location = await _context.LocationItems.FindAsync(id);
            if (location == null)
            {
                return NotFound();
            }

            _context.LocationItems.Remove(location);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LocationExists(int id)
        {
            return _context.LocationItems.Any(e => e.Id == id);
        }
    }
}
