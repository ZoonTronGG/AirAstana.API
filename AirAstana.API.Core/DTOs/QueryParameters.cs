namespace AirAstana.API.Core.DTOs;

public class QueryParameters
{
    public int StartIndex { get; set; } = 0;
    
    public int PageNumber { get; set; }
    
    public int PageSize { get; set; } = 10;
}