using Application.Repositories.Interfaces;
using Application.UnitOfWork;

namespace Infrastructure.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _dbContext;

    public UnitOfWork(
        ApplicationDbContext dbContext,
        ICatalogRepository catalogRepository
    )
    {
        _dbContext = dbContext;
        Catalogs = catalogRepository;
    }

    public ICatalogRepository Catalogs { get; }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return _dbContext.SaveChangesAsync(cancellationToken);
    }
}