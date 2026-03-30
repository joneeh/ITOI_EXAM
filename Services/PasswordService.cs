using ITOI_EXAM.Models;
using Microsoft.AspNetCore.Identity;

namespace ITOI_EXAM.Services
{

    public class PasswordService
    {
        private readonly PasswordHasher<Users> _hasher = new();

        public string HashPassword(Users user, string password)
        {
            return _hasher.HashPassword(user, password);
        }

        public bool VerifyPassword(Users user, string password)
        {
            var result = _hasher.VerifyHashedPassword(user, user.PasswordHash, password);
            return result == PasswordVerificationResult.Success;
        }
    }
}
