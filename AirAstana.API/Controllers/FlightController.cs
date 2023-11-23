using AirAstana.API.Core.Contracts;
using AirAstana.API.Core.DTOs.Flight;
using AirAstana.API.DAL;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.EntityFrameworkCore;

namespace AirAstana.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FlightController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IFlightRepository _flightsRepository;
    private readonly ILogger<FlightController> _logger;

    public FlightController(IFlightRepository flightsRepository, IMapper mapper, ILogger<FlightController> logger)
    {
        _flightsRepository = flightsRepository;
        _mapper = mapper;
        _logger = logger;
    }

    // GET: api/Flight
    [HttpGet]
    [Authorize]
    [EnableQuery]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public IActionResult GetFlights()
    {
        var flightsQuery = _flightsRepository.GetFlightDtosQueryable();
        return Ok(flightsQuery);
    }
    

    // PUT: api/Flight/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id:int}")]
    [Authorize(Roles = "Moderator")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PutFlight(int id, FlightChangeStatusDto dto)
    {
        var flight = await _flightsRepository.GetByIdAsync(id);
        _mapper.Map(dto, flight);
        try
        {
            await _flightsRepository.UpdateAsync(flight);
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await FlightExist(id))
            {
                return NotFound();
            }
            
            throw;
        }

        return NoContent();
    }

    // POST: api/Flight
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    [Authorize(Roles = "Moderator")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<Flight>> PostFlight(FlightCreateDto dto)
    {
        await _flightsRepository.AddAsync<FlightCreateDto, FlightDto>(dto);
        _logger.LogInformation($"Flight from {dto.Departure} to {dto.Destination} was created");
        return Ok();
    }

    private async Task<bool> FlightExist(int id)
    {
        return await _flightsRepository.ExistsAsync(id);
    }
}