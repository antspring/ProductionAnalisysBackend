using Application.Repositories.Interfaces;

namespace Application.UnitOfWork;

public interface IUnitOfWork
{
    public ICatalogRepository Catalogs { get; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}