using System.Linq.Expressions;
using Application.DTO.Responses;
using Application.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Implementations;

public class UserRepository : IUserRepository
{
    private ApplicationDbContext _dbContext;

    public UserRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ApplicationUser?> FindBy(Expression<Func<ApplicationUser, bool>> predicate,
        CancellationToken cancellationToken)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(predicate, cancellationToken);
    }

    public async Task<bool> ExistAsync(Expression<Func<ApplicationUser, bool>> predicate)
    {
        return await _dbContext.Users.AnyAsync(predicate);
    }

    public async Task<List<UserResponse>> GetAllAsync()
    {
        return await _dbContext.Users.Select(user => new UserResponse
        {
            UserName = user.UserName,
            Email = user.Email,
            Status = user.Status.Name,
            Roles = _dbContext.UserRoles
                .Where(ur => ur.UserId == user.Id)
                .Join(
                    _dbContext.Roles,
                    ur => ur.RoleId,
                    r => r.Id,
                    (ur, r) => r.Name)
                .ToList()
        }).ToListAsync();
    }
}