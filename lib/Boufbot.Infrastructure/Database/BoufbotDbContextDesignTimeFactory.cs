using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Boufbot.Infrastructure.Database;

/// <summary>
/// Class that provides an instance of the <see cref="BoufbotDbContext"/> to execute EF Core commands.
/// </summary>
public sealed class BoufbotDbContextDesignTimeFactory
    : IDesignTimeDbContextFactory<BoufbotDbContext>
{
    public BoufbotDbContext CreateDbContext(string[] args)
    {
        return new BoufbotDbContext(new DbContextOptions<BoufbotDbContext>());
    }
}