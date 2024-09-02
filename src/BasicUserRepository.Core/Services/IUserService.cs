using System.Threading;
using System.Threading.Tasks;
using BasicUserRepository.Core.Enums;
using BasicUserRepository.Core.Models;
using BasicUserRepository.Infrastructure.Models;

namespace BasicUserRepository.Core.Services
{
    public interface IUserService
    {
        Task<UserInfo> GetUserByIdAsync(int id, CancellationToken token);
        Task<UserInfo[]> GetAllUsersAsync(UserFilter filter, CancellationToken token);
        Task<int> AddUserAsync(UserInfo user, CancellationToken token);
        Task<UpdateUserResult> UpdateUserAsync(UpdateUserInfo user, CancellationToken token);
        Task<DeleteUserResult> DeleteUserAsync(int id, CancellationToken token);
    }
}