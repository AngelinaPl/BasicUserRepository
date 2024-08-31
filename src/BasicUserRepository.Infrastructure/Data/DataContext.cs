using BasicUserRepository.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace BasicUserRepository.Infrastructure.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) 
        {
            Database.EnsureCreated();
        }

        public DbSet<UserEntity> Users { get; set; }
    }
}
