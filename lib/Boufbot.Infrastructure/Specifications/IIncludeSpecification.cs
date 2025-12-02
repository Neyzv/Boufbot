using System.Linq.Expressions;

namespace Boufbot.Infrastructure.Specifications;

public interface IIncludeSpecification<TDbEntity>
    : ISpecification<TDbEntity>
    where TDbEntity : class
{
    /// <summary>
    /// Expression to specify the dependency property that needs to be loaded when retrieving result.
    /// </summary>
    Expression<Func<TDbEntity, object>> Include { get; }
}