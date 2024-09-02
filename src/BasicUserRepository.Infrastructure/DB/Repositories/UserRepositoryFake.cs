using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BasicUserRepository.Infrastructure.DB.Models;
using BasicUserRepository.Infrastructure.DB.Repositories.Interfaces;
using BasicUserRepository.Infrastructure.Enums;
using BasicUserRepository.Infrastructure.Models;

namespace BasicUserRepository.Infrastructure.DB.Repositories
{
    public class UserRepositoryFake : IUserRepository
    {
        private readonly List<UserEntity> _users;

        public UserRepositoryFake()
        {
            _users = new List<UserEntity>
            {
                new UserEntity()
                {
                    Id = 1, FirstName = "John", LastName = "Doe", Email = "john.doe@example.com",
                    DateOfBirth = new DateTime(1990, 1, 1), CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now
                },
                new UserEntity()
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

        public async Task<UserEntity[]> GetAllUsersAsync(UserDBFilter filter)
        {
            var query = _users.AsQueryable();

            if (!string.IsNullOrEmpty(filter.FirstName)) query = query.Where(u => u.FirstName.Contains(filter.FirstName));

            if (!string.IsNullOrEmpty(filter.LastName)) query = query.Where(u => u.LastName.Contains(filter.LastName));

            if (!string.IsNullOrEmpty(filter.Email)) query = query.Where(u => u.Email.Contains(filter.Email));

            if (filter.DateOfBirth != null) query = query.Where(u => u.DateOfBirth == filter.DateOfBirth);

            return query.ToArray();
        }

        public async Task<int> AddUserAsync(UserEntity user)
        {
            var id = _users.Count + 1;
            user.Id = id;
            _users.Add(user);
            return id;
        }

        public async Task<UpdateUserDBResult> UpdateUserAsync(UpdateUserInfo user)
        {
            var existingUser = _users.FirstOrDefault(u => u.Id == user.Id);
            if (existingUser != null)
            {
                _users.Remove(existingUser);
                var newUser = new UserEntity
                {
                    DateOfBirth = user.DateOfBirth?.Date ?? existingUser.DateOfBirth,
                    Email = user.Email ?? existingUser.Email,
                    FirstName = user.FirstName ?? existingUser.FirstName,
                    LastName = user.LastName ?? existingUser.LastName,
                    UpdatedAt = DateTime.UtcNow,
                    Id = existingUser.Id,
                    CreatedAt = existingUser.CreatedAt
                };
                _users.Add(newUser);
                return UpdateUserDBResult.Updated;
            }
            
            return UpdateUserDBResult.NotFound;
        }

        public async Task<DeleteUserDBResult> DeleteUserAsync(int id)
        {
            var user = _users.FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                _users.Remove(user);
                return DeleteUserDBResult.Deleted;
            }

            return DeleteUserDBResult.NotFound;
        }
    }
}