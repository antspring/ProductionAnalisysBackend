using DataAccess.DTO.Responses;
using DataAccess.Models;

namespace DataAccess.Repositories.Interfaces;

public interface IUserRepository : IRepository<ApplicationUser>
{
    public Task<List<UserResponse>> GetAllAsync();
}