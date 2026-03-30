namespace ITOI_EXAM.DTOs
{
    public class UserRolesDTO
    {
        public class CreateUserRole
        {
            public int UserId { get; set; }
            public string Role { get; set; } = string.Empty;
        }

        public class UserRole
        {
            public int Id { get; set; }
            public string Role { get; set; } = string.Empty;
        }
    }
}
