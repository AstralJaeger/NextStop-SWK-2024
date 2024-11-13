using System.Data.Common;
using Microsoft.Extensions.Configuration;

namespace NextStop.Common;

/// <summary>
/// Provides a default implementation of the <see cref="IConnectionFactory"/> interface.
/// </summary>
public class DefaultConnectionFactory : IConnectionFactory
{
    
	/// <summary>
    /// The database provider factory used to create database connections.
    /// </summary>

	private readonly DbProviderFactory dbProviderFactory;

    /// <summary>
    /// Creates an instance of <see cref="DefaultConnectionFactory"/> based on configuration data.
    /// </summary>
    /// <param name="configuration">The <see cref="IConfiguration"/> instance that contains the connection details.</param>
    /// <param name="connectionConfigName">The key name for the connection string.</param>
    /// <param name="providerConfigName">The key name for the provider name.</param>
    /// <returns>An instance of <see cref="IConnectionFactory"/> initialized with the connection settings.</returns>
    public static IConnectionFactory FromConfiguration(IConfiguration configuration, string connectionConfigName, string providerConfigName)
    {
        (string connectionString, string providerName) =
          ConfigurationUtil.GetConnectionParameters(configuration, connectionConfigName, providerConfigName);
        return new DefaultConnectionFactory(connectionString, providerName);
    }

    /// <summary>
    /// Constructor that creates a new instance of <see cref="DefaultConnectionFactory"/> with connection details.
    /// </summary>
    /// <param name="connectionString">The connection string for the database.</param>
    /// <param name="providerName">The name of the database provider being used.</param>
    public DefaultConnectionFactory(string connectionString, string providerName)
    {
        this.ConnectionString = connectionString;
        this.ProviderName = providerName;

        DbUtil.RegisterAdoProviders();
        this.dbProviderFactory = DbProviderFactories.GetFactory(providerName);
    }

    /// <summary>
    /// Gets the connection string used to establish a database connection.
    /// </summary>
    public string ConnectionString { get; }

    /// <summary>
    /// Gets the name of the database provider (e.g., "Microsoft.Data.SqlClient" for SQL Server).
    /// </summary>
    public string ProviderName { get; }

    /// <summary>
    /// Asynchronously creates a new database connection.
    /// </summary>
    /// <returns>A <see cref="Task{DbConnection}"/> that returns a database connection once it is created and opened.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the connection could not be created.</exception>
    public async Task<DbConnection> CreateConnectionAsync()
    {
        DbConnection? connection = dbProviderFactory.CreateConnection();
        if (connection is null)
        {
            throw new InvalidOperationException(
                "DbProviderFactory.CreateConnection() returned null");
        }
        connection.ConnectionString = ConnectionString;
        await connection.OpenAsync(); // Asynchronously opens the connection

        return connection;
    }
}
