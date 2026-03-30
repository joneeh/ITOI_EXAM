using ITOI_EXAM.Context;
using ITOI_EXAM.DTOs;
using ITOI_EXAM.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ITOI_EXAM.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserRolesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UserRolesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserRolesDTO.UserRole>>> GetRoles()
        {
            var roles = await _context.UserRoles
                .AsNoTracking()
                .Select(r => new UserRolesDTO.UserRole
                {
                    Id = r.Id,
                    Role = r.Role
                })
                .ToListAsync();

            return Ok(roles);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserRolesDTO.UserRole>> GetRole(int id)
        {
            var role = await _context.UserRoles
                .AsNoTracking()
                .Where(r => r.Id == id)
                .Select(r => new UserRolesDTO.UserRole
                {
                    Id = r.Id,
                    Role = r.Role
                })
                .FirstOrDefaultAsync();

            if (role == null)
                return NotFound();

            return Ok(role);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(UserRolesDTO.CreateUserRole dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Role))
                return BadRequest("Role is required.");

            var exists = await _context.UserRoles
                .AnyAsync(r => r.Role == dto.Role);

            if (exists)
                return BadRequest("Role already exists.");

            var role = new UserRoles
            {
                Role = dto.Role
            };

            _context.UserRoles.Add(role);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRole(int id, UserRolesDTO.CreateUserRole dto)
        {
            var role = await _context.UserRoles.FindAsync(id);

            if (role == null)
                return NotFound();

            role.Role = dto.Role;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            var role = await _context.UserRoles.FindAsync(id);

            if (role == null)
                return NotFound();

            _context.UserRoles.Remove(role);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
