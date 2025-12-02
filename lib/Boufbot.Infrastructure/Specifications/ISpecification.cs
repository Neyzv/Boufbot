namespace Boufbot.Infrastructure.Specifications;

/// <summary>
/// Base class for all kinds of specifications.
/// </summary>
/// <typeparam name="TDbEntity">The type of the entity.</typeparam>
public interface ISpecification<TDbEntity>
    where TDbEntity : class
{
}