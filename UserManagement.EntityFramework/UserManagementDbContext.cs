using Microsoft.EntityFrameworkCore;
using UserManagement.EntityFramework.Models;

namespace UserManagement.EntityFramework
{
    public class UserManagementDbContext : DbContext
    {
        public UserManagementDbContext(DbContextOptions<UserManagementDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
    }
}
