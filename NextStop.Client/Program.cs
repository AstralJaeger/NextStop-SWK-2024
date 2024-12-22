using Microsoft.Extensions.Configuration;
using NextStop.Common;
using NextStop.Dal.Ado;
using NextStop.Domain;

namespace NextStop.Client;

static class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Hello World!");
        // IConfiguration configuration = ConfigurationUtil.GetConfiguration();
        // IConnectionFactory connectionFactory =
        //     DefaultConnectionFactory.FromConfiguration(configuration,
        //         "PersonDbConnection", "ProviderName");
    }
}