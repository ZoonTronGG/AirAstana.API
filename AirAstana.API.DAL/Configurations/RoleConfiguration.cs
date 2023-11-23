using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AirAstana.API.DAL.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<ApiRole>
{
    public void Configure(EntityTypeBuilder<ApiRole> builder)
    {
        builder.HasData(
            new ApiRole { Id = 1, Name = "Moderator", NormalizedName = "MODERATOR", Code = "MDR"},
            new ApiRole { Id = 2, Name = "User", NormalizedName = "USER", Code = "USR"}
        );
    }
}