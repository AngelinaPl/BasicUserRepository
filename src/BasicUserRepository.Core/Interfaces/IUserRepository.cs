using BasicUserRepository.Infrastructure.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BasicUserRepository.Core.Interfaces
{
    public interface IUserRepository
    {
        Task<UserEntity> GetUserByIdAsync(int id);
        Task<IEnumerable<UserEntity>> GetAllUsersAsync();
        Task AddUserAsync(UserEntity user);
        Task UpdateUserAsync(UserEntity user);
        Task DeleteUserAsync(int id);
    }
}
