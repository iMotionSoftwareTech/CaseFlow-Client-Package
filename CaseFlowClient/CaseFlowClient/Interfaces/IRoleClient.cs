using IMotionSoftware.CaseFlowManager.API.Models.Models;
using IMotionSoftware.CaseFlowManager.API.Models.Request;

namespace IMotionSoftware.CaseFlowClient.Interfaces
{
    /// <summary>
    /// The IRoleClient interface.
    /// </summary>
    public interface IRoleClient
    {
        /// <summary>
        /// Creates the caseworker role asynchronous.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="ct">The ct.</param>
        /// <returns>The <see cref="Task{TResult}"/></returns>
        Task<NewRole> CreateCaseworkerRoleAsync(CreateRoleRequest request, CancellationToken ct = default);

        /// <summary>
        /// Gets all roles asynchronous.
        /// </summary>
        /// <param name="ct">The ct.</param>
        /// <returns>The <see cref="Task{TResult}"/></returns>
        Task<IEnumerable<CaseworkerRole>> GetAllRolesAsync(CancellationToken ct = default);
    }
}