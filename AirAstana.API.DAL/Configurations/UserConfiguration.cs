using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AirAstana.API.DAL.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<ApiUser>
{
    public void Configure(EntityTypeBuilder<ApiUser> builder)
    {
        
        var passwordHasher = new PasswordHasher<ApiUser>();

        // Create a list of default users
        ApiUser user1 = new ApiUser
        {
            Id = 1,
            UserName = "ArtyomTitorenko",
            NormalizedUserName = "ARTYOMTITORENKO",
            Email = "vbymrf2@gmail.com",
            NormalizedEmail = "VBYMRF2@GMAIL.COM",
            EmailConfirmed = true,
            PasswordHash = passwordHasher.HashPassword(null, "UWillBeHired"),
            SecurityStamp = Guid.NewGuid().ToString()
        };
        
        ApiUser user2 = new ApiUser
        {
            Id = 2,
            UserName = "ArtyomTitorenko2",
            NormalizedUserName = "ARTYOMTITORENKO2",
            Email = "test@gmail.com",
            NormalizedEmail = "TEST@GMAIL.COM",
            EmailConfirmed = true,
            PasswordHash = passwordHasher.HashPassword(null, "UWillBeHired2"),
            SecurityStamp = Guid.NewGuid().ToString()
        };
        
        builder.HasData(user1, user2);
    }
}