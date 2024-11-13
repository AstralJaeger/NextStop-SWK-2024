using NextStop.Domain;

namespace NextStop.Dal.Simple;

/// <summary>
/// A simple implementation of ITripCheckInDAO that stores data in a static in-memory list.
/// </summary>
public class SimpleTripCheckInDao
{
    // Static list to hold trip check-in data, simulating a database.
    private static IList<TripCheckin> tripCheckinList = new List<TripCheckin>
    {
        new TripCheckin 
        { 
            Id = 1, 
            TripId = 1, 
            StopPointId = 1, 
            Checkin = DateTime.Now.AddMinutes(-30),
            Trip = new Trip { Id = 1, RouteId = 1, VehicleId = 101 },
            StopPoint = new StopPoint { Id = 1, Name = "Hauptbahnhof" }
        },
        new TripCheckin 
        { 
            Id = 2, 
            TripId = 1, 
            StopPointId = 2, 
            Checkin = DateTime.Now.AddMinutes(-15),
            Trip = new Trip { Id = 1, RouteId = 1, VehicleId = 101 },
            StopPoint = new StopPoint { Id = 2, Name = "Uferpromenade" }
        }
    };

    /// <summary>
    /// Retrieves a trip check-in by its unique ID.
    /// </summary>
    /// <param name="id">The unique ID of the trip check-in.</param>
    /// <returns>The trip check-in object with the specified ID, or null if not found.</returns>
    public TripCheckin GetById(int id)
    {
        return null; //todo
    }

    /// <summary>
    /// Retrieves all trip check-ins from the list.
    /// </summary>
    /// <returns>A list of all trip check-in objects.</returns>
    public List<TripCheckin> GetAll()
    {
        return tripCheckinList.ToList();
    }

    /// <summary>
    /// Retrieves all check-ins for a specific trip.
    /// </summary>
    /// <param name="tripId">The ID of the trip.</param>
    /// <returns>A list of trip check-ins for the specified trip.</returns>
    public List<TripCheckin> GetCheckInsByTripId(int tripId)
    {
        return tripCheckinList.Where(tc => tc.TripId == tripId).ToList();
    }

    /// <summary>
    /// Inserts a new trip check-in into the list.
    /// </summary>
    /// <param name="tripCheckin">The trip check-in object to insert.</param>
    public void Insert(TripCheckin tripCheckin)
    {
        tripCheckin.Id = tripCheckinList.Max(tc => tc.Id) + 1; // Generate a new ID based on the current maximum
        tripCheckinList.Add(tripCheckin);
    }

    /// <summary>
    /// Updates an existing trip check-in in the list.
    /// </summary>
    /// <param name="tripCheckin">The trip check-in object with updated information.</param>
    public void Update(TripCheckin tripCheckin)
    {
        var existingTripCheckin = GetById(tripCheckin.Id);
        if (existingTripCheckin != null)
        {
            existingTripCheckin.TripId = tripCheckin.TripId;
            existingTripCheckin.StopPointId = tripCheckin.StopPointId;
            existingTripCheckin.Checkin = tripCheckin.Checkin;
            existingTripCheckin.Trip = tripCheckin.Trip;
            existingTripCheckin.StopPoint = tripCheckin.StopPoint;
        }
    }

    /// <summary>
    /// Deletes a trip check-in from the list by its unique ID.
    /// </summary>
    /// <param name="id">The unique ID of the trip check-in to delete.</param>
    public void Delete(int id)
    {
        var tripCheckin = GetById(id);
        if (tripCheckin != null)
        {
            tripCheckinList.Remove(tripCheckin);
        }
    }
}
