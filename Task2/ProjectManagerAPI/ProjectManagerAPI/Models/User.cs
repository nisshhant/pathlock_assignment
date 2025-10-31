using System.ComponentModel.DataAnnotations;

namespace ProjectManagerAPI.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        public List<Project> Projects { get; set; } = new();
    }
}
