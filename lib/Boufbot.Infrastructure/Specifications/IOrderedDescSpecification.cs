using System.Linq.Expressions;

namespace Boufbot.Infrastructure.Specifications;

public interface IOrderedDescSpecification<TDbEntity>
    : ISpecification<TDbEntity>
    where TDbEntity : class
{
    /// <summary>
    /// Expression to specify the property to order descending on.
    /// </summary>
    Expression<Func<TDbEntity, object>> OrderByDescending { get; }
}