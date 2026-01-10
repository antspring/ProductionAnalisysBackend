using System.Linq.Expressions;
using Domain.Models;

namespace Application.Repositories.Interfaces;

public interface IStatusRepository
{
    public Task<bool> ExistAsync(Expression<Func<Status, bool>> predicate);
}