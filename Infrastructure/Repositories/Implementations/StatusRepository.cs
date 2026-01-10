using System.Linq.Expressions;
using Application.Repositories.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Implementations;

public class StatusRepository : IStatusRepository
{
    private ApplicationDbContext _dbContext;

    public StatusRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Status?> FindBy(Expression<Func<Status, bool>> predicate,
        CancellationToken cancellationToken)
    {
        return await _dbContext.Statuses.FirstOrDefaultAsync(predicate, cancellationToken);
    }

    public async Task<bool> ExistAsync(Expression<Func<Status, bool>> predicate)
    {
        return await _dbContext.Statuses.AnyAsync(predicate);
    }
}