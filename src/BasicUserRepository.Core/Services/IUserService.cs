using BasicUserRepository.Core.Models;
using System.Threading;
using System.Threading.Tasks;

namespace BasicUserRepository.Core.Services
{
    public interface IUserService
    {
        Task<UserInfo> GetUserByIdAsync(int id, CancellationToken token);
        Task<UserInfo[]> GetAllUsersAsync(CancellationToken token);
        Task<int> AddUserAsync(UserInfo user, CancellationToken token);
        Task UpdateUserAsync(UserInfo user, CancellationToken token);
        Task DeleteUserAsync(int id, CancellationToken token);
    }
}
