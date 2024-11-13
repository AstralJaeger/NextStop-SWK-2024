using NextStop.Dal.Interface;
using NextStop.Domain;

namespace NextStop.Dal.Ado;

public class TripDAO : ITripDAO
{
    public Task<int> InsertTripAsync(Trip trip)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateTripAsync(Trip trip)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteTripAsync(int tripId)
    {
        throw new NotImplementedException();
    }

    public Task<Trip?> GetTripByIdAsync(int tripId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Trip>> GetAllTripsAsync()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Trip>> GetTripsByDateAsync(DateTime date)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Trip>> GetOngoingTripsAsync()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Trip>> GetTripsByRouteIdAsync(int routeId)
    {
        throw new NotImplementedException();
    }
}