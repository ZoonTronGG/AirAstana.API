using AirAstana.API.Core.DTOs.User;

namespace AirAstana.API.Core.Contracts;

public interface IAuthManager
{
    Task<AuthResponseDto> Login(LoginDto loginDto);
    Task<string> CreateRefreshToken();
}