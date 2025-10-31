using System.ComponentModel.DataAnnotations;

namespace ProjectManagerAPI.DTOs
{
    public record CreateTaskRequest([Required] string Title, DateTime? DueDate);
    public record TaskResponse(int Id, string Title, DateTime? DueDate, bool IsCompleted, int ProjectId);
}
