using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using UserManagement.BusinessLogic.Interfaces;
using UserManagement.BusinessLogic.Mappers;
using UserManagement.BusinessLogic.Models;
using UserManagement.BusinessLogic.Services;
using UserManagement.EntityFramework.Models;
using UserManagement.Repository.Interfaces;
using UserManagement.Repository.Repositories;

namespace UserManagement.Tests.Services
{
    public class UserServiceTests
    {
        private IMapper _mapper;
        private Mock<IUnitOfWork> _unitOfWork;
        private Mock<UserRepository> _userRepository;
        private IUserService _sut;

        [SetUp]
        public void Setup()
        {
            InitializeMapper();

            _unitOfWork = new Mock<IUnitOfWork>();
            _userRepository = new Mock<UserRepository>(It.IsAny<DbContext>());

            _unitOfWork.Setup(o => o.UserRepository).Returns(_userRepository.Object);

            _sut = new UserService(_unitOfWork.Object, _mapper);
        }

        [Test]
        public void ShouldGetUserById()
        {
            // Arrange
            _userRepository
                .Setup(o => o.Find(It.IsAny<Expression<Func<User, bool>>>()))
                .Returns(new List<User>() { new User() });

            // Act
            var user = _sut.GetUser(It.IsAny<int>());

            // Assert
            Assert.That(user, Is.Not.Null);
        }

        [Test]
        public void ShouldGetUsers()
        {
            // Arrange
            _userRepository
                .Setup(o => o.GetAll())
                .Returns(new List<User>() { new User() });

            // Act
            var users = _sut.GetUsers();

            // Assert
            Assert.That(users.Count(), Is.GreaterThan(0));
        }

        [Test]
        public void ShouldCallAddMethodOnce()
        {
            // Arrange
            _userRepository
                .Setup(o => o.Add(It.IsAny<User>()))
                .Verifiable();

            // Act
            _sut.AddUser(new UserDto() { Password = "password" });

            // Assert
            _userRepository.Verify(o => o.Add(It.IsAny<User>()), Times.Once);
        }

        [Test]
        public void ShouldCallUpdateMethodOnce()
        {
            _userRepository
                .Setup(o => o.Find(It.IsAny<Expression<Func<User, bool>>>()))
                .Returns(new List<User>() { new User() });

            _userRepository
                .Setup(o => o.Update(new User()))
                .Verifiable();

            // Act
            _sut.UpdateUser(new UserDto() { Id = 2 });

            // Assert
            _userRepository.Verify(o => o.Update(It.IsAny<User>()), Times.Once);
        }

        [Test]
        public void ShouldCallDeleteMethodOnce()
        {
            _userRepository
                .Setup(o => o.Find(It.IsAny<Expression<Func<User, bool>>>()))
                .Returns(new List<User>() { new User() });

            _userRepository
                .Setup(o => o.Delete(It.IsAny<User>()))
                .Verifiable();

            // Act
            _sut.DeleteUser(It.IsAny<int>());

            // Assert
            _userRepository.Verify(o => o.Delete(It.IsAny<User>()), Times.Once);
        }

        private void InitializeMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });

            _mapper = config.CreateMapper();
        }
    }
}