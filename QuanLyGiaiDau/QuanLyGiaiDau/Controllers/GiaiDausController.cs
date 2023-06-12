using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyGiaiDau.Models;

namespace QuanLyGiaiDau.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GiaiDausController : ControllerBase
    {
        private readonly QuanLyGiaiDauContext _context;

        public GiaiDausController(QuanLyGiaiDauContext context)
        {
            _context = context;
        }

        // GET: api/GiaiDaus
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GiaiDau>>> GetGiaiDaus()
        {
            var a = await _context.GiaiDaus.ToListAsync();
            foreach(var item in a)
            {
                item.MonTheThao = _context.MonTheThaos.Where(x => x.IdMonTheThao == item.IdMonTheThao).First();
            }
            return a;
        }

        // GET: api/GiaiDaus/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GiaiDau>> GetGiaiDau(int id)
        {
            var giaiDau = await _context.GiaiDaus.FindAsync(id);

            if (giaiDau == null)
            {
                return NotFound();
            }

            return giaiDau;
        }

        // PUT: api/GiaiDaus/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGiaiDau(int id, GiaiDau giaiDau)
        {
            if (id != giaiDau.IdGiaiDau)
            {
                return BadRequest();
            }

            _context.Entry(giaiDau).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GiaiDauExists(id))
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

        // POST: api/GiaiDaus
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<GiaiDau>> PostGiaiDau(GiaiDau giaiDau)
        {
            var a = _context.MonTheThaos.Where(x => x.IdMonTheThao == giaiDau.IdMonTheThao).First();
            if (a == null)
            {
                return BadRequest();
            }
            giaiDau.MonTheThao = a;
            giaiDau.TrangThai = true;
            _context.GiaiDaus.Add(giaiDau);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGiaiDau", new { id = giaiDau.IdGiaiDau }, giaiDau);
        }

        // DELETE: api/GiaiDaus/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGiaiDau(int id)
        {
            var giaiDau = await _context.GiaiDaus.FindAsync(id);
            if (giaiDau == null)
            {
                return NotFound();
            }
            giaiDau.TrangThai = false;
            
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GiaiDauExists(int id)
        {
            return _context.GiaiDaus.Any(e => e.IdGiaiDau == id);
        }
    }
}
