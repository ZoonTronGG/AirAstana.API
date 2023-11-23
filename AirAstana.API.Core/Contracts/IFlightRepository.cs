using AirAstana.API.Core.DTOs;
using AirAstana.API.Core.DTOs.Flight;
using AirAstana.API.DAL;

namespace AirAstana.API.Core.Contracts;

public interface IFlightRepository : IGenericRepository<Flight>
{
    Task<PagedResult<FlightDto>> GetFlightsSortedByArrivalAsync(QueryParameters queryParameters);
    IQueryable<FlightDto> GetFlightDtosQueryable();
}