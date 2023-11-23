using System.ComponentModel.DataAnnotations;

namespace AirAstana.API.Core.DTOs.User;

public record LoginDto
{
    [Required]
    [StringLength(256)]
    public string UserName { get; set; } = string.Empty;
    
    [Required]
    [StringLength(256)]
    public string Password { get; set; } = string.Empty;
}