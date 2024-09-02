using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BasicUserRepository.Core.Enums;
using BasicUserRepository.Core.Models;
using BasicUserRepository.Infrastructure.DB.Models;
using BasicUserRepository.Infrastructure.DB.Repositories.Interfaces;
using BasicUserRepository.Infrastructure.Enums;
using BasicUserRepository.Infrastructure.Models;

namespace BasicUserRepository.Core.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<int> AddUserAsync(UserInfo user, CancellationToken token)
    {
        var timeNow = DateTime.UtcNow;
        return await _userRepository.AddUserAsync(new UserEntity
        {
            CreatedAt = timeNow,
            DateOfBirth = user.DateOfBirth.Date,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            UpdatedAt = timeNow
        });
    }

    public async Task<DeleteUserResult> DeleteUserAsync(int id, CancellationToken token)
    {
        var result = await _userRepository.DeleteUserAsync(id);
        return result switch
        {
            DeleteUserDBResult.Deleted => DeleteUserResult.Deleted,
            DeleteUserDBResult.Error => DeleteUserResult.Error,
            DeleteUserDBResult.NotFound => DeleteUserResult.NotFound,
            _ => DeleteUserResult.Error
        };
    }

    public async Task<UserInfo[]> GetAllUsersAsync(UserFilter filter, CancellationToken token)
    {
        var users = await _userRepository.GetAllUsersAsync(new UserDBFilter
        {
            DateOfBirth = filter.DateOfBirth,
            Email = filter.Email,
            FirstName = filter.FirstName,
            LastName = filter.LastName
        });
        return users.Select(Convert).ToArray();
    }

    public async Task<UserInfo> GetUserByIdAsync(int id, CancellationToken token)
    {
        var user = await _userRepository.GetUserByIdAsync(id);
        return Convert(user);
    }

    public async Task<UpdateUserResult> UpdateUserAsync(UpdateUserInfo user, CancellationToken token)
    {
        var oldUser = await _userRepository.GetUserByIdAsync(user.Id);
        if (oldUser is not null)
        {
            var isSuccess = await _userRepository.UpdateUserAsync(new UserEntity
            {
                DateOfBirth = user.DateOfBirth?.Date ?? oldUser.DateOfBirth,
                Email = user.Email ?? oldUser.Email,
                FirstName = user.FirstName ?? oldUser.FirstName,
                LastName = user.LastName ?? oldUser.LastName,
                UpdatedAt = DateTime.UtcNow,
                Id = oldUser.Id,
                CreatedAt = oldUser.CreatedAt
            });
            return isSuccess ? UpdateUserResult.Updated : UpdateUserResult.Error;
        }

        return UpdateUserResult.NotFound;
    }

    private UserInfo Convert(UserEntity user)
    {
        return new UserInfo
        {
            CreatedAt = DateTime.UtcNow,
            DateOfBirth = user.DateOfBirth,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            UpdatedAt = DateTime.UtcNow,
            Id = user.Id
        };
    }
}