using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net;
using UserManagement.API.Controllers;
using UserManagement.BusinessLogic.Interfaces;
using UserManagement.BusinessLogic.Models;

namespace UserManagement.Tests.Controllers
{
    public class UserControllerTests
    {
        private Mock<IUserService> _userService;
        private Mock<IAuthenticateService> _authService;
        private Mock<ILogger<UserController>> _logger;
        private UserController _sut;

        [SetUp]
        public void Setup()
        {
            _userService = new Mock<IUserService>();
            _authService = new Mock<IAuthenticateService>();
            _logger = new Mock<ILogger<UserController>>();

            _sut = new UserController(_userService.Object, _authService.Object, _logger.Object);
        }

        [Test]
        public void GetUser_ShouldRespondOkStatusWithUser()
        {
            // Arrange
            _userService
                .Setup(o => o.GetUser(It.IsAny<int>()))
                .Returns(new UserDto());

            // Act
            var actionResult = _sut.GetUser(It.IsAny<int>());

            // Assert
            var result = actionResult.Result as OkObjectResult;
            Assert.Multiple(() =>
            {
                Assert.That(result.StatusCode, Is.EqualTo((int)HttpStatusCode.OK));
                Assert.That(result.Value, Is.Not.Null);
            });
        }

        [Test]
        public void GetUsers_ShouldRespondOkStatusWithUsers()
        {
            // Arrange
            _userService
                .Setup(o => o.GetUsers())
                .Returns(new List<UserDto>());

            // Act
            var actionResult = _sut.GetUsers();

            // Assert
            var result = actionResult.Result as OkObjectResult;
            Assert.Multiple(() =>
            {
                Assert.That(result.StatusCode, Is.EqualTo((int)HttpStatusCode.OK));
                Assert.That(result.Value, Is.Not.Null);
            });
        }

        [Test]
        public void AddUser_ShouldRespondOkStatus()
        {
            // Arrange
            _userService
                .Setup(o => o.AddUser(It.IsAny<UserDto>()))
                .Verifiable();

            // Act
            var actionResult = _sut.AddUser(It.IsAny<UserDto>());

            // Assert
            var result = actionResult as OkResult;
            Assert.That(result.StatusCode, Is.EqualTo((int)HttpStatusCode.OK));
        }

        [Test]
        public void UpdateUser_ShouldRespondOkStatus()
        {
            // Arrange
            _userService
                .Setup(o => o.UpdateUser(It.IsAny<UserDto>()))
                .Verifiable();

            // Act
            var actionResult = _sut.UpdateUser(It.IsAny<UserDto>());

            // Assert
            var result = actionResult as OkResult;
            Assert.That(result.StatusCode, Is.EqualTo((int)HttpStatusCode.OK));
        }

        [Test]
        public void DeleteUser_ShouldRespondOkStatus()
        {
            // Arrange
            _userService
                .Setup(o => o.DeleteUser(It.IsAny<int>()))
                .Verifiable();

            // Act
            var actionResult = _sut.DeleteUser(It.IsAny<int>());

            // Assert
            var result = actionResult as OkResult;
            Assert.That(result.StatusCode, Is.EqualTo((int)HttpStatusCode.OK));
        }

        [Test] // Czy taki test ma sens?
        public void GetUser_ShouldRespondInternalServerError()
        {
            // Arrange
            _userService
                .Setup(o => o.GetUser(It.IsAny<int>()))
                .Throws(new Exception());

            // Act
            var actionResult = _sut.GetUser(It.IsAny<int>());

            // Assert
            var result = actionResult.Result as StatusCodeResult;
            Assert.That(result.StatusCode, Is.EqualTo((int)HttpStatusCode.InternalServerError));
        }
    }
}
