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
    /// The TaskClientUnitTests
    /// </summary>
    [TestClass]
    public class TaskClientUnitTests
    {
        /// <summary>
        /// Creates the case task asynchronous when success returns new task.
        /// </summary>
        [TestMethod, TestCategory("UnitTest")]
        public async Task CreateCaseTaskAsync_WhenSuccess_ReturnsNewTask()
        {
            // Arrange
            var expected = MockData.GetNewTask();
            var httpClient = HttpClientHelper.CreateHttpClient(HttpStatusCode.OK, expected);
            var logger = Mock.Of<ILogger<TaskClient>>();
            var client = new TaskClient(httpClient, logger);

            // Act
            var result = await client.CreateCaseTaskAsync(MockData.GetCreateTaskRequest());

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(102, result.TaskId);
        }

        /// <summary>
        /// Creates the case task asynchronous when bad request throws API validation exception.
        /// </summary>
        [TestMethod, TestCategory("UnitTest")]
        public async Task CreateCaseTaskAsync_WhenBadRequest_ThrowsApiValidationException()
        {
            // Arrange
            var errorMessage = "Role already exists";

            var httpClient = HttpClientHelper.CreateHttpClient(
                HttpStatusCode.BadRequest,
                errorMessage);

            var logger = Mock.Of<ILogger<TaskClient>>();
            var client = new TaskClient(httpClient, logger);

            // Act + Assert
            var ex = await Assert.ThrowsExceptionAsync<ApiValidationException>(
                () => client.CreateCaseTaskAsync(new CreateTaskRequest()));
            var message = ex.Message;

            Assert.AreEqual(errorMessage, message);
        }

        /// <summary>
        /// Creates the case task asynchronous when server error throws API server exception and logs.
        /// </summary>
        [TestMethod, TestCategory("UnitTest")]
        public async Task CreateCaseTaskAsync_WhenServerError_ThrowsApiServerException_AndLogs()
        {
            // Arrange
            var httpClient = HttpClientHelper.CreateHttpClient(
                HttpStatusCode.InternalServerError,
                "DB failure to create task");

            var loggerMock = new Mock<ILogger<TaskClient>>();
            var client = new TaskClient(httpClient, loggerMock.Object);

            // Act
            var ex = await Assert.ThrowsExceptionAsync<ApiServerException>(
                () => client.CreateCaseTaskAsync(new CreateTaskRequest()));

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
        /// Logs the case task status asynchronous when success returns task update.
        /// </summary>
        [TestMethod, TestCategory("UnitTest")]
        public async Task LogCaseTaskStatusAsync_WhenSuccess_ReturnsTaskUpdate()
        {
            // Arrange
            var expected = MockData.GetTaskUpdate();
            var httpClient = HttpClientHelper.CreateHttpClient(HttpStatusCode.OK, expected);
            var logger = Mock.Of<ILogger<TaskClient>>();
            var client = new TaskClient(httpClient, logger);

            // Act
            var result = await client.LogCaseTaskStatusAsync(MockData.GetLogStatusRequests().First());

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(3, result.TaskStatusId);
        }

        /// <summary>
        /// Logs the case task status asynchronous when bad request throws API validation exception.
        /// </summary>
        [TestMethod, TestCategory("UnitTest")]
        public async Task LogCaseTaskStatusAsync_WhenBadRequest_ThrowsApiValidationException()
        {
            // Arrange
            var errorMessage = "Task Status already updated";

            var httpClient = HttpClientHelper.CreateHttpClient(
                HttpStatusCode.BadRequest,
                errorMessage);

            var logger = Mock.Of<ILogger<TaskClient>>();
            var client = new TaskClient(httpClient, logger);

            // Act + Assert
            var ex = await Assert.ThrowsExceptionAsync<ApiValidationException>(
                () => client.LogCaseTaskStatusAsync(new LogStatusRequest()));
            var message = ex.Message;

            Assert.AreEqual(errorMessage, message);
        }

        /// <summary>
        /// Logs the case task status asynchronous when server error throws API server exception and logs.
        /// </summary>
        [TestMethod, TestCategory("UnitTest")]
        public async Task LogCaseTaskStatusAsync_WhenServerError_ThrowsApiServerException_AndLogs()
        {
            // Arrange
            var httpClient = HttpClientHelper.CreateHttpClient(
                HttpStatusCode.InternalServerError,
                "DB failure to update task status");

            var loggerMock = new Mock<ILogger<TaskClient>>();
            var client = new TaskClient(httpClient, loggerMock.Object);

            // Act
            var ex = await Assert.ThrowsExceptionAsync<ApiServerException>(
                () => client.LogCaseTaskStatusAsync(new LogStatusRequest()));

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
        /// Logs the bulk task statuses asynchronous when success returns new role.
        /// </summary>
        [TestMethod, TestCategory("UnitTest")]
        public async Task LogBulkTaskStatusesAsync_WhenSuccess_ReturnsNewRole()
        {
            // Arrange
            var expected = MockData.GetBulkTaskUpdate();
            var httpClient = HttpClientHelper.CreateHttpClient(HttpStatusCode.OK, expected);
            var logger = Mock.Of<ILogger<TaskClient>>();
            var client = new TaskClient(httpClient, logger);

            // Act
            var result = await client.LogBulkTaskStatusesAsync(MockData.GetLogStatusRequests());

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(expected.InsertedCount, result.InsertedCount);
        }

        /// <summary>
        /// Logs the bulk task statuses asynchronous when bad request throws API validation exception.
        /// </summary>
        [TestMethod, TestCategory("UnitTest")]
        public async Task LogBulkTaskStatusesAsync_WhenBadRequest_ThrowsApiValidationException()
        {
            // Arrange
            var errorMessage = "Task status already updated";

            var httpClient = HttpClientHelper.CreateHttpClient(
                HttpStatusCode.BadRequest,
                errorMessage);

            var logger = Mock.Of<ILogger<TaskClient>>();
            var client = new TaskClient(httpClient, logger);

            // Act + Assert
            var ex = await Assert.ThrowsExceptionAsync<ApiValidationException>(
                () => client.LogBulkTaskStatusesAsync(new List<LogStatusRequest>()));
            var message = ex.Message;

            Assert.AreEqual(errorMessage, message);
        }

        /// <summary>
        /// Logs the bulk task statuses asynchronous when server error throws API server exception and logs.
        /// </summary>
        [TestMethod, TestCategory("UnitTest")]
        public async Task LogBulkTaskStatusesAsync_WhenServerError_ThrowsApiServerException_AndLogs()
        {
            // Arrange
            var httpClient = HttpClientHelper.CreateHttpClient(
                HttpStatusCode.InternalServerError,
                "DB failure to update multiple task with statuses");

            var loggerMock = new Mock<ILogger<TaskClient>>();
            var client = new TaskClient(httpClient, loggerMock.Object);

            // Act
            var ex = await Assert.ThrowsExceptionAsync<ApiServerException>(
                () => client.LogBulkTaskStatusesAsync(new List<LogStatusRequest>()));

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
        /// Gets all case tasks asynchronous when success returns case tasks.
        /// </summary>
        [TestMethod, TestCategory("UnitTest")]
        public async Task GetAllCaseTasksAsync_WhenSuccess_ReturnsCaseTasks()
        {
            // Arrange
            var expected = MockData.GetTaskRecord();
            var expectedTasks = expected.Tasks.ToList();
            var httpClient = HttpClientHelper.CreateHttpClient(HttpStatusCode.OK, expected);
            var logger = Mock.Of<ILogger<TaskClient>>();
            var client = new TaskClient(httpClient, logger);

            // Act
            var result = (await client.GetAllCaseTasksAsync(1, 10));
            var actualTasks = result.Tasks.ToList();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expected.TotalNoOfRecords, result.TotalNoOfRecords);
            for (int i = 0; i < expectedTasks.Count(); i++)
            {
                Assert.AreEqual(expectedTasks[i].TaskId, actualTasks[i].TaskId);
                Assert.AreEqual(expectedTasks[i].Title, actualTasks[i].Title);
                Assert.AreEqual(expectedTasks[i].Description, actualTasks[i].Description);
                Assert.AreEqual(expectedTasks[i].Status, actualTasks[i].Status);
                Assert.AreEqual(expectedTasks[i].DueDateTime, actualTasks[i].DueDateTime);
            }
        }

        /// <summary>
        /// Gets all case tasks asynchronous when bad request throws API validation exception.
        /// </summary>
        [TestMethod, TestCategory("UnitTest")]
        public async Task GetAllCaseTasksAsync_WhenBadRequest_ThrowsApiValidationException()
        {
            // Arrange
            var errorMessage = "No tasks found";

            var httpClient = HttpClientHelper.CreateHttpClient(
                HttpStatusCode.BadRequest,
                errorMessage);

            var logger = Mock.Of<ILogger<TaskClient>>();
            var client = new TaskClient(httpClient, logger);

            // Act + Assert
            var ex = await Assert.ThrowsExceptionAsync<ApiValidationException>(
                () => client.GetAllCaseTasksAsync(1, 1));
            var message = ex.Message;

            Assert.AreEqual(errorMessage, message);
        }

        /// <summary>
        /// Gets all case tasks asynchronous when server error throws API server exception and logs.
        /// </summary>
        [TestMethod, TestCategory("UnitTest")]
        public async Task GetAllCaseTasksAsync_WhenServerError_ThrowsApiServerException_AndLogs()
        {
            // Arrange
            var httpClient = HttpClientHelper.CreateHttpClient(
                HttpStatusCode.InternalServerError,
                "DB failure to retreive tasks");

            var loggerMock = new Mock<ILogger<TaskClient>>();
            var client = new TaskClient(httpClient, loggerMock.Object);

            // Act
            var ex = await Assert.ThrowsExceptionAsync<ApiServerException>(
                () => client.GetAllCaseTasksAsync(0, 0));

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
        /// Gets all statuses asynchronous when success returns all statuses.
        /// </summary>
        [TestMethod, TestCategory("UnitTest")]
        public async Task GetAllStatusesAsync_WhenSuccess_ReturnsAllStatuses()
        {
            // Arrange
            var expected = MockData.GetStatuses().ToList();
            var httpClient = HttpClientHelper.CreateHttpClient(HttpStatusCode.OK, expected);
            var logger = Mock.Of<ILogger<TaskClient>>();
            var client = new TaskClient(httpClient, logger);

            // Act
            var result = (await client.GetAllStatusesAsync()).ToList();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expected.Count(), result.Count());
            for (int i = 0; i < expected.Count(); i++)
            {
                Assert.AreEqual(expected[i].Id, result[i].Id);
                Assert.AreEqual(expected[i].Title, result[i].Title);
            }
        }

        /// <summary>
        /// Gets all statuses asynchronous when bad request throws API validation exception.
        /// </summary>
        [TestMethod, TestCategory("UnitTest")]
        public async Task GetAllStatusesAsync_WhenBadRequest_ThrowsApiValidationException()
        {
            // Arrange
            var errorMessage = "No statuses found";

            var httpClient = HttpClientHelper.CreateHttpClient(
                HttpStatusCode.BadRequest,
                errorMessage);

            var logger = Mock.Of<ILogger<TaskClient>>();
            var client = new TaskClient(httpClient, logger);

            // Act + Assert
            var ex = await Assert.ThrowsExceptionAsync<ApiValidationException>(
                () => client.GetAllStatusesAsync());
            var message = ex.Message;

            Assert.AreEqual(errorMessage, message);
        }

        /// <summary>
        /// Gets all statuses asynchronous when server error throws API server exception and logs.
        /// </summary>
        [TestMethod, TestCategory("UnitTest")]
        public async Task GetAllStatusesAsync_WhenServerError_ThrowsApiServerException_AndLogs()
        {
            // Arrange
            var httpClient = HttpClientHelper.CreateHttpClient(
                HttpStatusCode.InternalServerError,
                "DB failure to retreive statuses");

            var loggerMock = new Mock<ILogger<TaskClient>>();
            var client = new TaskClient(httpClient, loggerMock.Object);

            // Act
            var ex = await Assert.ThrowsExceptionAsync<ApiServerException>(
                () => client.GetAllStatusesAsync());

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
        /// Gets the case task with statuses by identifier asynchronous when success returns task statuses.
        /// </summary>
        [TestMethod, TestCategory("UnitTest")]
        public async Task GetCaseTaskWithStatusesByIdAsync_WhenSuccess_ReturnsTaskStatuses()
        {
            // Arrange
            var expected = MockData.GetTaskStatuses().ToList();
            var httpClient = HttpClientHelper.CreateHttpClient(HttpStatusCode.OK, expected);
            var logger = Mock.Of<ILogger<TaskClient>>();
            var client = new TaskClient(httpClient, logger);

            // Act
            var result = (await client.GetCaseTaskWithStatusesByIdAsync(1)).ToList();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expected.Count(), result.Count());
            for (int i = 0; i < expected.Count(); i++)
            {
                Assert.AreEqual(expected[i].Id, result[i].Id);
                Assert.AreEqual(expected[i].CaseWorker, result[i].CaseWorker);
                Assert.AreEqual(expected[i].TaskId, result[i].TaskId);
                Assert.AreEqual(expected[i].StatusId, result[i].StatusId);
                Assert.AreEqual(expected[i].Status, result[i].Status);
            }
        }

        /// <summary>
        /// Gets the case task with statuses by identifier asynchronous when bad request throws API validation exception.
        /// </summary>
        [TestMethod, TestCategory("UnitTest")]
        public async Task GetCaseTaskWithStatusesByIdAsync_WhenBadRequest_ThrowsApiValidationException()
        {
            // Arrange
            var errorMessage = "No task statuses found";

            var httpClient = HttpClientHelper.CreateHttpClient(
                HttpStatusCode.BadRequest,
                errorMessage);

            var logger = Mock.Of<ILogger<TaskClient>>();
            var client = new TaskClient(httpClient, logger);

            // Act + Assert
            var ex = await Assert.ThrowsExceptionAsync<ApiValidationException>(
                () => client.GetCaseTaskWithStatusesByIdAsync(1));
            var message = ex.Message;

            Assert.AreEqual(errorMessage, message);
        }

        /// <summary>
        /// Gets the case task with statuses by identifier asynchronous when server error throws API server exception and logs.
        /// </summary>
        [TestMethod, TestCategory("UnitTest")]
        public async Task GetCaseTaskWithStatusesByIdAsync_WhenServerError_ThrowsApiServerException_AndLogs()
        {
            // Arrange
            var httpClient = HttpClientHelper.CreateHttpClient(
                HttpStatusCode.InternalServerError,
                "DB failure to retreive task statuses");

            var loggerMock = new Mock<ILogger<TaskClient>>();
            var client = new TaskClient(httpClient, loggerMock.Object);

            // Act
            var ex = await Assert.ThrowsExceptionAsync<ApiServerException>(
                () => client.GetCaseTaskWithStatusesByIdAsync(0));

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