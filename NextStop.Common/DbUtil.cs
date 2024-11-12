using System.Data.Common;
using Npgsql; 

namespace NextStop.Common;

/// <summary>
/// Utility class for registering ADO.NET providers.
/// </summary>
public static class DbUtil
{
    /// <summary>
    /// Registers ADO.NET providers for database connections.
    /// This method registers the PostgreSQL provider to allow the application to connect to PostgreSQL databases.
    /// </summary>
    public static void RegisterAdoProviders()
    {
        // Registers the PostgreSQL provider.
        // "Npgsql" is the name of the provider, and the factory instance is used to create connections.
        DbProviderFactories.RegisterFactory("Npgsql", NpgsqlFactory.Instance);
    }
}