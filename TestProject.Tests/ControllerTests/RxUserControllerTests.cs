using DataAccess.DomainModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using System;
using System.Collections.Generic;
using System.Text;
using TestProject.API.Controllers;
using TestProject.Library.Services;
using Xunit;

namespace TestProject.Tests.ControllerTests
{
    public class RxUserControllerTests
    {
        private readonly RxUserController _sut;
        private readonly IRxUserService _RxUserService = Substitute.For<IRxUserService>();
        private readonly ILogger<RxUserController> _logger = Substitute.For<ILogger<RxUserController>>();

        public RxUserControllerTests()
        {
            _sut = new RxUserController(_RxUserService, _logger);
        }

        [Fact]
        public void GetRxUser_ShouldReturnOkObjectResult_WhenUserExists()
        {
            // Arrange
            var RxUserId = Guid.NewGuid();
            var RxUserFirst = "FirstName";
            var RxUserLast = "LastName";
            var RxUser = new RxUserModel { Id = RxUserId.ToString(), FirstName = RxUserFirst, LastName = RxUserLast };

            _RxUserService.GetById(RxUserId).Returns(RxUser);

            // Act
            var result = _sut.GetRxUser(Guid.Parse(RxUser.Id));

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void GetRxUser_ShouldReturnNotFoundResult_WhenUserDoesNotExists()
        {
            // Arrange
           _RxUserService.GetById(Arg.Any<Guid>()).ReturnsNull();

            // Act
            var result = _sut.GetRxUser(Guid.NewGuid());

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
