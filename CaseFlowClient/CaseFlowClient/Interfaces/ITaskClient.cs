using IMotionSoftware.CaseFlowManager.API.Models.Models;
using IMotionSoftware.CaseFlowManager.API.Models.Request;

namespace IMotionSoftware.CaseFlowClient.Interfaces
{
    /// <summary>
    /// The ITaskClient
    /// </summary>
    public interface ITaskClient
    {
        /// <summary>
        /// Creates the case task asynchronous.
        /// </summary>
        /// <param name="createTaskRequest">The create task request.</param>
        /// <param name="ct">The ct.</param>
        /// <returns>The <see cref="Task{TResult}" /></returns>
        Task<NewTask> CreateCaseTaskAsync(CreateTaskRequest createTaskRequest, CancellationToken ct = default);

        /// <summary>
        /// Gets all statuses asynchronous.
        /// </summary>
        /// <param name="ct">The ct.</param>
        /// <returns>The <see cref="Task{TResult}" /></returns>
        Task<IEnumerable<Status>> GetAllStatusesAsync(CancellationToken ct = default);

        /// <summary>
        /// Gets all case tasks asynchronous.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="ct">The ct.</param>
        /// <returns>The <see cref="Task{TResult}" /></returns>
        Task<TaskRecord> GetAllCaseTasksAsync(int pageNumber, int pageSize, CancellationToken ct = default);

        /// <summary>
        /// Gets the case task with statuses by identifier asynchronous.
        /// </summary>
        /// <param name="taskId">The task identifier.</param>
        /// <param name="ct">The ct.</param>
        /// <returns>The <see cref="Task{TResult}" /></returns>
        Task<IEnumerable<CaseTaskStatus>> GetCaseTaskWithStatusesByIdAsync(int taskId, CancellationToken ct = default);

        /// <summary>
        /// Logs the case task status asynchronous.
        /// </summary>
        /// <param name="logStatusRequest">The log status request.</param>
        /// <param name="ct">The ct.</param>
        /// <returns>The <see cref="Task{TResult}" /></returns>
        Task<TaskUpdate> LogCaseTaskStatusAsync(LogStatusRequest logStatusRequest, CancellationToken ct = default);

        /// <summary>
        /// Logs the bulk task statuses asynchronous.
        /// </summary>
        /// <param name="logTaskStatusParameters">The log task status parameters.</param>
        /// <param name="ct">The ct.</param>
        /// <returns>The <see cref="Task{TResult}" /></returns>
        Task<BulkTaskUpdate> LogBulkTaskStatusesAsync(IEnumerable<LogStatusRequest> logTaskStatusParameters, CancellationToken ct = default);
    }
}