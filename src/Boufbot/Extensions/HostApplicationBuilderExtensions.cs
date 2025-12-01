using Boufbot.Core.Extensions;
using Boufbot.Models;
using Boufbot.Services.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using NetCord.Gateway;
using NetCord.Hosting.Gateway;

namespace Boufbot.Extensions;

public static class HostApplicationBuilderExtensions
{
    private const string AppSettingsFileName = "appsettings.json";
    private const string DebugAppSettingsFileName = "appsettings.debug.json";

    private static HostApplicationBuilder AddServices(this HostApplicationBuilder builder)
    {
        builder.Services
            .AddCore()
            .AddDiscordShardedGateway((options, sp) =>
            {
                options.Token = sp.GetRequiredService<IOptions<DiscordConfiguration>>().Value.Token;
                options.Intents = GatewayIntents.All;
            })
            .AddShardedGatewayHandlers(typeof(HostApplicationBuilderExtensions).Assembly)
            .AddSingleton<IHttpService, HttpService>();

        return builder;
    }

    private static HostApplicationBuilder AddConfigurations(this HostApplicationBuilder builder)
    {
        builder.Configuration
            .AddJsonFile(AppSettingsFileName);

#if DEBUG
        builder.Configuration
            .AddJsonFile(DebugAppSettingsFileName);
#endif

        builder.Services
            .Configure<DiscordConfiguration>(builder.Configuration.GetSection(nameof(DiscordConfiguration)));

        return builder;
    }

    public static HostApplicationBuilder UseServicesAndConfiguration(this HostApplicationBuilder builder) =>
        builder
            .AddServices()
            .AddConfigurations();
}