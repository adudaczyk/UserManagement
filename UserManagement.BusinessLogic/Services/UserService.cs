using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using UserManagement.BusinessLogic.Interfaces;
using UserManagement.BusinessLogic.Models;
using UserManagement.EntityFramework.Models;
using UserManagement.Repository.Interfaces;
using UserManagement.Utils.Helpers;

namespace UserManagement.BusinessLogic.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public UserDto GetUser(int id)
        {
            var user = _unitOfWork.UserRepository.Find(x => x.Id == id).SingleOrDefault();

            if (user == null)
            {
                throw new KeyNotFoundException($"User with ID {id} not found!");
            }

            return _mapper.Map<UserDto>(user);
        }

        public IEnumerable<UserDto> GetUsers()
        {
            var users = _unitOfWork.UserRepository.GetAll();
            return _mapper.Map<IEnumerable<UserDto>>(users);
        }

        public void AddUser(UserDto userDto)
        {
            if (userDto == null)
            {
                throw new KeyNotFoundException(nameof(userDto));
            }

            var user = _mapper.Map<User>(userDto);
            user.PasswordHash = Hasher.Hash(userDto.Password);

            _unitOfWork.UserRepository.Add(user);
            _unitOfWork.SaveChanges();
        }

        public void UpdateUser(UserDto userDto)
        {
            var user = _unitOfWork.UserRepository.Find(x => x.Id == userDto.Id).SingleOrDefault();

            if (user == null)
            {
                throw new KeyNotFoundException($"User with ID {userDto.Id} not found!");
            }

            user.FirstName = userDto.FirstName;
            user.LastName = userDto.LastName;
            user.Username = userDto.Username;

            if (!string.IsNullOrEmpty(userDto.Password))
            {
                user.PasswordHash = Hasher.Hash(userDto.Password);
            }

            _unitOfWork.UserRepository.Update(user);
            _unitOfWork.SaveChanges();
        }

        public void DeleteUser(int id)
        {
            var user = _unitOfWork.UserRepository.Find(x => x.Id == id).SingleOrDefault();

            if (user == null)
            {
                throw new KeyNotFoundException($"User with ID {id} not found!");
            }

            _unitOfWork.UserRepository.Delete(user);
            _unitOfWork.SaveChanges();
        }
    }
}
