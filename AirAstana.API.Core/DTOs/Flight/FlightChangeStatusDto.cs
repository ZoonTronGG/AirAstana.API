using AirAstana.API.DAL;
using Microsoft.Build.Framework;

namespace AirAstana.API.Core.DTOs.Flight;

public class FlightChangeStatusDto
{
    [Required]
    public Status Status { get; set; }
}