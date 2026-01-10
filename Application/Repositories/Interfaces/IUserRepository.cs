using Application.DTO.Responses;

namespace Application.Repositories.Interfaces;

public interface IUserRepository
{
    public Task<List<UserResponse>> GetAllAsync();
}