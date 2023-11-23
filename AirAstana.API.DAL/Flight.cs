using System.ComponentModel.DataAnnotations;

namespace AirAstana.API.DAL;

public class Flight
{
    public int Id { get; set; }
    
    [StringLength(256)]
    public string Origin { get; set; } = string.Empty;
    
    [StringLength(256)]
    public string Destination { get; set; } = string.Empty;
    
    public DateTimeOffset Departure { get; set; }
    
    public DateTimeOffset Arrival { get; set; }
    
    public Status Status { get; set; }
}