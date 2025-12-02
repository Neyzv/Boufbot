using System.Linq.Expressions;

namespace Boufbot.Infrastructure.Specifications;

public interface ICriteriaSpecification<TDbEntity>
    : ISpecification<TDbEntity>
    where TDbEntity : class
{
    /// <summary>
    /// Expression to specify the criteria to retrieve an instance of the model.
    /// </summary>
    Expression<Func<TDbEntity, bool>> Criteria { get; }
}