﻿using NextStop.Dal.Interface;
using NextStop.Domain;

namespace NextStop.Dal.Simple;

/// <summary>
/// A simple implementation of IRouteStopPointDAO that stores data in a static in-memory list.
/// </summary>
public class SimpleRouteStopPointDao
{
    // Static list to hold route stop point data, simulating a database.
    private static IList<RouteStopPoint> routeStopPointList = new List<RouteStopPoint>
    {
        new RouteStopPoint
        {
            Id = 1,
            StopPointId = 1,
            RouteId = 1,
            ArrivalTime = DateTime.Now.AddMinutes(10),
            DepartureTime = DateTime.Now.AddMinutes(15),
            Order = 1
        },
        new RouteStopPoint
        {
            Id = 2,
            StopPointId = 2,
            RouteId = 1,
            ArrivalTime = DateTime.Now.AddMinutes(20),
            DepartureTime = DateTime.Now.AddMinutes(25),
            Order = 2
        }
    };

    /// <summary>
    /// Retrieves a route stop point by its unique ID.
    /// </summary>
    /// <param name="id">The unique ID of the route stop point.</param>
    /// <returns>The route stop point object with the specified ID, or null if not found.</returns>
    public RouteStopPoint GetById(int id)
    {
        return null; //todo
    }

    /// <summary>
    /// Retrieves all route stop points from the list.
    /// </summary>
    /// <returns>A list of all route stop point objects.</returns>
    public List<RouteStopPoint> GetAll()
    {
        return routeStopPointList.ToList();
    }

    /// <summary>
    /// Retrieves all stop points for a specific route.
    /// </summary>
    /// <param name="routeId">The ID of the route.</param>
    /// <returns>A list of route stop points for the specified route.</returns>
    public List<RouteStopPoint> GetStopPointsByRouteId(int routeId)
    {
        return routeStopPointList.Where(rsp => rsp.RouteId == routeId).OrderBy(rsp => rsp.Order).ToList();
    }

    /// <summary>
    /// Inserts a new route stop point into the list.
    /// </summary>
    /// <param name="routeStopPoint">The route stop point object to insert.</param>
    public void Insert(RouteStopPoint routeStopPoint)
    {
        routeStopPoint.Id = routeStopPointList.Max(rsp => rsp.Id) + 1; // Generate a new ID based on the current maximum
        routeStopPointList.Add(routeStopPoint);
    }

    /// <summary>
    /// Updates an existing route stop point in the list.
    /// </summary>
    /// <param name="routeStopPoint">The route stop point object with updated information.</param>
    public void Update(RouteStopPoint routeStopPoint)
    {
        var existingRouteStopPoint = GetById(routeStopPoint.Id);
        if (existingRouteStopPoint != null)
        {
            existingRouteStopPoint.StopPointId = routeStopPoint.StopPointId;
            existingRouteStopPoint.RouteId = routeStopPoint.RouteId;
            existingRouteStopPoint.ArrivalTime = routeStopPoint.ArrivalTime;
            existingRouteStopPoint.DepartureTime = routeStopPoint.DepartureTime;
            existingRouteStopPoint.Order = routeStopPoint.Order;
            existingRouteStopPoint.StopPoint = routeStopPoint.StopPoint;
            existingRouteStopPoint.Route = routeStopPoint.Route;
        }
    }

    /// <summary>
    /// Deletes a route stop point from the list by its unique ID.
    /// </summary>
    /// <param name="id">The unique ID of the route stop point to delete.</param>
    public void Delete(int id)
    {
        var routeStopPoint = GetById(id);
        if (routeStopPoint != null)
        {
            routeStopPointList.Remove(routeStopPoint);
        }
    }

}