namespace ITOI_EXAM.Models
{
    public class UserRoles
    {
        public int Id { get; set; }
        public string Role { get; set; } = string.Empty;

        public ICollection<Users> Users { get; set; } = new List<Users>();
    }
}

