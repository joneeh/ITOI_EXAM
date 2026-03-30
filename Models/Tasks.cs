namespace ITOI_EXAM.Models
{
    public class Tasks
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string Status { get; set; } = "Pending";
        public int? AssignedUserId { get; set; }
    }
}
