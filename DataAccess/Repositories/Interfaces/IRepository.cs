using System.Linq.Expressions;

namespace DataAccess.Repositories.Interfaces;

public interface IRepository<T> where T : class
{
    public Task<T?> FindBy(Expression<Func<T, bool>> predicate,
        CancellationToken cancellationToken);

    public Task<bool> ExistAsync(Expression<Func<T, bool>> predicate);
}