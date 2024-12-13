using NextStop.Domain;

namespace NextStop.Service.Interfaces;

public interface ITripService
{
    // CREATE-Methods
    public Task InsertTripAsync(Trip trip);
    //------------------------------------------------------------------------------

    // READ-Methods
    public Task<IEnumerable<Trip>> GetAllTripsAsync();

    public Task<Trip?> GetTripByIdAsync(int id);

    public Task<IEnumerable<Trip>> GetTripsByRouteIdAsync(int routeId);

    public Task<IEnumerable<Trip>> GetTripsByVehicleIdAsync(int vehicleId);
    
    public Task<bool> TripAlreadyExists(int id);
}