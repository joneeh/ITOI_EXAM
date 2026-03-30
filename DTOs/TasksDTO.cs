namespace ITOI_EXAM.DTOs
{
    public class TasksDTO
    {
        public class CreateTask
        {
            public string Title { get; set; } = string.Empty;
            public string? Description { get; set; }
            public int? AssignedUserId { get; set; }
        }

        public class UpdateTask
        {
            public string Title { get; set; } = string.Empty;
            public string? Description { get; set; }
            public string Status { get; set; } = "Pending";
            public int? AssignedUserId { get; set; }
        }

        public class Task
        {
            public int Id { get; set; }
            public string Title { get; set; } = string.Empty;
            public string? Description { get; set; }
            public string Status { get; set; } = "Pending";
            public int? AssignedUserId { get; set; }
        }
    }
}