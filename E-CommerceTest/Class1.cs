//using e_comm.Auth;
//using e_comm.Controllers;
//using e_comm.Models;
//using e_comm.Services;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Routing;
//using UserWebAPI.Exceptions.DemoAPI.Exception;
//using UserWebAPI.Exceptions;
//using NUnit.Framework;
//using Moq;
//using Assert = NUnit.Framework.Assert;

//namespace E_CommerceTest
//{
//    [TestFixture]
//    public class UserControllerTests
//    {
//        private Mock<IUserService> _mockUserService;
//        private Mock<IAuth> _mockAuth;
//        private UserController _userController;

//        [SetUp]
//        public void Setup()
//        {
//            _mockUserService = new Mock<IUserService>();
//            _mockAuth = new Mock<IAuth>();
//            _userController = new UserController(_mockAuth.Object, _mockUserService.Object);
//        }

//        // ... other code ...

//        [Test]
//        public void Get_ShouldReturnOkResultWithUsers()
//        {
//            // Arrange
//            var users = new List<User> { new User { UserId = 1, UserName = "JohnDoe" } };
//            _mockUserService.Setup(s => s.GetUsers()).Returns(users);

//            // Act
//            var result = _userController.Get();

//            // Assert
//            Assert.That(result, Is.InstanceOf<OkObjectResult>());
//            var okResult = result as OkObjectResult;
//            Assert.That(okResult?.Value, Is.EqualTo(users));
//        }

//        [Test]
//        public void GetUser_ShouldReturnNotFound_WhenUserDoesNotExist()
//        {
//            // Arrange
//            _mockUserService.Setup(s => s.GetUser(It.IsAny<int>())).Throws(new CustomerNotFoundException("User not found"));

//            // Act
//            var result = _userController.GetUser(1);

//            // Assert
//            Assert.That(result, Is.InstanceOf<NotFoundObjectResult>());
//        }

//        [Test]
//        public void AddUser_ShouldReturnConflict_WhenUserAlreadyExists()
//        {
//            // Arrange
//            var user = new User { UserId = 1, UserName = "JohnDoe" };
//            _mockUserService.Setup(s => s.AddUser(user)).Throws(new CustomerAlreadyExistsException("User already exists"));

//            // Act
//            var result = _userController.AddUser(user);

//            // Assert
//            Assert.That(result, Is.InstanceOf<ConflictObjectResult>());
//        }

//        [Test]
//        public void Put_ShouldReturnNotFound_WhenUserDoesNotExist()
//        {
//            // Arrange
//            var user = new User { UserId = 1, UserName = "JohnDoe" };
//            _mockUserService.Setup(s => s.UpdateUser(It.IsAny<int>(), user)).Throws(new CustomerNotFoundException("User not found"));

//            // Act
//            var result = _userController.Put(1, user);

//            // Assert
//            Assert.That(result, Is.InstanceOf<NotFoundObjectResult>());
//        }

//        [Test]
//        public void Delete_ShouldReturnNotFound_WhenUserDoesNotExist()
//        {
//            // Arrange
//            _mockUserService.Setup(s => s.DeleteUser(It.IsAny<int>())).Throws(new CustomerNotFoundException("User not found"));

//            // Act
//            var result = _userController.Delete(1);

//            // Assert
//            Assert.That(result, Is.InstanceOf<NotFoundObjectResult>());
//        }

//        [Test]
//        public void Authentication_ShouldReturnUnauthorized_WhenCredentialsAreInvalid()
//        {
//            // Arrange
//            var credentials = new UserCredentials { Email = "test@example.com", Password = "wrongpassword" };
//            _mockAuth.Setup(a => a.Authentication(credentials.Email, credentials.Password)).Returns((string)null);

//            // Act
//            var result = _userController.Authentication(credentials);

//            // Assert
//            Assert.That(result, Is.InstanceOf<UnauthorizedResult>());
//        }
//    }
//}




using NUnit.Framework;
using Moq;
using Microsoft.AspNetCore.Mvc;
using e_comm.Controllers;
using e_comm.Models;
using e_comm.Services;
using e_comm.Auth;
using System.Collections.Generic;

namespace E_CommerceTest
{
    [TestFixture]
    public class UserControllerTests
    {
        private Mock<IUserService> _mockUserService;
        private Mock<IAuth> _mockAuth;
        private UserController _userController;

        [SetUp]
        public void Setup()
        {
            _mockUserService = new Mock<IUserService>();
            _mockAuth = new Mock<IAuth>();
            _userController = new UserController(_mockAuth.Object, _mockUserService.Object);
        }

        [Test]
        public void Get_ShouldReturnOkResultWithUsers()
        {
            // Arrange
            var users = new List<User> { new User { UserId = 1, UserName = "JohnDoe" } };
            _mockUserService.Setup(s => s.GetUsers()).Returns(users);

            // Act
            var result = _userController.Get();

            // Assert
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
            var okResult = result as OkObjectResult;
            Assert.That(okResult?.Value, Is.EqualTo(users));
        }
    }
}