using NextStop.Api.DTOs;
using NextStop.Domain;
using Riok.Mapperly.Abstractions;

namespace NextStop.Api.Mappers;

[Mapper]
public static partial class ConnectionMapper
{
    /// <summary>
    /// Maps a <see cref="Connection"/> domain object to a <see cref="ConnectionDto"/>.
    /// </summary>
    /// <param name="connection">The <see cref="Connection"/> object to map.</param>
    /// <returns>A <see cref="ConnectionDto"/> containing the mapped data.</returns>
    public static partial ConnectionDto ToConnectionDto(this Connection connection);
    
    /// <summary>
    /// Maps a <see cref="ConnectionDto"/> data transfer object to a <see cref="Connection"/> domain object.
    /// </summary>
    /// <param name="connectionDto">The <see cref="ConnectionDto"/> to map.</param>
    /// <returns>A <see cref="Connection"/> object containing the mapped data.</returns>
    public static partial Connection ToConnection(this ConnectionDto connectionDto);
}