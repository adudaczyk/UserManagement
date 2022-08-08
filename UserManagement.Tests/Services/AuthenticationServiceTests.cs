using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using UserManagement.BusinessLogic.Interfaces;
using UserManagement.BusinessLogic.Models.Requests;
using UserManagement.BusinessLogic.Services;
using UserManagement.EntityFramework.Models;
using UserManagement.Repository.Interfaces;
using UserManagement.Repository.Repositories;
using UserManagement.Utils.Settings;

namespace UserManagement.Tests.Services
{
    public class AuthenticationServiceTests
    {
        private Mock<IUnitOfWork> _unitOfWork;
        private Mock<UserRepository> _userRepository;
        private IAuthenticateService _sut;
        private IOptions<AppSettings> _appSettings;

        [SetUp]
        public void Setup()
        {
            _appSettings = Options.Create(new AppSettings() { JwtSecret = "9f4e1660-80b5-4011-81f8-51f53d2b2171" });
            _unitOfWork = new Mock<IUnitOfWork>();
            _userRepository = new Mock<UserRepository>(It.IsAny<DbContext>());
            _unitOfWork.Setup(o => o.UserRepository).Returns(_userRepository.Object);

            _sut = new AuthenticateService(_unitOfWork.Object, _appSettings);
        }

        [Test]
        public void ShouldAuthenticateWithSuccess()
        {
            // Arrange
            _userRepository
                .Setup(o => o.Find(It.IsAny<Expression<Func<User, bool>>>()))
                .Returns(new List<User>() {
                    new User()
                    {
                        Username = "Joe",
                        PasswordHash = "$QAZ$V8$10000$EnLpi9pmFJ/CRMUt8ohunYZqBPGuigp1J7YWgExJ+BHvFZKQ"
                    }
                });

            // Act
            var authenticateResponse = _sut.Authenticate(
                new AuthenticateRequest() { Username = "Joe", Password = "testtest" });

            // Assert
            Assert.That(authenticateResponse, Is.Not.Null);
        }

        [Test]
        public void ShouldNotAuthenticate()
        {
            // Arrange
            _userRepository
                .Setup(o => o.Find(It.IsAny<Expression<Func<User, bool>>>()))
                .Returns(new List<User>() {
                    new User()
                    {
                        Username = "Joe",
                        PasswordHash = "$QAZ$V8$10000$EnLpi9pmFJ/CRMUt8ohunYZqBPGuigp1J7YWgExJ+BHvFZKQ"
                    }
                });

            // Act
            var authenticateResponse = _sut.Authenticate(
                new AuthenticateRequest() { Username = "Joe", Password = "loremipsum" });

            // Assert
            Assert.That(authenticateResponse, Is.Null);
        }
    }
}