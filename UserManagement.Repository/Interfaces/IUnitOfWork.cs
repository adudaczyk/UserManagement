using UserManagement.EntityFramework.Models;

namespace UserManagement.Repository.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<User> UserRepository { get; }
        void SaveChanges();
    }
}
