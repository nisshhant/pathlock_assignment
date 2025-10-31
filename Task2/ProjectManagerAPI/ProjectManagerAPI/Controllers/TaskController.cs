using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectManagerAPI.Data;
using ProjectManagerAPI.DTOs;
using ProjectManagerAPI.Models;
using System.Security.Claims;

namespace ProjectManagerAPI.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api")]
    public class TasksController : ControllerBase
    {
        private readonly AppDbContext _db;
        public TasksController(AppDbContext db) { _db = db; }

        private int CurrentUserId => int.Parse(User.FindFirstValue("uid"));

        [HttpGet("projects/{projectId:int}/tasks")]
        public async Task<IActionResult> GetTasks(int projectId)
        {
            var project = await _db.Projects.FirstOrDefaultAsync(p => p.Id == projectId && p.OwnerId == CurrentUserId);
            if (project == null) return NotFound();

            var tasks = await _db.Tasks
                .Where(t => t.ProjectId == projectId)
                .Select(t => new TaskResponse(t.Id, t.Title, t.DueDate, t.IsCompleted, t.ProjectId))
                .ToListAsync();

            return Ok(tasks);
        }

        [HttpPost("projects/{projectId:int}/tasks")]
        public async Task<IActionResult> CreateTask(int projectId, CreateTaskRequest req)
        {
            var project = await _db.Projects.FirstOrDefaultAsync(p => p.Id == projectId && p.OwnerId == CurrentUserId);
            if (project == null) return NotFound();

            var task = new TaskItem
            {
                Title = req.Title,
                DueDate = req.DueDate,
                ProjectId = projectId
            };

            _db.Tasks.Add(task);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTasks), new { projectId }, new TaskResponse(task.Id, task.Title, task.DueDate, task.IsCompleted, task.ProjectId));
        }

        [HttpPut("tasks/{id:int}")]
        public async Task<IActionResult> UpdateTask(int id, TaskResponse update)
        {
            var task = await _db.Tasks
                .Include(t => t.Project)
                .FirstOrDefaultAsync(t => t.Id == id && t.Project.OwnerId == CurrentUserId);

            if (task == null) return NotFound();

            // update allowed fields
            task.Title = update.Title;
            task.DueDate = update.DueDate;
            task.IsCompleted = update.IsCompleted;

            await _db.SaveChangesAsync();

            return Ok(new TaskResponse(task.Id, task.Title, task.DueDate, task.IsCompleted, task.ProjectId));
        }

        [HttpDelete("tasks/{id:int}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var task = await _db.Tasks
                .Include(t => t.Project)
                .FirstOrDefaultAsync(t => t.Id == id && t.Project.OwnerId == CurrentUserId);

            if (task == null) return NotFound();

            _db.Tasks.Remove(task);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
