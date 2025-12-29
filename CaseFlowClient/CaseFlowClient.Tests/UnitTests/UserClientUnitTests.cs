using IMotionSoftware.CaseFlowClient.Clients;
using IMotionSoftware.CaseFlowClient.Tests.Helpers;
using IMotionSoftware.CaseFlowClient.Utilities;
using IMotionSoftware.CaseFlowManager.API.Models.Request;
using Microsoft.Extensions.Logging;
using Moq;
using System.Net;

namespace IMotionSoftware.CaseFlowClient.Tests
{
    /// <summary>
    /// The UserClientUnitTests
    /// </summary>
    [TestClass]
    public class UserClientUnitTests
    {
        /// <summary>
        /// Creates the caseworker user asynchronous when success returns new user.
        /// </summary>
        [TestMethod, TestCategory("UnitTest")]
        public async Task CreateCaseworkerUserAsync_WhenSuccess_ReturnsNewUser()
        {
            // Arrange
            var expected = MockData.GetNewUser();
            var httpClient = HttpClientHelper.CreateHttpClient(HttpStatusCode.OK, expected);
            var logger = Mock.Of<ILogger<UserClient>>();
            var client = new UserClient(httpClient, logger);

            // Act
            var result = await client.CreateCaseworkerUserAsync(MockData.GetCreateUserRequest());

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(1, result.CaseworkerId);
        }

        /// <summary>
        /// Creates the caseworker user asynchronous when bad request throws API validation exception.
        /// </summary>
        [TestMethod, TestCategory("UnitTest")]
        public async Task CreateCaseworkerUserAsync_WhenBadRequest_ThrowsApiValidationException()
        {
            // Arrange
            var errorMessage = "User already exists";

            var httpClient = HttpClientHelper.CreateHttpClient(
                HttpStatusCode.BadRequest,
                errorMessage);

            var logger = Mock.Of<ILogger<UserClient>>();
            var client = new UserClient(httpClient, logger);

            // Act + Assert
            var ex = await Assert.ThrowsExceptionAsync<ApiValidationException>(
                () => client.CreateCaseworkerUserAsync(new CreateUserRequest()));
            var message = ex.Message;

            Assert.AreEqual(errorMessage, message);
        }

        /// <summary>
        /// Creates the caseworker user asynchronous when server error throws API server exception and logs.
        /// </summary>
        [TestMethod, TestCategory("UnitTest")]
        public async Task CreateCaseworkerUserAsync_WhenServerError_ThrowsApiServerException_AndLogs()
        {
            // Arrange
            var httpClient = HttpClientHelper.CreateHttpClient(
                HttpStatusCode.InternalServerError,
                "DB failure");

            var loggerMock = new Mock<ILogger<UserClient>>();
            var client = new UserClient(httpClient, loggerMock.Object);

            // Act
            var ex = await Assert.ThrowsExceptionAsync<ApiServerException>(
                () => client.CreateCaseworkerUserAsync(new CreateUserRequest()));

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

        /// <summary>
        /// Gets the caseworker user asynchronous when success returns user details.
        /// </summary>
        [TestMethod, TestCategory("UnitTest")]
        public async Task GetCaseworkerUserAsync_WhenSuccess_ReturnsUserDetails()
        {
            // Arrange
            var expected = MockData.GetUserDetail();
            var httpClient = HttpClientHelper.CreateHttpClient(HttpStatusCode.OK, expected);
            var logger = Mock.Of<ILogger<UserClient>>();
            var client = new UserClient(httpClient, logger);

            // Act
            var result = await client.GetCaseworkerUserAsync("j.doe@test.com");

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expected.CaseworkerId, result.CaseworkerId);
            Assert.AreEqual(expected.Forename, result.Forename);
            Assert.AreEqual(expected.Surname, result.Surname);
            Assert.AreEqual(expected.Role, result.Role);
            Assert.AreEqual(expected.Email, result.Email);
        }

        /// <summary>
        /// Gets the caseworker user asynchronous when bad request throws API validation exception.
        /// </summary>
        [TestMethod, TestCategory("UnitTest")]
        public async Task GetCaseworkerUserAsync_WhenBadRequest_ThrowsApiValidationException()
        {
            // Arrange
            var errorMessage = "No user found";

            var httpClient = HttpClientHelper.CreateHttpClient(
                HttpStatusCode.BadRequest,
                errorMessage);

            var logger = Mock.Of<ILogger<UserClient>>();
            var client = new UserClient(httpClient, logger);

            // Act + Assert
            var ex = await Assert.ThrowsExceptionAsync<ApiValidationException>(
                () => client.GetCaseworkerUserAsync("j.doe@test.com"));
            var message = ex.Message;

            Assert.AreEqual(errorMessage, message);
        }

        /// <summary>
        /// Gets the caseworker user asynchronous when server error throws API server exception and logs.
        /// </summary>
        [TestMethod, TestCategory("UnitTest")]
        public async Task GetCaseworkerUserAsync_WhenServerError_ThrowsApiServerException_AndLogs()
        {
            // Arrange
            var httpClient = HttpClientHelper.CreateHttpClient(
                HttpStatusCode.InternalServerError,
                "DB failure to retreive users");

            var loggerMock = new Mock<ILogger<UserClient>>();
            var client = new UserClient(httpClient, loggerMock.Object);

            // Act
            var ex = await Assert.ThrowsExceptionAsync<ApiServerException>(
                () => client.GetCaseworkerUserAsync(string.Empty));

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

        /// <summary>
        /// Updates the user password attempt asynchronous when success returns update password attempt success.
        /// </summary>
        [TestMethod, TestCategory("UnitTest")]
        public async Task UpdateUserPasswordAttemptAsync_WhenSuccess_ReturnsUpdatePasswordAttemptSuccess()
        {
            // Arrange
            var expected = MockData.GetPasswordAttempt();
            var httpClient = HttpClientHelper.CreateHttpClient(HttpStatusCode.OK, expected);
            var logger = Mock.Of<ILogger<UserClient>>();
            var client = new UserClient(httpClient, logger);

            // Act
            var result = await client.UpdateUserPasswordAttemptAsync(MockData.GetPasswordAttemptRequest());

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsSuccess);
        }

        /// <summary>
        /// Updates the user password attempt asynchronous when bad request throws API validation exception.
        /// </summary>
        [TestMethod, TestCategory("UnitTest")]
        public async Task UpdateUserPasswordAttemptAsync_WhenBadRequest_ThrowsApiValidationException()
        {
            // Arrange
            var errorMessage = "Unable to update password attempt";

            var httpClient = HttpClientHelper.CreateHttpClient(
                HttpStatusCode.BadRequest,
                errorMessage);

            var logger = Mock.Of<ILogger<UserClient>>();
            var client = new UserClient(httpClient, logger);

            // Act + Assert
            var ex = await Assert.ThrowsExceptionAsync<ApiValidationException>(
                () => client.UpdateUserPasswordAttemptAsync(new PasswordAttemptRequest()));
            var message = ex.Message;

            Assert.AreEqual(errorMessage, message);
        }

        /// <summary>
        /// Updates the user password attempt asynchronous when server error throws API server exception and logs.
        /// </summary>
        [TestMethod, TestCategory("UnitTest")]
        public async Task UpdateUserPasswordAttemptAsync_WhenServerError_ThrowsApiServerException_AndLogs()
        {
            // Arrange
            var httpClient = HttpClientHelper.CreateHttpClient(
                HttpStatusCode.InternalServerError,
                "DB failure to update user password attempt");

            var loggerMock = new Mock<ILogger<UserClient>>();
            var client = new UserClient(httpClient, loggerMock.Object);

            // Act
            var ex = await Assert.ThrowsExceptionAsync<ApiServerException>(
                () => client.UpdateUserPasswordAttemptAsync(new PasswordAttemptRequest()));

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