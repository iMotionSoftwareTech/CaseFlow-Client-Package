using IMotionSoftware.CaseFlowManager.API.Models.Models;
using IMotionSoftware.CaseFlowManager.API.Models.Request;

namespace IMotionSoftware.CaseFlowClient.Interfaces
{
    /// <summary>
    /// The IUserClient
    /// </summary>
    public interface IUserClient
    {
        /// <summary>
        /// Creates the caseworker user asynchronous.
        /// </summary>
        /// <param name="createUserRequest">The create user request.</param>
        /// <param name="ct">The ct.</param>
        /// <returns>The <see cref="Task{TResult}"/></returns>
        Task<NewUser> CreateCaseworkerUserAsync(CreateUserRequest createUserRequest, CancellationToken ct = default);

        /// <summary>
        /// Gets the caseworker user asynchronous.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="ct">The ct.</param>
        /// <returns>The <see cref="Task{TResult}"/></returns>
        Task<UserDetail> GetCaseworkerUserAsync(string email, CancellationToken ct = default);

        /// <summary>
        /// Updates the user password attempt asynchronous.
        /// </summary>
        /// <param name="passwordAttemptRequest">The password attempt request.</param>
        /// <param name="ct">The ct.</param>
        /// <returns>The <see cref="Task{TResult}"/></returns>
        Task<PasswordAttempt> UpdateUserPasswordAttemptAsync(PasswordAttemptRequest passwordAttemptRequest, CancellationToken ct = default);
    }
}