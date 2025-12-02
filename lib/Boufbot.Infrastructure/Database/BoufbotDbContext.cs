using Microsoft.EntityFrameworkCore;

namespace Boufbot.Infrastructure.Database;

public sealed class BoufbotDbContext(DbContextOptions<BoufbotDbContext> options)
    : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BoufbotDbContext).Assembly);
    }
}