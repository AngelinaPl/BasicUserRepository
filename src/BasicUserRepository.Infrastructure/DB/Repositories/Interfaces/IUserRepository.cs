using System.Threading.Tasks;
using BasicUserRepository.Infrastructure.DB.Models;
using BasicUserRepository.Infrastructure.Enums;
using BasicUserRepository.Infrastructure.Models;

namespace BasicUserRepository.Infrastructure.DB.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<UserEntity> GetUserByIdAsync(int id);
        Task<UserEntity[]> GetAllUsersAsync(UserDBFilter filter);
        Task<int> AddUserAsync(UserEntity user);
        Task<UpdateUserDBResult> UpdateUserAsync(UpdateUserInfo user);
        Task<DeleteUserDBResult> DeleteUserAsync(int id);
    }
}