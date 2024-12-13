using NextStop.Domain;
using NextStop.Service.Interfaces;

namespace NextStop.Service.Services;

public class TripCheckInService : ITripCheckInService
{
    public Task<IEnumerable<TripCheckin>> GetAllTripCheckInsAsync()
    {
        throw new NotImplementedException();
    }

    public Task<TripCheckin> GetTripCheckInByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<TripCheckin> GetTripCheckInsByTrip(Trip trip)
    {
        throw new NotImplementedException();
    }

    public Task<TripCheckin> GetTripCheckInsByStopPoint(StopPoint stopPoint)
    {
        throw new NotImplementedException();
    }

    public Task<TripCheckin> GetTripCheckInByCheckIn(DateTime checkIn)
    {
        throw new NotImplementedException();
    }

    public Task<bool> InsertTripCheckInAsync(TripCheckin checkIn)
    {
        throw new NotImplementedException();
    }
}