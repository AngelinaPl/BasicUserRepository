using BasicUserRepository.Infrastructure.DB.Models;
using Microsoft.EntityFrameworkCore;

namespace BasicUserRepository.Infrastructure.DB;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    public DbSet<UserEntity> Users { get; set; }
}