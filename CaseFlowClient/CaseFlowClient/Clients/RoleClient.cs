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
    /// The RoleClient
    /// </summary>
    /// <seealso cref="IMotionSoftware.CaseFlowClient.Interfaces.IRoleClient" />
    public class RoleClient : IRoleClient
    {
        /// <summary>
        /// The HTTP client
        /// </summary>
        private readonly HttpClient _httpClient;

        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger<RoleClient> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="RoleClient"/> class.
        /// </summary>
        /// <param name="httpClient">The HTTP client.</param>
        /// <param name="logger">The logger.</param>
        public RoleClient(HttpClient httpClient, ILogger<RoleClient> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        /// <summary>
        /// Creates the caseworker role asynchronous.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="ct">The ct.</param>
        /// <returns>
        /// The <see cref="Task{TResult}" />
        /// </returns>
        /// <exception cref="IMotionSoftware.CaseFlowClient.Utilities.ApiValidationException"></exception>
        /// <exception cref="IMotionSoftware.CaseFlowClient.Utilities.ApiServerException"></exception>
        public async Task<NewRole> CreateCaseworkerRoleAsync(CreateRoleRequest request, CancellationToken ct = default)
        {
            var response = await _httpClient.PostAsJsonAsync("Role/CreateCaseworkerRoleAsync", request, ct);

            if (response.IsSuccessStatusCode)
            {
                return (await response.Content.ReadFromJsonAsync<NewRole>(cancellationToken: ct))!;
            }

            var body = await response.Content.ReadFromJsonAsync<string>(ct);

            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                throw new ApiValidationException(body ?? "Bad Request");
            }

            _logger.LogError("Role creation failed. Status: {StatusCode}, Body: {Body}", response.StatusCode, body);

            throw new ApiServerException(response.StatusCode, body ?? "Bad Request");
        }

        /// <summary>
        /// Gets all roles.
        /// </summary>
        /// <param name="ct">The ct.</param>
        /// <returns>
        /// The <see cref="Task{TResult}" />
        /// </returns>
        /// <exception cref="IMotionSoftware.CaseFlowClient.Utilities.ApiValidationException"></exception>
        /// <exception cref="IMotionSoftware.CaseFlowClient.Utilities.ApiServerException"></exception>
        public async Task<IEnumerable<CaseworkerRole>> GetAllRolesAsync(CancellationToken ct = default)
        {
            var response = await _httpClient.GetAsync("Role/GetAllCaseworkerRolesAsync", ct);
            if (response.IsSuccessStatusCode)
            {
                var roles = await response.Content
                    .ReadFromJsonAsync<IEnumerable<CaseworkerRole>>(cancellationToken: ct);

                return roles ?? Enumerable.Empty<CaseworkerRole>();
            }

            var body = await response.Content.ReadFromJsonAsync<string>(ct);

            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                throw new ApiValidationException(body ?? "Bad Request");
            }

            _logger.LogError(
                "Get all roles failed. Status: {StatusCode}, Body: {Body}",
                response.StatusCode, body);

            throw new ApiServerException(response.StatusCode, body ?? "Unexpected Error");
        }
    }
}