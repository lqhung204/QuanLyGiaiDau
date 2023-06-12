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
    public class UsersController : ControllerBase
    {
        private readonly QuanLyGiaiDauContext _context;

        public UsersController(QuanLyGiaiDauContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(string id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(string id, User user)
        {
            if (id != user.IdUser)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(user))
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

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            _context.Users.Add(user);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UserExists(user))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetUser", new { id = user.IdUser }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            user.TrangThai= false;
            await _context.SaveChangesAsync();
           

            return NoContent();
        }

        private bool UserExists(User user)
        {
            var a = _context.Users.Where(x=>x.Email == user.Email || x.IdUser == user.Password).ToList();
            if( a == null)
                return false;
            return true;
            
        }

        [HttpPost]
        [Route("/api/dang-nhap")]
        public async Task<ActionResult<User>> DangNhap(UserLogin userLogin)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == userLogin.Email && x.Password == userLogin.Password);

            if (user == null)
            {
                return Unauthorized();
            }

            

            return user;
        }

        [HttpPost]
        [Route("/api/dang-ky")]
        public async Task<ActionResult<User>> DangKy(User user)
        {
            user.TrangThai = true;
            user.Role = "User";

            _context.Users.Add(user);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UserExists(user))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetUser", new { id = user.IdUser }, user);
        }
    }
    public class UserLogin
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
