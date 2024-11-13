using Microsoft.Extensions.Configuration;

namespace NextStop.Common;

/// <summary>
/// Utility class for managing configuration settings.
/// </summary>
public class ConfigurationUtil
{
    /// <summary>
    /// Static variable that stores the loaded configuration.
    /// It is initialized only once and reused for future calls.
    /// </summary>
    private static IConfiguration? configuration = null;

    /// <summary>
    /// Loads the configuration from the "appsettings.json" file and returns it.
    /// The configuration is loaded only once (Lazy Initialization) and reused for future calls.
    /// </summary>
    /// <returns>The loaded <see cref="IConfiguration"/> instance.</returns>
    public static IConfiguration GetConfiguration() =>
        configuration ??= new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false)
            .Build();

    /// <summary>
    /// Overloaded method that retrieves connection parameters from the configuration.
    /// It first retrieves the configuration and then searches for the specified keys.
    /// </summary>
    /// <param name="connectionConfigName">The key for the connection string.</param>
    /// <param name="providerConfigName">The key for the provider name.</param>
    /// <returns>A tuple containing the connection string and provider name.</returns>
    public static (string ConnectionString, string ProviderName) GetConnectionParameters(string connectionConfigName, string providerConfigName)
    {
        return GetConnectionParameters(GetConfiguration(), connectionConfigName, providerConfigName);
    }

    /// <summary>
    /// Retrieves the connection string and provider name from the configuration.
    /// Used to configure the database connection.
    /// </summary>
    /// <param name="configuration">The <see cref="IConfiguration"/> instance to retrieve settings from.</param>
    /// <param name="connectionConfigName">The key for the connection string.</param>
    /// <param name="providerConfigName">The key for the provider name.</param>
    /// <returns>A tuple containing the connection string and provider name.</returns>
    /// <exception cref="ArgumentException">
    /// Thrown when the connection string or provider name does not exist in the configuration.
    /// </exception>
    public static (string ConnectionString, string ProviderName) GetConnectionParameters(IConfiguration configuration, string connectionConfigName, string providerConfigName)
    {
        // Searches for the connection string with the specified key.
        var connectionString = configuration.GetConnectionString(connectionConfigName);
        if (connectionString is null)
        {
            // Throws an exception if the connection string does not exist.
            throw new ArgumentException($"Connection string with key '{connectionConfigName}' does not exist");
        }

        // Searches for the provider name in the configuration.
        var providerName = configuration[providerConfigName];
        if (providerName is null)
        {
            // Throws an exception if the provider key does not exist.
            throw new ArgumentException($"Configuration property '{providerConfigName}' does not exist");
        }

        // Returns the connection string and provider name.
        return (connectionString, providerName);
    }
}
