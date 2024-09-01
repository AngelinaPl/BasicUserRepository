using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BasicUserRepository.Infrastructure.DB.Models;
using BasicUserRepository.Infrastructure.DB.Repositories.Interfaces;

namespace BasicUserRepository.Infrastructure.DB.Repositories
{
    public class UserRepositoryFake : IUserRepository
    {
        private readonly List<UserEntity> _users;

        public UserRepositoryFake()
        {
            _users = new List<UserEntity>
            {
                new UserEntity
                {
                    Id = 1, FirstName = "John", LastName = "Doe", Email = "john.doe@example.com",
                    DateOfBirth = new DateTime(1990, 1, 1), CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now
                },
                new UserEntity
                {
                    Id = 2, FirstName = "Jane", LastName = "Smith", Email = "jane.smith@example.com",
                    DateOfBirth = new DateTime(1992, 2, 2), CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now
                }
            };
        }

        public async Task<UserEntity> GetUserByIdAsync(int id)
        {
            return await Task.FromResult(_users.FirstOrDefault(u => u.Id == id));
        }

        public async Task<IEnumerable<UserEntity>> GetAllUsersAsync()
        {
            return await Task.FromResult(_users);
        }

        public async Task<int> AddUserAsync(UserEntity user)
        {
            var id = _users.Count + 1;
            user.Id = id;
            _users.Add(user);
            return id;
        }

        public async Task UpdateUserAsync(UserEntity user)
        {
            var existingUser = _users.FirstOrDefault(u => u.Id == user.Id);
            if (existingUser != null)
            {
                existingUser.FirstName = user.FirstName;
                existingUser.LastName = user.LastName;
                existingUser.Email = user.Email;
                existingUser.DateOfBirth = user.DateOfBirth;
                existingUser.CreatedAt = user.CreatedAt;
                existingUser.UpdatedAt = user.UpdatedAt;
            }

            await Task.CompletedTask;
        }

        public async Task DeleteUserAsync(int id)
        {
            var user = _users.FirstOrDefault(u => u.Id == id);
            if (user != null) _users.Remove(user);
            await Task.CompletedTask;
        }
    }
}