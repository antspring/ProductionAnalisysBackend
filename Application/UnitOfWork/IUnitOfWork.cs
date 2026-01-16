using Application.Repositories.Interfaces;

namespace Application.UnitOfWork;

public interface IUnitOfWork
{
    public ICatalogRepository Catalogs { get; }
    public ICatalogValueRepository CatalogValues { get; }
    public IHourlyByTactTimeRepository HourlyByTactTime { get; }
    public IHourlyByPowerRepository HourlyByPower { get; }
    public IHourlySeveralRepository HourlySeveral { get; }
    public ILessThanPerHourRepository LessThanPerHour { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}