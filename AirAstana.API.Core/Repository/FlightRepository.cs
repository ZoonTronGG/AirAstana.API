using AirAstana.API.Core.Contracts;
using AirAstana.API.Core.DTOs;
using AirAstana.API.Core.DTOs.Flight;
using AirAstana.API.DAL;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace AirAstana.API.Core.Repository;

public class FlightRepository : GenericRepository<Flight>, IFlightRepository
{
    public FlightRepository(AirAstanaDbContext db, IMapper mapper) : base(db, mapper)
    {
    }

    public async Task<PagedResult<FlightDto>> GetFlightsSortedByArrivalAsync(QueryParameters queryParameters)
    {
        var flightsPagedResult = await GetAllAsync<FlightDto>(queryParameters);
        
        if (flightsPagedResult.Items != null)
            flightsPagedResult.Items = flightsPagedResult.Items.OrderBy(f => f.Arrival).ToList();

        return flightsPagedResult;
    }

    public IQueryable<FlightDto> GetFlightDtosQueryable()
    {
        return Db.Flights.ProjectTo<FlightDto>(Mapper.ConfigurationProvider);
    }
}