using AirAstana.API.DAL.Configurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace AirAstana.API.DAL;

public class AirAstanaDbContext : IdentityDbContext<ApiUser, ApiRole, int>
{
    public AirAstanaDbContext(DbContextOptions options) : base(options)
    {
        
    }
    
    public DbSet<Flight> Flights { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // User has unique username
        modelBuilder.Entity<ApiUser>().HasIndex(u => u.UserName).IsUnique();
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        
        // Role has unique code
        modelBuilder.Entity<ApiRole>().HasIndex(r => r.Code).IsUnique();
        modelBuilder.ApplyConfiguration(new RoleConfiguration());
        
        modelBuilder.ApplyConfiguration(new UserRoleConfiguration());
        
        modelBuilder.ApplyConfiguration(new FlightConfiguration());
    }
}


// was made to be able to run scaffolding controllers
public class AirAstanaDbContextFactory : IDesignTimeDbContextFactory<AirAstanaDbContext>
{
    public AirAstanaDbContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var connectionString = configuration.GetConnectionString("AirAstanaConnection");

        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException("Could not find a connection string named 'AirAstanaConnection'.");
        }

        var optionsBuilder = new DbContextOptionsBuilder<AirAstanaDbContext>();
        optionsBuilder.UseNpgsql(connectionString);

        return new AirAstanaDbContext(optionsBuilder.Options);
    }
}