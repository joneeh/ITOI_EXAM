using System.ComponentModel.DataAnnotations;

namespace ITOI_EXAM.Models
{
    public class Users
    {
        public int Id { get; set; }

        [Required]
        public string Fullname { get; set; } = string.Empty;

        [Required]
        public string Email { get; set; } = string.Empty;

        public string PasswordHash { get; set; } = string.Empty;

        public int RoleId { get; set; }
        public UserRoles Role { get; set; } = null!;
    }
}
