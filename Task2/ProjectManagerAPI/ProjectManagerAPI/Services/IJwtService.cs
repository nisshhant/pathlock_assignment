using ProjectManagerAPI.Models;

namespace ProjectManagerAPI.Services
{
    public interface IJwtService
    {
        string GenerateToken(User user);
    }
}
