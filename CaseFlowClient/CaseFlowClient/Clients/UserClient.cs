using IMotionSoftware.CaseFlowClient.Interfaces;
using IMotionSoftware.CaseFlowClient.Utilities;
using IMotionSoftware.CaseFlowManager.API.Models.Models;
using IMotionSoftware.CaseFlowManager.API.Models.Request;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.Http.Json;

namespace IMotionSoftware.CaseFlowClient.Clients
{
    /// <summary>
    /// The UserClient
    /// </summary>
    /// <seealso cref="IMotionSoftware.CaseFlowClient.Interfaces.IUserClient" />
    public class UserClient : IUserClient
    {
        /// <summary>
        /// The HTTP client
        /// </summary>
        private readonly HttpClient _httpClient;

        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger<UserClient> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserClient"/> class.
        /// </summary>
        /// <param name="httpClient">The HTTP client.</param>
        /// <param name="logger">The logger.</param>
        public UserClient(HttpClient httpClient, ILogger<UserClient> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        /// <summary>
        /// Creates the caseworker user asynchronous.
        /// </summary>
        /// <param name="createUserRequest">The create user request.</param>
        /// <param name="ct">The ct.</param>
        /// <returns>
        /// The <see cref="Task{TResult}" />
        /// </returns>
        /// <exception cref="IMotionSoftware.CaseFlowClient.Utilities.ApiValidationException"></exception>
        /// <exception cref="IMotionSoftware.CaseFlowClient.Utilities.ApiServerException"></exception>
        public async Task<NewUser> CreateCaseworkerUserAsync(CreateUserRequest createUserRequest, CancellationToken ct = default)
        {
            var response = await _httpClient.PostAsJsonAsync("User/CreateNewUserAsync", createUserRequest, ct);

            if (response.IsSuccessStatusCode)
            {
                return (await response.Content.ReadFromJsonAsync<NewUser>(cancellationToken: ct))!;
            }

            var body = await response.Content.ReadFromJsonAsync<string>(ct);

            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                throw new ApiValidationException(body ?? "Bad Request");
            }

            _logger.LogError("User creation failed. Status: {StatusCode}, Body: {Body}", response.StatusCode, body);

            throw new ApiServerException(response.StatusCode, body ?? "Bad Request");
        }

        /// <summary>
        /// Gets the caseworker user asynchronous.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="ct">The ct.</param>
        /// <returns>
        /// The <see cref="Task{TResult}" />
        /// </returns>
        /// <exception cref="IMotionSoftware.CaseFlowClient.Utilities.ApiValidationException"></exception>
        /// <exception cref="IMotionSoftware.CaseFlowClient.Utilities.ApiServerException"></exception>
        public async Task<UserDetail> GetCaseworkerUserAsync(string email, CancellationToken ct = default)
        {
            var response = await _httpClient.GetAsync($"User/GetUser/{email}", ct);
            if (response.IsSuccessStatusCode)
            {
                return (await response.Content.ReadFromJsonAsync<UserDetail>(cancellationToken: ct))!;
            }

            var body = await response.Content.ReadFromJsonAsync<string>(ct);

            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                throw new ApiValidationException(body ?? "Bad Request");
            }

            _logger.LogError("Getting user failed. Status: {StatusCode}, Body: {Body}",
                response.StatusCode, body);

            throw new ApiServerException(response.StatusCode, body ?? "Unexpected Error");
        }

        /// <summary>
        /// Updates the user password attempt asynchronous.
        /// </summary>
        /// <param name="passwordAttemptRequest">The password attempt request.</param>
        /// <param name="ct">The ct.</param>
        /// <returns>
        /// The <see cref="Task{TResult}" />
        /// </returns>
        /// <exception cref="IMotionSoftware.CaseFlowClient.Utilities.ApiValidationException"></exception>
        /// <exception cref="IMotionSoftware.CaseFlowClient.Utilities.ApiServerException"></exception>
        public async Task<PasswordAttempt> UpdateUserPasswordAttemptAsync(PasswordAttemptRequest passwordAttemptRequest, CancellationToken ct = default)
        {
            var response = await _httpClient.PutAsJsonAsync("User/UpdatePasswordAttempt", passwordAttemptRequest, ct);

            if (response.IsSuccessStatusCode)
            {
                return (await response.Content.ReadFromJsonAsync<PasswordAttempt>(cancellationToken: ct))!;
            }

            var body = await response.Content.ReadFromJsonAsync<string>(ct);

            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                throw new ApiValidationException(body ?? "Bad Request");
            }

            _logger.LogError("Updating password attempt failed. Status: {StatusCode}, Body: {Body}", response.StatusCode, body);

            throw new ApiServerException(response.StatusCode, body ?? "Bad Request");
        }
    }
}