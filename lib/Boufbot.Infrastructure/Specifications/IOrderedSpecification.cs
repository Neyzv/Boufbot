using System.Linq.Expressions;

namespace Boufbot.Infrastructure.Specifications;

public interface IOrderedSpecification<TDbEntity>
    : ISpecification<TDbEntity>
    where TDbEntity : class
{
    /// <summary>
    /// Expression to specify the property to order on.
    /// </summary>
    Expression<Func<TDbEntity, object>> OrderBy { get; }
}