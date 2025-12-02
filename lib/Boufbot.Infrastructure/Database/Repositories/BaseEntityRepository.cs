using Boufbot.Infrastructure.Extensions;
using Boufbot.Infrastructure.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Boufbot.Infrastructure.Database.Repositories;

internal abstract class BaseEntityRepository<TDbEntity>(IDbContextFactory<BoufbotDbContext> dbContextFactory)
    where TDbEntity : class
{
    /// <summary>
    /// Retrieve all instances of an entity without their dependency properties.
    /// </summary>
    /// <param name="asNoTracking">If the entities should be tracked or not.</param>
    /// <returns></returns>
    public async Task<List<TDbEntity>> GetAllAsync(bool asNoTracking = true)
    {
        await using var context = await dbContextFactory.CreateDbContextAsync().ConfigureAwait(false);

        var dbSet = context
            .Set<TDbEntity>();

        if (asNoTracking)
            dbSet.AsNoTracking();

        return await dbSet
            .ToListAsync()
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieve the first instance of an entity which satisfies the specified specifications.
    /// </summary>
    /// <param name="asNoTracking">If the entities should be tracked or not.</param>
    /// <param name="specifications">The specifications that need to be satisfied.</param>
    /// <returns></returns>
    protected async Task<TDbEntity?> GetAsync(bool asNoTracking, params IEnumerable<ISpecification<TDbEntity>> specifications)
    {
        await using var context = await dbContextFactory.CreateDbContextAsync().ConfigureAwait(false);

        var dbSet = context
            .Set<TDbEntity>();

        if (asNoTracking)
            dbSet.AsNoTracking();

        return await dbSet
            .GetQuery(specifications)
            .FirstOrDefaultAsync()
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieve all instances of an entity which satisfies the specified specifications.
    /// </summary>
    /// <param name="asNoTracking">If the entities should be tracked or not.</param>
    /// <param name="specifications">The specifications that need to be satisfied.</param>
    /// <returns></returns>
    protected async Task<List<TDbEntity>> GetAllAsync(bool asNoTracking, params IEnumerable<ISpecification<TDbEntity>> specifications)
    {
        await using var context = await dbContextFactory.CreateDbContextAsync().ConfigureAwait(false);

        var dbSet = context
            .Set<TDbEntity>();

        if (asNoTracking)
            dbSet.AsNoTracking();

        return await dbSet
            .GetQuery(specifications)
            .ToListAsync()
            .ConfigureAwait(false);
    }
}