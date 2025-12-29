using IMotionSoftware.CaseFlowManager.API.Models.Models;
using IMotionSoftware.CaseFlowManager.API.Models.Request;

namespace IMotionSoftware.CaseFlowClient.Tests.Helpers
{
    /// <summary>
    /// The MockData
    /// </summary>
    public static class MockData
    {
        /// <summary>
        /// Gets the create role request.
        /// </summary>
        /// <returns>The <see cref="CreateRoleRequest"/></returns>
        public static CreateRoleRequest GetCreateRoleRequest()
        {
            return new CreateRoleRequest
            {
                RoleName = "Caseworker",
                Description = "Caseworker Role"
            };
        }

        /// <summary>
        /// Gets the new role.
        /// </summary>
        /// <returns>The <see cref="NewRole"/></returns>
        public static NewRole GetNewRole()
        {
            return new NewRole { IsSuccess = true, RoleId = 42 };
        }

        /// <summary>
        /// Gets the caseworker roles.
        /// </summary>
        /// <returns>The <see cref="IEnumerable{T}"/></returns>
        public static IEnumerable<CaseworkerRole> GetCaseworkerRoles()
        {
            return new List<CaseworkerRole>
            {
                new CaseworkerRole { Id = 1, Name = "Caseworker", Description = "Caseworker Role" },
                new CaseworkerRole { Id = 2, Name = "Supervisor", Description = "Supervisor Role" }
            };
        }
    }
}