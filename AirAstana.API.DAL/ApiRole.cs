using Microsoft.AspNetCore.Identity;

namespace AirAstana.API.DAL;

public class ApiRole : IdentityRole<int>
{
    public string Code { get; set; } = null!;
}