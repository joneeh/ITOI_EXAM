using ITOI_EXAM.Context;
using ITOI_EXAM.DTOs;
using ITOI_EXAM.Models;
using ITOI_EXAM.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ITOI_EXAM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly PasswordService _passwordService;

        public UsersController(AppDbContext context, PasswordService passwordService)
        {
            _context = context;
            _passwordService = passwordService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsersDTO.User>>> GetUsers()
        {
            var users = await _context.Users
                .AsNoTracking()
                .Include(u => u.Role)
                .Select(u => new UsersDTO.User
                {
                    Id = u.Id,
                    Fullname = u.Fullname,
                    Email = u.Email,
                    Role = u.Role.Role
                })
                .ToListAsync();

            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UsersDTO.User>> GetUser(int id)
        {
            var user = await _context.Users
                .AsNoTracking()
                .Include(u => u.Role)
                .Where(u => u.Id == id)
                .Select(u => new UsersDTO.User
                {
                    Id = u.Id,
                    Fullname = u.Fullname,
                    Email = u.Email,
                    Role = u.Role.Role
                })
                .FirstOrDefaultAsync();

            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<UsersDTO.User>> CreateUser(UsersDTO.CreateUser dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Password))
                return BadRequest("Password is required.");

            var roleExists = await _context.UserRoles
                .AnyAsync(r => r.Id == dto.RoleId);

            if (!roleExists)
                return BadRequest("Invalid RoleId.");

            var user = new Users
            {
                Fullname = dto.Fullname,
                Email = dto.Email,
                RoleId = dto.RoleId
            };

            user.PasswordHash = _passwordService.HashPassword(user, dto.Password);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var role = await _context.UserRoles
                .Where(r => r.Id == dto.RoleId)
                .Select(r => r.Role)
                .FirstAsync();

            return Ok(new UsersDTO.User
            {
                Id = user.Id,
                Fullname = user.Fullname,
                Email = user.Email,
                Role = role
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, UsersDTO.UpdateUser dto)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
                return NotFound();

            user.Fullname = dto.Fullname;
            user.Email = dto.Email;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
                return NotFound();

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost("{id}/change-password")]
        public async Task<IActionResult> ChangePassword(int id, UsersDTO.ChangePassword dto)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
                return NotFound();

            var valid = _passwordService.VerifyPassword(user, dto.CurrentPassword);

            if (!valid)
                return BadRequest("Invalid current password.");

            user.PasswordHash = _passwordService.HashPassword(user, dto.NewPassword);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
