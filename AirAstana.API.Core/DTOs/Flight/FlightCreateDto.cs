using System.ComponentModel.DataAnnotations;
using AirAstana.API.DAL;

namespace AirAstana.API.Core.DTOs.Flight;

public class FlightCreateDto
{
    [Required]
    [StringLength(256)]
    public string Origin { get; set; } = string.Empty;
    
    [Required]
    [StringLength(256)]
    public string Destination { get; set; } = string.Empty;
    
    [Required]
    public DateTimeOffset Departure { get; set; }
    
    [Required]
    public DateTimeOffset Arrival { get; set; }
    
    [Required]
    public Status Status { get; set; }
}