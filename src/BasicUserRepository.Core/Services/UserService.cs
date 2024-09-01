using BasicUserRepository.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BasicUserRepository.Infrastructure.DB.Models;
using BasicUserRepository.Infrastructure.DB.Repositories.Interfaces;
using System.Linq;
using System.Threading;

namespace BasicUserRepository.Core.Services
{
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
            return await _userRepository.AddUserAsync(new UserEntity()
            {
                CreatedAt = timeNow,
                DateOfBirth = user.DateOfBirth.Date,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UpdatedAt = timeNow
            });
        }

        public async Task DeleteUserAsync(int id, CancellationToken token)
        {
            await _userRepository.DeleteUserAsync(id);
        }

        public async Task<UserInfo[]> GetAllUsersAsync(CancellationToken token)
        {
            var users = await _userRepository.GetAllUsersAsync();
            return users.Select(Convert).ToArray();
        }

        public async Task<UserInfo> GetUserByIdAsync(int id, CancellationToken token)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            return Convert(user);
        }

        public async Task UpdateUserAsync(UserInfo user, CancellationToken token)
        {
            await _userRepository.UpdateUserAsync(new UserEntity()
            {
                DateOfBirth = user.DateOfBirth.Date,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UpdatedAt = DateTime.UtcNow
            });
        }

        private UserInfo Convert(UserEntity user)
        {
            return new UserInfo()
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
}
