using Application.Repositories.Interfaces;
using Application.UnitOfWork;

namespace Infrastructure.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _dbContext;

    public UnitOfWork(
        ApplicationDbContext dbContext,
        ICatalogRepository catalogRepository,
        ICatalogValueRepository catalogValueRepository
    )
    {
        _dbContext = dbContext;
        Catalogs = catalogRepository;
        CatalogValues = catalogValueRepository;
    }

    public ICatalogRepository Catalogs { get; }
    public ICatalogValueRepository CatalogValues { get; }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return _dbContext.SaveChangesAsync(cancellationToken);
    }
}