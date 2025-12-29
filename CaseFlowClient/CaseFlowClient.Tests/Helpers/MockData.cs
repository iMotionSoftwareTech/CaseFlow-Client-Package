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

        /// <summary>
        /// Gets the create user request.
        /// </summary>
        /// <returns>The <see cref="CreateUserRequest"/></returns>
        public static CreateUserRequest GetCreateUserRequest()
        {
            return new CreateUserRequest
            {
                Forename = "John",
                Surname = "Doe",
                Email = "john.doe@test.com",
                CaseworkerRoleId = 1,
                PasswordHash = "hashedpassword",
                PasswordSalt = "salt",
                CreatedDateTime = DateTime.Now
            };
        }

        /// <summary>
        /// Gets the new user.
        /// </summary>
        /// <returns>The <see cref="NewUser"/></returns>
        public static NewUser GetNewUser()
        {
            return new NewUser
            {
                IsSuccess = true,
                CaseworkerId = 1
            };
        }

        /// <summary>
        /// Gets the user detail.
        /// </summary>
        /// <returns>The <see cref="UserDetail"/></returns>
        public static UserDetail GetUserDetail()
        {
            return new UserDetail
            {
                CaseworkerId = 1,
                Forename = "John",
                Surname = "Doe",
                Email = "",
                Role = "Caseworker",
                CaseworkerRoleId = 1,
                IsLocked = false,
                PasswordAttempt = 0,
                Username = "JDoe"
            };
        }

        /// <summary>
        /// Gets the password attempt request.
        /// </summary>
        /// <returns>The <see cref="PasswordAttemptRequest"/></returns>
        public static PasswordAttemptRequest GetPasswordAttemptRequest()
        {
            return new PasswordAttemptRequest
            {
                CaseworkerId = 1,
                MaxAttempts = 5
            };
        }

        /// <summary>
        /// Gets the password attempt.
        /// </summary>
        /// <returns>The <see cref="PasswordAttempt"/></returns>
        public static PasswordAttempt GetPasswordAttempt()
        {
            return new PasswordAttempt
            {
                IsSuccess = true,
                NewAttemptCount = 1,
                WasLocked = false
            };
        }
    }
}