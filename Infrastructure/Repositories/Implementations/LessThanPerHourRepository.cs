using Application.DTO.Requests.LessThanPerHour;
using Application.Repositories.Interfaces;
using Domain.Models.ProductionAnalysis.LessThanPerHour;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Implementations;

public class LessThanPerHourRepository : ILessThanPerHourRepository
{
    private ApplicationDbContext _dbContext;

    public LessThanPerHourRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<List<LessThanPerHourView>> GetAllThroughViewAsync()
    {
        return _dbContext.LessThanPerHourViews
            .Include(v => v.Department)
            .Include(v => v.Performer)
            .Include(v => v.Shift)
            .Include(v => v.WorkHour)
            .Include(v => v.OperationName)
            .Include(v => v.ProductionDocument)
            .ToListAsync();
    }

    public Task<LessThanPerHourView?> GetByIdThroughViewAsync(int id)
    {
        return _dbContext.LessThanPerHourViews
            .Include(v => v.Department)
            .Include(v => v.Performer)
            .Include(v => v.Shift)
            .Include(v => v.WorkHour)
            .Include(v => v.OperationName)
            .Include(v => v.ProductionDocument)
            .FirstOrDefaultAsync(v => v.Id == id);
    }

    public Task<LessThanPerHour?> GetByIdAsync(int id)
    {
        return _dbContext.LessThanPerHour.FirstOrDefaultAsync(v => v.Id == id);
    }

    public async Task<LessThanPerHour> CreateAsync(LessThanPerHour lessThanPerHour)
    {
        await _dbContext.LessThanPerHour.AddAsync(lessThanPerHour);

        return lessThanPerHour;
    }

    public void Update(LessThanPerHour lessThanPerHour,
        LessThanPerHourUpdateRequest request)
    {
        var entry = _dbContext.Entry(lessThanPerHour);

        foreach (var prop in typeof(LessThanPerHourUpdateRequest).GetProperties())
        {
            var value = prop.GetValue(request);
            if (value is null)
                continue;
            entry.Property(prop.Name).CurrentValue = value;
            entry.Property(prop.Name).IsModified = true;
        }
    }

    public Task RemoveAsync(int id)
    {
        return _dbContext.LessThanPerHour.Where(hb => hb.Id == id).ExecuteDeleteAsync();
    }
}