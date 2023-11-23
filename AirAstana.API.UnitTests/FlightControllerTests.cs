using AirAstana.API.Controllers;
using AirAstana.API.Core.Contracts;
using AirAstana.API.Core.DTOs.Flight;
using AirAstana.API.DAL;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

public class FlightControllerTests
{
    [Fact]
    public void GetFlights_ReturnsOkResult_WithFlights()
    {
        // Arrange
        var mockRepo = new Mock<IFlightRepository>();
        var mockMapper = new Mock<IMapper>();
        var mockLogger = new Mock<ILogger<FlightController>>();
        var controller = new FlightController(mockRepo.Object, mockMapper.Object, mockLogger.Object);

        // Act
        var result = controller.GetFlights();

        // Assert
        var actionResult = Assert.IsType<OkObjectResult>(result);
    }
    
    [Fact]
    public async Task PutFlight_ReturnsNoContent_WhenUpdateIsSuccessful()
    {
        // Arrange
        var mockRepo = new Mock<IFlightRepository>();
        var mockMapper = new Mock<IMapper>();
        var mockLogger = new Mock<ILogger<FlightController>>();
        var controller = new FlightController(mockRepo.Object, mockMapper.Object, mockLogger.Object);
    
        // Mock the repository and mapper behavior
        mockRepo.Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(new Flight());

        // Act
        var result = await controller.PutFlight(1, new FlightChangeStatusDto());

        // Assert
        Assert.IsType<NoContentResult>(result);
    }
}