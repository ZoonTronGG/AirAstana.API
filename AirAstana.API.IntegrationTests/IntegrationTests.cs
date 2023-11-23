using AirAstana.API.Controllers;
using AirAstana.API.Core.Configuration;
using AirAstana.API.Core.DTOs.Flight;
using AirAstana.API.Core.Repository;
using AirAstana.API.DAL;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AirAstana.API.IntegrationTests;

public class IntegrationTests
{
    private readonly FlightController _controller;
    private readonly DbContextOptions<AirAstanaDbContext> _dbContextOptions;

    public IntegrationTests()
    {
        // Set up DbContext with in-memory database
        _dbContextOptions = new DbContextOptionsBuilder<AirAstanaDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;

        // Set up AutoMapper
        var mapperConfig = new MapperConfiguration(mc => {
            mc.AddProfile(new MapperConfig()); // Replace with your actual AutoMapper profile
        });
        var mapper = mapperConfig.CreateMapper();

        // Create instances of your repository and logger
        var repository = new FlightRepository(new AirAstanaDbContext(_dbContextOptions), mapper);
        var logger = LoggerFactory.Create(builder => builder.AddConsole()).CreateLogger<FlightController>();

        // Instantiate the controller
        _controller = new FlightController(repository, mapper, logger);
    }

    [Fact]
    public async Task GetFlights_ReturnsFlights_WhenCalled()
    {
        // Arrange - Add test data to the in-memory database
        await using (var context = new AirAstanaDbContext(_dbContextOptions))
        {
            // Add test data to the context
            context.Flights.Add(new Flight
            {
                Id = 1,
                Arrival = DateTimeOffset.Now,
                Departure = DateTimeOffset.Now,
                Destination = "Test",
                Origin = "Test",
                Status = Status.Delayed
            });
            await context.SaveChangesAsync();
        }

        // Act
        var result = _controller.GetFlights();

        // Assert
        var actionResult = Assert.IsType<OkObjectResult>(result);
        var flights = Assert.IsAssignableFrom<IQueryable<FlightDto>>(actionResult.Value);
        var flightList = flights.ToList();
        Assert.NotEmpty(flightList);
    }
}