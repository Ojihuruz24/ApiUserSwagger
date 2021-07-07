using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BaseDeDatos.DataAccess;

namespace BaseDeDatos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsersController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            try
            {
               
                return Ok(_context.Users.ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{identity}", Name = nameof(GetUsers))]
        public async Task<ActionResult<User>> GetUsers(int identity)
        {
            try
            {
                var users = await _context.Users.FirstAsync(u => u.Identity == identity);
                return Ok(users);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /// <summary>
        /// Se utiliza [FromBody para que todo vaya en el json lo que se envia]
        /// </summary>
        /// <param name="users"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<User>> PostUsers(User users)
        {
            try
            {
                _context.Users.Add(users);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetUsers), new { Identity = users.Identity }, users);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{identity}")]
        public async Task<IActionResult> PutUsers(int identity, User users)
        {
            if (identity != users.Identity)
            {
                return BadRequest();
            }

            _context.Entry(users).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return Ok(users);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsersExists(identity))
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


        [HttpDelete("{identity}")]
        public async Task<IActionResult> DeleteUsers(int identity)
        {
            var user = await _context.Users.FirstAsync(u => u.Identity == identity);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UsersExists(int id)
        {
            return _context.Users.Any(e => e.Identity == id);
        }
    }
}
