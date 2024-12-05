using System.Net;
using NextStop.Dal.Ado;
using NextStop.Dal.Interface;
using NextStop.Domain;
using NextStop.ServiceInterface;

namespace NextStop.Service;

public class StopPointService(IStopPointDao stopPointDao): IEndPointService
{
    private readonly IStopPointDao stopPointDao = stopPointDao;
    public async Task<IEnumerable<StopPoint>> GetAllAsync()
    {
        IEnumerable<StopPoint> endpoints = await stopPointDao.GetAllAsync();
        return endpoints;
        
    }
}