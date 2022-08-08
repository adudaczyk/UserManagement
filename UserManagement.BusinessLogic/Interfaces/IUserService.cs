using System.Collections.Generic;
using UserManagement.BusinessLogic.Models;

namespace UserManagement.BusinessLogic.Interfaces
{
    public interface IUserService
    {
        UserDto GetUser(int id);
        IEnumerable<UserDto> GetUsers();
        void AddUser(UserDto user);
        void UpdateUser(UserDto user);
        void DeleteUser(int id);
    }
}
