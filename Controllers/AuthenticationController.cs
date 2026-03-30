using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ITOI_EXAM.Context;
using ITOI_EXAM.DTOs;
using ITOI_EXAM.Models;
using ITOI_EXAM.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace ITOI_EXAM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly PasswordService _passwordService;
        private readonly IConfiguration _config;

        public AuthController(
            AppDbContext context,
            PasswordService passwordService,
            IConfiguration config)
        {
            _context = context;
            _passwordService = passwordService;
            _config = config;
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthDTO.LoginResponse>> Login(AuthDTO.LoginRequest dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Email) ||
                string.IsNullOrWhiteSpace(dto.Password))
            {
                return BadRequest("Email and Password are required.");
            }

            var user = await _context.Users
                .AsNoTracking()
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Email == dto.Email);

            if (user == null)
                return Unauthorized("Invalid credentials.");

            var valid = _passwordService.VerifyPassword(user, dto.Password);

            if (!valid)
                return Unauthorized("Invalid credentials.");

            var token = GenerateJwtToken(user);

            return Ok(new AuthDTO.LoginResponse
            {
                Token = token,
                Fullname = user.Fullname,
                Email = user.Email,
                Role = user.Role.Role
            });
        }

        private string GenerateJwtToken(Users user)
        {
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Fullname),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role?.Role ?? "")
            };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(8), 
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}