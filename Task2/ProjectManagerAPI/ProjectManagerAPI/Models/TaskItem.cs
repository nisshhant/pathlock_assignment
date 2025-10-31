using System.ComponentModel.DataAnnotations;

namespace ProjectManagerAPI.Models
{
    public class TaskItem
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public DateTime? DueDate { get; set; }

        public bool IsCompleted { get; set; } = false;

        // relation
        public int ProjectId { get; set; }
        public Project Project { get; set; }
    }
}
