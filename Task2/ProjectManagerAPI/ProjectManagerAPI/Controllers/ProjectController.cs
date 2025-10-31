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
    [Route("api/[controller]")]
    public class ProjectsController : ControllerBase
    {
        private readonly AppDbContext _db;
        public ProjectsController(AppDbContext db) { _db = db; }

        private int CurrentUserId => int.Parse(User.FindFirstValue("uid"));

        [HttpGet]
        public async Task<IActionResult> GetProjects()
        {
            var projects = await _db.Projects
                .Where(p => p.OwnerId == CurrentUserId)
                .Select(p => new ProjectResponse(p.Id, p.Title, p.Description, p.CreatedAt))
                .ToListAsync();
            return Ok(projects);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProject(CreateProjectRequest req)
        {
            var project = new Project
            {
                Title = req.Title,
                Description = req.Description,
                OwnerId = CurrentUserId
            };
            _db.Projects.Add(project);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetProjects), new { id = project.Id }, new ProjectResponse(project.Id, project.Title, project.Description, project.CreatedAt));
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            var project = await _db.Projects.FirstOrDefaultAsync(p => p.Id == id && p.OwnerId == CurrentUserId);
            if (project == null) return NotFound();
            _db.Projects.Remove(project);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
