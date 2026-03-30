namespace ITOI_EXAM.DTOs
{
    public class UsersDTO
    {
        public class CreateUser
        {
            public string Fullname { get; set; } = string.Empty;
            public string Email { get; set; } = string.Empty;
            public string Password { get; set; } = string.Empty;
            public int RoleId { get; set; }
        }

        public class UpdateUser
        {
            public string Fullname { get; set; } = string.Empty;
            public string Email { get; set; } = string.Empty;
        }

        public class User
        {
            public int Id { get; set; }
            public string Fullname { get; set; } = string.Empty;
            public string Email { get; set; } = string.Empty;
            public string Role { get; set; } = string.Empty;
        }

        public class ChangePassword
        {
            public string CurrentPassword { get; set; } = string.Empty;
            public string NewPassword { get; set; } = string.Empty;
        }
    }
}
