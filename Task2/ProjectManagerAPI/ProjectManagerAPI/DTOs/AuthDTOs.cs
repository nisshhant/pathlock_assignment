using System.ComponentModel.DataAnnotations;

namespace ProjectManagerAPI.DTOs
{
    public record RegisterRequest([Required, EmailAddress] string Email, [Required, MinLength(6)] string Password);
    public record AuthResponse(string Token, string Email);
    public record LoginRequest([Required, EmailAddress] string Email, [Required] string Password);
}
