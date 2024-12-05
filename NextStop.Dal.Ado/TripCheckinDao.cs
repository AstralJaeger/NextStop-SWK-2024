using NextStop.Common;
using NextStop.Domain;

namespace NextStop.Dal.Ado;

public class TripCheckinDao : ITripCheckinDao
{
    public Task<int> InsertTripCheckInAsync(TripCheckin tripCheckIn)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateTripCheckInAsync(TripCheckin tripCheckIn)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteTripCheckInAsync(int checkInId)
    {
        throw new NotImplementedException();
    }

    public Task<TripCheckin?> GetTripCheckInByIdAsync(int checkInId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<TripCheckin>> GetTripCheckInsByTripIdAsync(int tripId)
    {
        throw new NotImplementedException();
    }

    public Task<double> CalculateAverageDelayForTripAsync(int tripId)
    {
        throw new NotImplementedException();
    }
}