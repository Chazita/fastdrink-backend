using FastDrink.Domain.Entities;

namespace FastDrink.Application.Common.Interfaces;

public interface IAuthService
{
    byte[] GenerateSalt();
    string GenerateToken(User user);
    string HashPassword(string requestPassword, byte[] salt);
}
