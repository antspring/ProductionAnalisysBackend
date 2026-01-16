using Application.Repositories.Interfaces;
using Application.UnitOfWork;

namespace Infrastructure.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _dbContext;

    public UnitOfWork(
        ApplicationDbContext dbContext,
        ICatalogRepository catalogRepository,
        ICatalogValueRepository catalogValueRepository,
        IHourlyByTactTimeRepository hourlyByTactTimeRepository,
        IHourlyByPowerRepository hourlyByPowerRepository,
        IHourlySeveralRepository hourlySeveralRepository
    )
    {
        _dbContext = dbContext;
        Catalogs = catalogRepository;
        CatalogValues = catalogValueRepository;
        HourlyByTactTime = hourlyByTactTimeRepository;
        HourlyByPower = hourlyByPowerRepository;
        HourlySeveral = hourlySeveralRepository;
    }

    public ICatalogRepository Catalogs { get; }
    public ICatalogValueRepository CatalogValues { get; }
    public IHourlyByTactTimeRepository HourlyByTactTime { get; }
    public IHourlyByPowerRepository HourlyByPower { get; }
    public IHourlySeveralRepository HourlySeveral { get; }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return _dbContext.SaveChangesAsync(cancellationToken);
    }
}