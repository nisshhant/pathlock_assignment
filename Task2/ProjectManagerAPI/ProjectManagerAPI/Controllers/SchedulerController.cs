using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectManagerAPI.Data;
using System.Security.Claims;

namespace ProjectManagerAPI.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/projects/{projectId:int}/schedule")]
    public class SchedulerController : ControllerBase
    {
        private readonly AppDbContext _db;
        public SchedulerController(AppDbContext db) { _db = db; }

        private int CurrentUserId => int.Parse(User.FindFirstValue("uid"));

        public class ScheduleRequest
        {
            public DateTime? StartDate { get; set; } = DateTime.UtcNow;
            public int WorkHoursPerDay { get; set; } = 6;
        }

        [HttpPost]
        public async Task<IActionResult> GenerateSchedule(int projectId, [FromBody] ScheduleRequest req)
        {
            var project = await _db.Projects
                .Include(p => p.Tasks)
                .FirstOrDefaultAsync(p => p.Id == projectId && p.OwnerId == CurrentUserId);

            if (project == null)
                return NotFound(new { error = "Project not found or unauthorized" });

            var tasks = project.Tasks
                .Where(t => !t.IsCompleted)
                .OrderBy(t => t.DueDate ?? DateTime.MaxValue)
                .ToList();

            if (!tasks.Any())
                return BadRequest(new { message = "No pending tasks to schedule" });

            // Simple logic: spread tasks across days
            var start = req.StartDate?.Date ?? DateTime.UtcNow.Date;
            var schedule = new List<object>();
            int dayOffset = 0;

            foreach (var task in tasks)
            {
                var date = start.AddDays(dayOffset);
                schedule.Add(new
                {
                    taskId = task.Id,
                    taskTitle = task.Title,
                    scheduledDate = date.ToString("yyyy-MM-dd")
                });

                dayOffset++;
            }

            return Ok(new
            {
                projectId,
                project.Title,
                startDate = start,
                schedule
            });
        }
    }
}
