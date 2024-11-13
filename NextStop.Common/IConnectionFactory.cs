using System.Data.Common;

namespace NextStop.Common;

/// <summary>
/// Interface for creating database connections.
/// </summary>
public interface IConnectionFactory
{
    /// <summary>
    /// Gets the connection string used to establish a database connection.
    /// </summary>
    string ConnectionString { get; }

    /// <summary>
    /// Gets the name of the database provider (e.g., Microsoft.Data.SqlClient).
    /// </summary>
    string ProviderName { get; }

    /// <summary>
    /// Asynchronously creates a new database connection.
    /// </summary>
    /// <returns>A <see cref="Task{DbConnection}"/> that returns the connection once it has been created.</returns>
    Task<DbConnection> CreateConnectionAsync();
}