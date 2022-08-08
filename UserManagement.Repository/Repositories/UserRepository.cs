using UserManagement.EntityFramework;
using UserManagement.EntityFramework.Models;

namespace UserManagement.Repository.Repositories
{
    public class UserRepository : GenericRepository<User, UserManagementDbContext>
    {
        public UserRepository(UserManagementDbContext context) : base(context) { }
    }
}
