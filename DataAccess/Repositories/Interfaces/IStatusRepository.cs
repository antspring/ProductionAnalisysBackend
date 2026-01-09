using DataAccess.Models;

namespace DataAccess.Repositories.Interfaces;

public interface IStatusRepository : IRepository<Status>
{
    public Task<List<Status>> GetAllAsync();
}