using NextStop.Domain;

namespace NextStop.Service.Interfaces;

public interface IRouteService
{
    public Task<IEnumerable<Route>> GetAllRoutesAsync();
    
    public Task<Route> GetRouteByIdAsync(int id);
    
    public Task<Route> GetRouteByNameAsync(string name); 
    
    //public Task<IEnumerable<Route>> GetRoutesByValidityDayAsync(int v);
    public Task<IEnumerable<Route>> GetRoutesByValidToAsync(DateTime validToDate);
    public Task<IEnumerable<Route>> GetRoutesByValidFromAsync(DateTime validFromDate);
    
    Task<bool> RouteAlreadyExist(int routeId);
    
    Task InsertRouteAsync(Route route);
}