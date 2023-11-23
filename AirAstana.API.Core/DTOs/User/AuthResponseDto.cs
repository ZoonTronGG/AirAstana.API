namespace AirAstana.API.Core.DTOs.User;

public record AuthResponseDto
{
    public int UserId { get; set; }
    public string Token { get; set; }
    public string RefreshToken { get; set; }
}