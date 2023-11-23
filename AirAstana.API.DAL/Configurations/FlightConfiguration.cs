using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AirAstana.API.DAL.Configurations;

public class FlightConfiguration : IEntityTypeConfiguration<Flight>
{
    public void Configure(EntityTypeBuilder<Flight> builder)
    {
        builder.Property(f => f.Origin)
            .HasMaxLength(256)
            .IsRequired();
        
        builder.Property(f => f.Destination)
            .HasMaxLength(256)
            .IsRequired();
        
        builder.Property(f => f.Departure)
            .IsRequired();
        
        builder.Property(f => f.Arrival)
            .IsRequired();
        
        builder.Property(f => f.Status)
            .IsRequired();
        
        builder.HasData(
            new Flight
            {
                Id = 1,
                Origin = "Almaty",
                Destination = "Nur-Sultan",
                Departure = new DateTimeOffset(2021, 10, 10, 10, 10, 10, TimeSpan.Zero),
                Arrival = new DateTimeOffset(2021, 10, 10, 12, 10, 10, TimeSpan.Zero),
                Status = Status.InTime
            },
            new Flight
            {
                Id = 2,
                Origin = "Almaty",
                Destination = "Nur-Sultan",
                Departure = new DateTimeOffset(2021, 10, 10, 10, 10, 10, TimeSpan.Zero),
                Arrival = new DateTimeOffset(2021, 10, 10, 12, 10, 10, TimeSpan.Zero),
                Status = Status.InTime
            },
            new Flight
            {
                Id = 3,
                Origin = "Almaty",
                Destination = "Nur-Sultan",
                Departure = new DateTimeOffset(2021, 10, 10, 10, 10, 10, TimeSpan.Zero),
                Arrival = new DateTimeOffset(2021, 10, 10, 12, 10, 10, TimeSpan.Zero),
                Status = Status.InTime
            },
            new Flight
            {
                Id = 4,
                Origin = "Almaty",
                Destination = "Nur-Sultan",
                Departure = new DateTimeOffset(2021, 10, 10, 10, 10, 10, TimeSpan.Zero),
                Arrival = new DateTimeOffset(2021, 10, 10, 12, 10, 10, TimeSpan.Zero),
                Status = Status.InTime
            });
    }
}