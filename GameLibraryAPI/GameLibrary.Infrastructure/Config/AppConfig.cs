using GameLibrary.Infrastructure.Config.Models;
using Microsoft.Extensions.Configuration;

namespace GameLibrary.Infrastructure.Config;

public class AppConfig
{
    public static ConnectionStringsSettings? ConnectionStrings { get; set; }

    public static void Init(IConfiguration configuration)
    {
        Configure(configuration);
    }

    private static void Configure(IConfiguration configuration)
    {
        ConnectionStrings = configuration.GetSection("ConnectionStrings").Get<ConnectionStringsSettings>();
    }
}
