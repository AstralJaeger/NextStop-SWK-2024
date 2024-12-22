using NextStop.Domain;

namespace NextStop.Dal.Interface;


/// <summary>
/// Interface for finding routes operations.
/// Provides methods for finding routing data.
/// </summary>
public interface IRoutingService
{
    /// <summary>
    /// Retrieves all routes from the system.
    /// </summary>
    /// <returns>A collection of all <see cref="Holiday"/> objects.</returns>
    public Task<IList<Connection>> GetConnectionAtTimeAsync(int startId, int endId, DateTime time);

}