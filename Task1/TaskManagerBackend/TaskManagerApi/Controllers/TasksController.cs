using Microsoft.AspNetCore.Mvc;
using TaskManagerApi.Data;
using TaskManagerApi.Models;

namespace TaskManagerApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<TaskItem>> GetAll()
    {
        return Ok(InMemoryTaskStore.GetAll());
    }

    [HttpGet("{id:guid}")]
    public ActionResult<TaskItem> Get(Guid id)
    {
        var task = InMemoryTaskStore.Get(id);
        if (task == null) return NotFound();
        return Ok(task);
    }

    [HttpPost]
    public ActionResult<TaskItem> Create([FromBody] TaskItemCreateDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Description)) return BadRequest("Description required");

        var item = new TaskItem
        {
            Description = dto.Description,
            IsCompleted = false
        };

        InMemoryTaskStore.Add(item);
        return CreatedAtAction(nameof(Get), new { id = item.Id }, item);
    }

    [HttpPut("{id:guid}")]
    public ActionResult Update(Guid id, [FromBody] TaskItemUpdateDto dto)
    {
        var existing = InMemoryTaskStore.Get(id);
        if (existing == null) return NotFound();

        existing.Description = dto.Description ?? existing.Description;
        existing.IsCompleted = dto.IsCompleted ?? existing.IsCompleted;

        InMemoryTaskStore.Update(id, existing);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public ActionResult Delete(Guid id)
    {
        var removed = InMemoryTaskStore.Remove(id);
        if (!removed) return NotFound();
        return NoContent();
    }
}

public record TaskItemCreateDto(string Description);
public record TaskItemUpdateDto(string? Description, bool? IsCompleted);
