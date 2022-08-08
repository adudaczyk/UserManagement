using UserManagement.EntityFramework;
using UserManagement.EntityFramework.Models;
using UserManagement.Repository.Interfaces;
using UserManagement.Repository.Repositories;

namespace UserManagement.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private IRepository<User> userRepository;
        private readonly UserManagementDbContext context;

        public IRepository<User> UserRepository
        {
            get
            {
                if (userRepository == null)
                {
                    userRepository = new UserRepository(context);
                }

                return userRepository;
            }
        }

        public UnitOfWork(UserManagementDbContext context)
        {
            this.context = context;
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }
    }
}
