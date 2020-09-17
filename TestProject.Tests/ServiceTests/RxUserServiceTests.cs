using DataAccess.DomainModels;
using DataAccess.Repositories;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using System;
using System.Collections.Generic;
using System.Text;
using TestProject.Library.Services;
using Xunit;

namespace TestProject.Tests.ServiceTests
{
    public class RxUserServiceTests
    {
        private readonly IRxUserService _sut;
        private readonly IRxUserRepository _RxUserRepository = Substitute.For<IRxUserRepository>();
        private readonly ILogger<RxUserService> _logger = Substitute.For<ILogger<RxUserService>>();

        public RxUserServiceTests()
        {
            _sut = new RxUserService(_RxUserRepository, _logger);

        }

        [Fact]
        public void GetByIdAsync_ShouldReturnUser_WhenUserExists()
        {
            var RxUserId = Guid.NewGuid();
            var RxUserFirst = "FirstName";
            var RxUserLast = "LastName";
            var RxUserdto = new RxUserModel { Id = RxUserId.ToString(), FirstName = RxUserFirst, LastName = RxUserLast };

            _RxUserRepository.GetById(RxUserId).Returns(RxUserdto);

            // Act
            var RxUser = _sut.GetById(RxUserId);

            // Assert
            Assert.Equal(RxUserId.ToString(), RxUser.Id);
            Assert.Equal(RxUserFirst, RxUser.FirstName);
            Assert.Equal(RxUserLast, RxUser.LastName);

        }

        [Fact]
        public void GetByIdAsync_ShouldReturnNull_WhenUserDoesNotExist()
        {
            // Arrange
            _RxUserRepository.GetById(Arg.Any<Guid>()).ReturnsNull();

            // Act
            var RxUser = _sut.GetById(Guid.NewGuid());

            // Assert
            Assert.Null(RxUser);
        }

        [Fact]
        public void GetById_ShouldLogAppropriateMessage_WhenCustomerExists()
        {
            // Arrange
            var RxUserId = Guid.NewGuid();
            var RxUserFirst = "FirstName";
            var RxUserLast = "LastName";
            var RxUserdto = new RxUserModel { Id = RxUserId.ToString(), FirstName = RxUserFirst, LastName = RxUserLast };

            _RxUserRepository.GetById(RxUserId).Returns(RxUserdto);

            // Act
            var RxUser = _sut.GetById(RxUserId);

            // Assert
            _logger.Received(1).LogInformation("Found a user");
            _logger.DidNotReceive().LogInformation("No user found!");
        }

        [Fact]
        public void GetById_ShouldLogAppropriateMessage_WhenCustomerDoesNotExist()
        {
            // Arrange
            _RxUserRepository.GetById(Arg.Any<Guid>()).ReturnsNull();

            // Act
            var RxUser = _sut.GetById(Guid.NewGuid());

            // Assert
            _logger.Received(1).LogWarning("No user found!");
            
        }
    }
}
