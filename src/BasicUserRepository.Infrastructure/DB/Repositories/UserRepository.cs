using System.Linq;
using System.Threading.Tasks;
using BasicUserRepository.Infrastructure.DB.Models;
using BasicUserRepository.Infrastructure.DB.Repositories.Interfaces;
using BasicUserRepository.Infrastructure.Enums;
using BasicUserRepository.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace BasicUserRepository.Infrastructure.DB.Repositories;

public class UserRepository : IUserRepository
{
    private readonly DataContext _context;

    public UserRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<UserEntity> GetUserByIdAsync(int id)
    {
        return await _context.Users.FindAsync(id);
    }

    public async Task<UserEntity[]> GetAllUsersAsync(UserDBFilter filter)
    {
        var query = _context.Users.AsQueryable();

        if (!string.IsNullOrEmpty(filter.FirstName)) query = query.Where(u => u.FirstName.Contains(filter.FirstName));

        if (!string.IsNullOrEmpty(filter.LastName)) query = query.Where(u => u.LastName.Contains(filter.LastName));

        if (!string.IsNullOrEmpty(filter.Email)) query = query.Where(u => u.Email.Contains(filter.Email));

        if (filter.DateOfBirth != null) query = query.Where(u => u.DateOfBirth == filter.DateOfBirth);

        return await query.ToArrayAsync();
    }

    public async Task<int> AddUserAsync(UserEntity user)
    {
        var createdUser = await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        return createdUser.Entity.Id;
    }

    public async Task<bool> UpdateUserAsync(UserEntity user)
    {
        _context.Users.Update(user);
        var updatedInstances = await _context.SaveChangesAsync();
        return updatedInstances > 0;
    }

    public async Task<DeleteUserDBResult> DeleteUserAsync(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user != null)
        {
            _context.Users.Remove(user);
            var instances = await _context.SaveChangesAsync();
            return instances > 0 ? DeleteUserDBResult.Deleted : DeleteUserDBResult.Error;
        }

        return DeleteUserDBResult.NotFound;
    }
}