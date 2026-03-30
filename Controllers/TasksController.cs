using ITOI_EXAM.Context;
using ITOI_EXAM.DTOs;
using ITOI_EXAM.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ITOI_EXAM.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TasksController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TasksDTO.Task>>> GetTasks()
        {
            var tasks = await _context.Tasks
                .AsNoTracking()
                .Select(t => new TasksDTO.Task
                {
                    Id = t.Id,
                    Title = t.Title,
                    Description = t.Description,
                    Status = t.Status,
                    AssignedUserId = t.AssignedUserId
                })
                .ToListAsync();

            return Ok(tasks);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TasksDTO.Task>> GetTask(int id)
        {
            var task = await _context.Tasks
                .AsNoTracking()
                .Where(t => t.Id == id)
                .Select(t => new TasksDTO.Task
                {
                    Id = t.Id,
                    Title = t.Title,
                    Description = t.Description,
                    Status = t.Status,
                    AssignedUserId = t.AssignedUserId
                })
                .FirstOrDefaultAsync();

            if (task == null)
                return NotFound();

            return Ok(task);
        }

        [HttpPost]
        public async Task<ActionResult<TasksDTO.Task>> CreateTask(TasksDTO.CreateTask dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Title))
                return BadRequest("Title is required.");

            var task = new Tasks
            {
                Title = dto.Title,
                Description = dto.Description,
                Status = "Pending",
                AssignedUserId = dto.AssignedUserId
            };

            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();

            return Ok(new TasksDTO.Task
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                Status = task.Status,
                AssignedUserId = task.AssignedUserId
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, TasksDTO.UpdateTask dto)
        {
            var task = await _context.Tasks.FindAsync(id);

            if (task == null)
                return NotFound();

            task.Title = dto.Title;
            task.Description = dto.Description;
            task.Status = dto.Status;
            task.AssignedUserId = dto.AssignedUserId;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPatch("{id}/status")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] string status)
        {
            var allowed = new[] { "Pending", "In-Progress", "Done" };

            if (!allowed.Contains(status))
                return BadRequest("Invalid status.");

            var task = await _context.Tasks.FindAsync(id);

            if (task == null)
                return NotFound();

            task.Status = status;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPatch("{id}/assign-user")]
        public async Task<IActionResult> AssignUser(int id, [FromBody] int? userId)
        {
            var task = await _context.Tasks.FindAsync(id);

            if (task == null)
                return NotFound();

            task.AssignedUserId = userId;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var task = await _context.Tasks.FindAsync(id);

            if (task == null)
                return NotFound();

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}