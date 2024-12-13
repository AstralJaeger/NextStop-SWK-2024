using NextStop.Domain;

namespace NextStop.Service.Interfaces;

public interface ITripCheckInService
{
    public Task<IEnumerable<TripCheckin>> GetAllTripCheckInsAsync();
    
    public Task<TripCheckin> GetTripCheckInByIdAsync(int id);
    
    public Task<TripCheckin> GetTripCheckInsByTrip(Trip trip);
    
    public Task<TripCheckin> GetTripCheckInsByStopPoint(StopPoint stopPoint);
    
    public Task<TripCheckin> GetTripCheckInByCheckIn(DateTime checkIn);
    
    public Task<bool> InsertTripCheckInAsync(TripCheckin checkIn);


}