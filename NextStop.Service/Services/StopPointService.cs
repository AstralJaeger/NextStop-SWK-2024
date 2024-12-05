using System.Net;
using NextStop.Dal.Ado;
using NextStop.Dal.Interface;
using NextStop.Domain;
using NextStop.Service.Interfaces;
using NextStop.ServiceInterface;

namespace NextStop.Service;

public class StopPointService(IStopPointDao stopPointDao): IStopPointService
{
    private readonly IStopPointDao stopPointDao = stopPointDao;
    public async Task<IEnumerable<StopPoint>> GetAllAsync()
    {
        IEnumerable<StopPoint> endpoints = await stopPointDao.GetAllAsync();
        return endpoints;
        
    }

    public async Task<StopPoint> GetStopPointByIdAsync(int id)
    {
        //todo Fehlerbehandlung
        StopPoint stopPpoint = await stopPointDao.GetByIdAsync(id);
        return stopPpoint;
    }
}