using Boufbot.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Boufbot.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Add services of this project to the <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The app service collection instance.</param>
    /// <param name="configure">An action to configure the database usage.</param>
    /// <returns></returns>
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, Action<IServiceProvider, DbContextOptionsBuilder>? configure = null) =>
        services.AddDbContextFactory<BoufbotDbContext>((sp, o) => configure?.Invoke(sp, o));
}