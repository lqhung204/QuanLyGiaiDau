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
    public class CT_DoiDauController : ControllerBase
    {
        private readonly QuanLyGiaiDauContext _context;
        private ActionResult<IEnumerable<DoiDau>> listUser;

        public CT_DoiDauController(QuanLyGiaiDauContext context)
        {
            _context = context;
        }

        // GET: api/CT_DoiDau
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CT_DoiDau>>> GetCT_DoiDaus()
        {
            var a = await _context.CT_DoiDaus.ToListAsync();
            foreach(var item in a)
            {
                item.DoiDau =_context.DoiDaus.Where(d => d.IdDoiDau == item.IdDoiDau).First();
                item.User = _context.Users.Where(d => d.IdUser == item.IdUser).First();
            }
            return a;
        }

        // GET: api/CT_DoiDau/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CT_DoiDau>> GetCT_DoiDau(int id)
        {
            var cT_DoiDau = await _context.CT_DoiDaus.FindAsync(id);

            if (cT_DoiDau == null)
            {
                return NotFound();
            }

            return cT_DoiDau;
        }

        // PUT: api/CT_DoiDau/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCT_DoiDau(int id, CT_DoiDau cT_DoiDau)
        {
            if (id != cT_DoiDau.IdCTDoiDau)
            {
                return BadRequest();
            }

            _context.Entry(cT_DoiDau).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CT_DoiDauExists(id))
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

        // POST: api/CT_DoiDau
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CT_DoiDau>> PostCT_DoiDau([FromBody] CT_DoiDau cT_DoiDau)
        {
            var a =  await _context.DoiDaus.Where(d => d.IdDoiDau== cT_DoiDau.IdDoiDau).FirstOrDefaultAsync();
            var b = await _context.Users.Where(d=>d.IdUser==cT_DoiDau.IdUser).FirstOrDefaultAsync();
            
            if (a==null || b == null )
            {
                return BadRequest();
            }
            
            

            cT_DoiDau.DoiDau = a;
                cT_DoiDau.User = b;
                cT_DoiDau.TrangThaiTV = true;
                _context.CT_DoiDaus.Add(cT_DoiDau);

                await _context.SaveChangesAsync();
                
                return CreatedAtAction("GetCT_DoiDau", new { id = cT_DoiDau.IdCTDoiDau }, cT_DoiDau);
              
            
        }

        // DELETE: api/CT_DoiDau/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCT_DoiDau(int id)
        {
            var cT_DoiDau = await _context.CT_DoiDaus.FindAsync(id);
            if (cT_DoiDau == null)
            {
                return NotFound();
            }

            _context.CT_DoiDaus.Remove(cT_DoiDau);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CT_DoiDauExists(int id)
        {
            return _context.CT_DoiDaus.Any(e => e.IdCTDoiDau == id);
        }



        [HttpPost]
        [Route("tham-gia-doi")]
        public async Task<ActionResult<CT_DoiDau>> ThemThanhVienVaoDoi([FromBody] CT_DoiDau cT_DoiDau)
        {
            var a = await _context.DoiDaus.Where(d => d.IdDoiDau == cT_DoiDau.IdDoiDau).FirstOrDefaultAsync();
            var b = await _context.Users.Where(d => d.IdUser == cT_DoiDau.IdUser).FirstOrDefaultAsync();
            //var c = await _context.CT_DoiDaus.Where(d=>d.IdUser == cT_DoiDau.IdUser && d.IdDoiDau == cT_DoiDau.IdDoiDau).FirstOrDefaultAsync();
            if (a == null || b == null )
            {
                return BadRequest();
            }



            cT_DoiDau.DoiDau = a;
            cT_DoiDau.User = b;
            cT_DoiDau.TrangThaiTV = true;
            _context.CT_DoiDaus.Add(cT_DoiDau);

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCT_DoiDau", new { id = cT_DoiDau.IdCTDoiDau }, cT_DoiDau);


        }
    }
}
