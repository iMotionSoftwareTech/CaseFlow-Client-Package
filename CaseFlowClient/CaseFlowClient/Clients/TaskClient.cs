using IMotionSoftware.CaseFlowClient.Interfaces;
using IMotionSoftware.CaseFlowClient.Utilities;
using IMotionSoftware.CaseFlowManager.API.Models.Models;
using IMotionSoftware.CaseFlowManager.API.Models.Request;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace IMotionSoftware.CaseFlowClient.Clients
{
    /// <summary>
    /// The TaskClient
    /// </summary>
    /// <seealso cref="IMotionSoftware.CaseFlowClient.Interfaces.ITaskClient" />
    public class TaskClient : ITaskClient
    {
        /// <summary>
        /// The HTTP client
        /// </summary>
        private readonly HttpClient _httpClient;

        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger<TaskClient> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="TaskClient"/> class.
        /// </summary>
        /// <param name="httpClient">The HTTP client.</param>
        /// <param name="logger">The logger.</param>
        public TaskClient(HttpClient httpClient, ILogger<TaskClient> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        /// <summary>
        /// Creates the case task asynchronous.
        /// </summary>
        /// <param name="createTaskRequest">The create task request.</param>
        /// <param name="ct">The ct.</param>
        /// <returns>
        /// The <see cref="Task{TResult}" />
        /// </returns>
        /// <exception cref="IMotionSoftware.CaseFlowClient.Utilities.ApiValidationException"></exception>
        /// <exception cref="IMotionSoftware.CaseFlowClient.Utilities.ApiServerException"></exception>
        public async Task<NewTask> CreateCaseTaskAsync(CreateTaskRequest createTaskRequest, CancellationToken ct = default)
        {
            var response = await _httpClient.PostAsJsonAsync("Task/CreateCaseTaskAsync", createTaskRequest, ct);

            if (response.IsSuccessStatusCode)
            {
                return (await response.Content.ReadFromJsonAsync<NewTask>(cancellationToken: ct))!;
            }

            var body = await response.Content.ReadFromJsonAsync<string>(ct);

            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                throw new ApiValidationException(body ?? "Bad Request");
            }

            _logger.LogError("Task creation failed. Status: {StatusCode}, Body: {Body}", response.StatusCode, body);

            throw new ApiServerException(response.StatusCode, body ?? "Bad Request");
        }

        /// <summary>
        /// Gets all case tasks asynchronous.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="ct">The ct.</param>
        /// <returns>
        /// The <see cref="Task{TResult}" />
        /// </returns>
        /// <exception cref="IMotionSoftware.CaseFlowClient.Utilities.ApiValidationException"></exception>
        /// <exception cref="IMotionSoftware.CaseFlowClient.Utilities.ApiServerException"></exception>
        public async Task<TaskRecord> GetAllCaseTasksAsync(int pageNumber, int pageSize, CancellationToken ct = default)
        {
            var response = await _httpClient.GetAsync($"Task/GetAllCaseTasksAsync/{pageNumber}/{pageSize}", ct);
            if (response.IsSuccessStatusCode)
            {
                return (await response.Content.ReadFromJsonAsync<TaskRecord>(cancellationToken: ct))!;
            }

            var body = await response.Content.ReadFromJsonAsync<string>(ct);

            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                throw new ApiValidationException(body ?? "Bad Request");
            }

            _logger.LogError("Getting tasks failed. Status: {StatusCode}, Body: {Body}",
                response.StatusCode, body);

            throw new ApiServerException(response.StatusCode, body ?? "Unexpected Error");
        }

        /// <summary>
        /// Gets all statuses asynchronous.
        /// </summary>
        /// <param name="ct">The ct.</param>
        /// <returns>
        /// The <see cref="Task{TResult}" />
        /// </returns>
        /// <exception cref="IMotionSoftware.CaseFlowClient.Utilities.ApiValidationException"></exception>
        /// <exception cref="IMotionSoftware.CaseFlowClient.Utilities.ApiServerException"></exception>
        public async Task<IEnumerable<Status>> GetAllStatusesAsync(CancellationToken ct = default)
        {
            var response = await _httpClient.GetAsync("Task/GetAllStatusesAsync", ct);
            if (response.IsSuccessStatusCode)
            {
                return (await response.Content.ReadFromJsonAsync<IEnumerable<Status>>(cancellationToken: ct))!;
            }

            var body = await response.Content.ReadFromJsonAsync<string>(ct);

            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                throw new ApiValidationException(body ?? "Bad Request");
            }

            _logger.LogError("Geting all statuses failed. Status: {StatusCode}, Body: {Body}",
                response.StatusCode, body);

            throw new ApiServerException(response.StatusCode, body ?? "Unexpected Error");
        }

        /// <summary>
        /// Gets the case task with statuses by identifier asynchronous.
        /// </summary>
        /// <param name="taskId">The task identifier.</param>
        /// <param name="ct">The ct.</param>
        /// <returns>
        /// The <see cref="Task{TResult}" />
        /// </returns>
        /// <exception cref="IMotionSoftware.CaseFlowClient.Utilities.ApiValidationException"></exception>
        /// <exception cref="IMotionSoftware.CaseFlowClient.Utilities.ApiServerException"></exception>
        public async Task<IEnumerable<CaseTaskStatus>> GetCaseTaskWithStatusesByIdAsync(int taskId, CancellationToken ct = default)
        {
            var response = await _httpClient.GetAsync($"Task/GetTaskWithStatusesByIdAsync/{taskId}", ct);
            if (response.IsSuccessStatusCode)
            {
                return (await response.Content.ReadFromJsonAsync<IEnumerable<CaseTaskStatus>>(cancellationToken: ct))!;
            }

            var body = await response.Content.ReadFromJsonAsync<string>(ct);

            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                throw new ApiValidationException(body ?? "Bad Request");
            }

            _logger.LogError("Geting task statuses failed. Status: {StatusCode}, Body: {Body}",
                response.StatusCode, body);

            throw new ApiServerException(response.StatusCode, body ?? "Unexpected Error");
        }

        /// <summary>
        /// Logs the case task status asynchronous.
        /// </summary>
        /// <param name="logStatusRequest">The log status request.</param>
        /// <param name="ct">The ct.</param>
        /// <returns>
        /// The <see cref="Task{TResult}" />
        /// </returns>
        /// <exception cref="IMotionSoftware.CaseFlowClient.Utilities.ApiValidationException"></exception>
        /// <exception cref="IMotionSoftware.CaseFlowClient.Utilities.ApiServerException"></exception>
        public async Task<TaskUpdate> LogCaseTaskStatusAsync(LogStatusRequest logStatusRequest, CancellationToken ct = default)
        {
            var response = await _httpClient.PostAsJsonAsync("Task/LogCaseTaskStatusAsync", logStatusRequest, ct);

            if (response.IsSuccessStatusCode)
            {
                return (await response.Content.ReadFromJsonAsync<TaskUpdate>(cancellationToken: ct))!;
            }

            var body = await response.Content.ReadFromJsonAsync<string>(ct);

            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                throw new ApiValidationException(body ?? "Bad Request");
            }

            _logger.LogError("Task status update failed. Status: {StatusCode}, Body: {Body}", response.StatusCode, body);

            throw new ApiServerException(response.StatusCode, body ?? "Bad Request");
        }

        /// <summary>
        /// Logs the bulk task statuses asynchronous.
        /// </summary>
        /// <param name="logTaskStatusParameters">The log task status parameters.</param>
        /// <param name="ct">The ct.</param>
        /// <returns>
        /// The <see cref="Task{TResult}" />
        /// </returns>
        /// <exception cref="IMotionSoftware.CaseFlowClient.Utilities.ApiValidationException"></exception>
        /// <exception cref="IMotionSoftware.CaseFlowClient.Utilities.ApiServerException"></exception>
        public async Task<BulkTaskUpdate> LogBulkTaskStatusesAsync(IEnumerable<LogStatusRequest> logTaskStatusParameters, CancellationToken ct = default)
        {
            var response = await _httpClient.PostAsJsonAsync("Task/LogBulkTaskStatusesAsync", logTaskStatusParameters, ct);

            if (response.IsSuccessStatusCode)
            {
                return (await response.Content.ReadFromJsonAsync<BulkTaskUpdate>(cancellationToken: ct))!;
            }

            var body = await response.Content.ReadFromJsonAsync<string>(ct);

            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                throw new ApiValidationException(body ?? "Bad Request");
            }

            _logger.LogError("Multiple task status updates failed. Status: {StatusCode}, Body: {Body}", response.StatusCode, body);

            throw new ApiServerException(response.StatusCode, body ?? "Bad Request");
        }
    }
}