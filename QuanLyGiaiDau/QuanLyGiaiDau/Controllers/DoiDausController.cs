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
    public class DoiDausController : ControllerBase
    {
        private readonly QuanLyGiaiDauContext _context;

        public DoiDausController(QuanLyGiaiDauContext context)
        {
            _context = context;
        }

        // GET: api/DoiDaus
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DoiDau>>> GetDoiDaus()
        {
            
            return await _context.DoiDaus.ToListAsync();
        }

        // GET: api/DoiDaus/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DoiDau>> GetDoiDau(string id)
        {
            var doiDau = await _context.DoiDaus.FindAsync(id);

            if (doiDau == null)
            {
                return NotFound();
            }

            return doiDau;
        }

        // PUT: api/DoiDaus/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDoiDau(string id, DoiDau doiDau)
        {
            if (id != doiDau.IdDoiDau)
            {
                return BadRequest();
            }

            _context.Entry(doiDau).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DoiDauExists(id))
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

        // POST: api/DoiDaus
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DoiDau>> PostDoiDau(DoiDau doiDau)
        {
            _context.DoiDaus.Add(doiDau);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (DoiDauExists(doiDau.IdDoiDau))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetDoiDau", new { id = doiDau.IdDoiDau }, doiDau);
        }

        // DELETE: api/DoiDaus/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDoiDau(string id)
        {
            var doiDau = await _context.DoiDaus.FindAsync(id);
            if (doiDau == null)
            {
                return NotFound();
            }

            _context.DoiDaus.Remove(doiDau);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DoiDauExists(string id)
        {
            return _context.DoiDaus.Any(e => e.IdDoiDau == id);
        }

        [HttpGet("{id}/danh-sach-player")]
        public async Task<ActionResult<IEnumerable<User>>> GetPlayersByDoiDauId(string id)
        {
            var listUser = await _context.Users
                .Join(
                    _context.CT_DoiDaus,
                    user => user.IdUser,
                    ct => ct.IdUser,
                    (user, ct) => new { User = user, CT_DoiDau = ct }
                )
                .Where(join => join.CT_DoiDau.IdDoiDau == id)
                .Select(join => join.User)
                .ToListAsync();

            return listUser;
        }
    }
}
