using NextStop.Domain;

namespace NextStop.Service.Interfaces;

public interface IRouteStopPointService
{
    public Task<IEnumerable<RouteStopPoint>> GetAllRouteStopPointsAsync();
    
    public Task<RouteStopPoint?> GetRouteStopPointByIdAsync(int id);
    
    //public Task<IEnumerable<RouteStopPoint>> GetRouteStopPointsByStopPointAsync(int stopPointId);

    public Task<IEnumerable<RouteStopPoint>> GetRouteStopPointsByRouteIdAsync(int routeId);
    
    public Task<IEnumerable<RouteStopPoint>> GetRouteStopPointsByRouteNameAsync(string routeName);

    
    public Task<RouteStopPoint?> GetRouteStopPointByArrivalTimeAsync(DateTime arrivalTime);
    
    public Task<RouteStopPoint?> GetRouteStopPointByDepartureTimeAsync(DateTime departureTime);
    
    public Task InsertRouteStopPointAsync(RouteStopPoint routeStopPoint);


    Task<bool> RouteStopPointAlreadyExists(int routeStopPointDto);
    // Task<IEnumerable<RouteStopPoint>> GetRouteBetweenStopPointsAsync(string startStopPointName, string endStopPointNam);
    Task<bool> IsSameRouteForRouteStopPoints(string startStopPointName, string endStopPointName);
}