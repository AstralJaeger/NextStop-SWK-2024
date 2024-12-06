using NextStop.Dal.Interface;
using NextStop.Domain;
using NextStop.Service.Interfaces;

namespace NextStop.Service;

public class StopPointService(IStopPointDao stopPointDao): IStopPointService
{
    private readonly IStopPointDao stopPointDao = stopPointDao;
    public async Task<IEnumerable<StopPoint>> GetAllStopPointsAsync()
    {
        IEnumerable<StopPoint> endpoints = await stopPointDao.GetAllStopPointsAsync();
        return endpoints;
        
    }

    public async Task<StopPoint> GetStopPointByIdAsync(int id)
    {
        //todo Fehlerbehandlung
        StopPoint stopPpoint = await stopPointDao.GetByIdAsync(id);
        return stopPpoint;
    }
}