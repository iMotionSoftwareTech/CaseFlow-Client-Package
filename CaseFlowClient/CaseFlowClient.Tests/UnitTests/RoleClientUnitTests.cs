using IMotionSoftware.CaseFlowClient.Clients;
using IMotionSoftware.CaseFlowClient.Tests.Helpers;
using IMotionSoftware.CaseFlowClient.Utilities;
using IMotionSoftware.CaseFlowManager.API.Models.Models;
using IMotionSoftware.CaseFlowManager.API.Models.Request;
using Microsoft.Extensions.Logging;
using Moq;
using System.Net;

namespace IMotionSoftware.CaseFlowClient.Tests.UnitTests
{
    /// <summary>
    /// The RoleClientUnitTests
    /// </summary>
    [TestClass]
    public class RoleClientUnitTests
    {
        /// <summary>
        /// Creates the caseworker role asynchronous when success returns new role.
        /// </summary>
        [TestMethod, TestCategory("UnitTest")]
        public async Task CreateCaseworkerRoleAsync_WhenSuccess_ReturnsNewRole()
        {
            // Arrange
            var expected = new NewRole { IsSuccess = true, RoleId = 42 };
            var httpClient = HttpClientHelper.CreateHttpClient(HttpStatusCode.OK, expected);
            var logger = Mock.Of<ILogger<RoleClient>>();
            var client = new RoleClient(httpClient, logger);

            // Act
            var result = await client.CreateCaseworkerRoleAsync(new CreateRoleRequest
            {
                RoleName = "Caseworker", Description = "Caseworker Role"
            });

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(42, result.RoleId);
        }

        /// <summary>
        /// Creates the caseworker role asynchronous when bad request throws API validation exception.
        /// </summary>
        [TestMethod, TestCategory("UnitTest")]
        public async Task CreateCaseworkerRoleAsync_WhenBadRequest_ThrowsApiValidationException()
        {
            // Arrange
            var errorMessage = "Role already exists";

            var httpClient = HttpClientHelper.CreateHttpClient(
                HttpStatusCode.BadRequest,
                errorMessage);

            var logger = Mock.Of<ILogger<RoleClient>>();
            var client = new RoleClient(httpClient, logger);

            // Act + Assert
            var ex = await Assert.ThrowsExceptionAsync<ApiValidationException>(
                () => client.CreateCaseworkerRoleAsync(new CreateRoleRequest()));
            var message = ex.Message;

            Assert.AreEqual(errorMessage, message);
        }

        /// <summary>
        /// Creates the caseworker role asynchronous when server error throws API server exception and logs.
        /// </summary>
        [TestMethod, TestCategory("UnitTest")]
        public async Task CreateCaseworkerRoleAsync_WhenServerError_ThrowsApiServerException_AndLogs()
        {
            // Arrange
            var httpClient = HttpClientHelper.CreateHttpClient(
                HttpStatusCode.InternalServerError,
                "DB failure");

            var loggerMock = new Mock<ILogger<RoleClient>>();
            var client = new RoleClient(httpClient, loggerMock.Object);

            // Act
            var ex = await Assert.ThrowsExceptionAsync<ApiServerException>(
                () => client.CreateCaseworkerRoleAsync(new CreateRoleRequest()));

            // Assert
            Assert.AreEqual(HttpStatusCode.InternalServerError, ex.StatusCode);

            loggerMock.Verify(
                x => x.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.IsAny<It.IsAnyType>(),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
                Times.Once);
        }
    }
}