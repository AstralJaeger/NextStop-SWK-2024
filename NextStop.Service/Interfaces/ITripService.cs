using NextStop.Domain;

namespace NextStop.Service.Interfaces;

public interface ITripService
{
    public Task<IEnumerable<Trip>> GetAllTripsAsync();
    
    public Task<Trip> GetTripByIdAsync(int id);
    
    public Task<Trip> GetTripByRouteAsync(int routeId);

    public Task<bool> InsertTripAsync(Trip trip);

}