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
    public class MonTheThaosController : ControllerBase
    {
        private readonly QuanLyGiaiDauContext _context;

        public MonTheThaosController(QuanLyGiaiDauContext context)
        {
            _context = context;
        }

        // GET: api/MonTheThaos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MonTheThao>>> GetMonTheThaos()
        {
            return await _context.MonTheThaos.ToListAsync();
        }

        // GET: api/MonTheThaos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MonTheThao>> GetMonTheThao(string id)
        {
            var monTheThao = await _context.MonTheThaos.FindAsync(id);

            if (monTheThao == null)
            {
                return NotFound();
            }

            return monTheThao;
        }

        // PUT: api/MonTheThaos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> PutMonTheThao( MonTheThao monTheThao)
        {
            

            _context.Entry(monTheThao).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MonTheThaoExists(monTheThao.IdMonTheThao))
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

        // POST: api/MonTheThaos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MonTheThao>> PostMonTheThao(MonTheThao monTheThao)
        {
            _context.MonTheThaos.Add(monTheThao);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (MonTheThaoExists(monTheThao.IdMonTheThao))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetMonTheThao", new { id = monTheThao.IdMonTheThao }, monTheThao);
        }

        // DELETE: api/MonTheThaos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMonTheThao(string id)
        {
            var monTheThao = await _context.MonTheThaos.FindAsync(id);
            if (monTheThao == null)
            {
                return NotFound();
            }

            _context.MonTheThaos.Remove(monTheThao);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MonTheThaoExists(string id)
        {
            return _context.MonTheThaos.Any(e => e.IdMonTheThao == id);
        }
    }
}
