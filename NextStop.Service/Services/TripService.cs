using NextStop.Domain;
using NextStop.Service.Interfaces;

namespace NextStop.Service.Services;

public class TripService : ITripService
{
    public Task<IEnumerable<Trip>> GetAllTripsAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Trip> GetTripByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Trip> GetTripByRouteAsync(int routeId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> InsertTripAsync(Trip trip)
    {
        throw new NotImplementedException();
    }
}