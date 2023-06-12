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
    public class CT_TranDauController : ControllerBase
    {
        private readonly QuanLyGiaiDauContext _context;

        public CT_TranDauController(QuanLyGiaiDauContext context)
        {
            _context = context;
        }

        // GET: api/CT_TranDau
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CT_TranDau>>> GetCT_TranDaus()
        {
            var a = await _context.CT_TranDaus.ToListAsync();
            foreach(var item in a)
            {
                item.DoiDau = _context.DoiDaus.Where(x => x.IdDoiDau == item.IdDoiDau).First();
                item.GiaiDau = _context.GiaiDaus.Where(x => x.IdGiaiDau == item.IdGiaiDau).First();
            }

            return a;
        }

        // GET: api/CT_TranDau/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CT_TranDau>> GetCT_TranDau(int id)
        {
            var cT_TranDau = await _context.CT_TranDaus.FindAsync(id);

            if (cT_TranDau == null)
            {
                return NotFound();
            }

            return cT_TranDau;
        }

        // PUT: api/CT_TranDau/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCT_TranDau(int id, CT_TranDau cT_TranDau)
        {
            if (id != cT_TranDau.IdCTTranDau)
            {
                return BadRequest();
            }

            _context.Entry(cT_TranDau).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CT_TranDauExists(id))
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

        // POST: api/CT_TranDau
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CT_TranDau>> PostCT_TranDau(CT_TranDau cT_TranDau)
        {
            var a = _context.DoiDaus.Where(x => x.IdDoiDau == cT_TranDau.IdDoiDau).First();
            var b = _context.GiaiDaus.Where(x => x.IdGiaiDau == cT_TranDau.IdGiaiDau).First();
            if (a == null || b == null)
            {
                return BadRequest();
            }
            cT_TranDau.DoiDau = a;
            cT_TranDau.GiaiDau = b;
            _context.CT_TranDaus.Add(cT_TranDau);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCT_TranDau", new { id = cT_TranDau.IdCTTranDau }, cT_TranDau);
        }

        // DELETE: api/CT_TranDau/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCT_TranDau(int id)
        {
            var cT_TranDau = await _context.CT_TranDaus.FindAsync(id);
            if (cT_TranDau == null)
            {
                return NotFound();
            }

            _context.CT_TranDaus.Remove(cT_TranDau);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CT_TranDauExists(int id)
        {
            return _context.CT_TranDaus.Any(e => e.IdCTTranDau == id);
        }

        
    }
}
