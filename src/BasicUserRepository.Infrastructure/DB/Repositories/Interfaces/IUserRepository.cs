using System.Collections.Generic;
using System.Threading.Tasks;
using BasicUserRepository.Infrastructure.DB.Models;

namespace BasicUserRepository.Infrastructure.DB.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<UserEntity> GetUserByIdAsync(int id);
        Task<IEnumerable<UserEntity>> GetAllUsersAsync();
        Task<int> AddUserAsync(UserEntity user);
        Task UpdateUserAsync(UserEntity user);
        Task DeleteUserAsync(int id);
    }
}