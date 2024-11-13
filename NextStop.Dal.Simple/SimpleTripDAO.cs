using NextStop.Dal.Interface;
using NextStop.Domain;
using System.Collections.Generic;
using System.Linq;

namespace NextStop.Dal.Simple;

/// <summary>
/// A simple implementation of ITripDAO that stores data in a static in-memory list.
/// </summary>
public class SimpleTripDAO
{
    // Static list to hold trip data, simulating a database.
    private static IList<Trip> tripList = new List<Trip>
    {
        new Trip 
        { 
            Id = 1, 
            RouteId = 1, 
            VehicleId = 101, 
            Route = new Route { Id = 1, Name = "Green" },
            TripCheckins = new List<TripCheckin>
            {
                new TripCheckin { Id = 1, TripId = 1, StopPointId = 1, Checkin = DateTime.Now.AddMinutes(-15) },
                new TripCheckin { Id = 2, TripId = 1, StopPointId = 2, Checkin = DateTime.Now }
            }
        },
        new Trip 
        { 
            Id = 2, 
            RouteId = 2, 
            VehicleId = 102, 
            Route = new Route { Id = 2, Name = "Glue" },
            TripCheckins = new List<TripCheckin>
            {
                new TripCheckin { Id = 3, TripId = 2, StopPointId = 3, Checkin = DateTime.Now.AddMinutes(-10) }
            }
        }
    };

    /// <summary>
    /// Retrieves a trip by its unique ID.
    /// </summary>
    /// <param name="id">The unique ID of the trip.</param>
    /// <returns>The trip object with the specified ID, or null if not found.</returns>
    public Trip GetById(int id)
    {
        return null; //todo
    }

    /// <summary>
    /// Retrieves all trips from the list.
    /// </summary>
    /// <returns>A list of all trip objects.</returns>
    public List<Trip> GetAll()
    {
        return tripList.ToList();
    }

    /// <summary>
    /// Inserts a new trip into the list.
    /// </summary>
    /// <param name="trip">The trip object to insert.</param>
    public void Insert(Trip trip)
    {
        trip.Id = tripList.Max(t => t.Id) + 1; // Generate a new ID based on the current maximum
        tripList.Add(trip);
    }

    /// <summary>
    /// Updates an existing trip in the list.
    /// </summary>
    /// <param name="trip">The trip object with updated information.</param>
    public void Update(Trip trip)
    {
        var existingTrip = GetById(trip.Id);
        if (existingTrip != null)
        {
            existingTrip.RouteId = trip.RouteId;
            existingTrip.VehicleId = trip.VehicleId;
            existingTrip.Route = trip.Route;
            existingTrip.TripCheckins = trip.TripCheckins;
        }
    }

    /// <summary>
    /// Deletes a trip from the list by its unique ID.
    /// </summary>
    /// <param name="id">The unique ID of the trip to delete.</param>
    public void Delete(int id)
    {
        var trip = GetById(id);
        if (trip != null)
        {
            tripList.Remove(trip);
        }
    }
}
