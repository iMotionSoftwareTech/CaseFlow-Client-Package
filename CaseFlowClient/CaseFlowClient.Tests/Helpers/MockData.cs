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

        /// <summary>
        /// Gets the log status requests.
        /// </summary>
        /// <returns>The <see cref="IEnumerable{T}"/></returns>
        public static IEnumerable<LogStatusRequest> GetLogStatusRequests()
        {
            return new List<LogStatusRequest>
            {
                new LogStatusRequest
                {
                    CaseworkerId = 1,
                    TaskId = 101,
                    StatusId = 2,
                    Notes = "Started working on task 101",
                    LogDateTime = DateTime.Now
                },
                new LogStatusRequest
                {
                    CaseworkerId = 2,
                    TaskId = 102,
                    StatusId = 2,
                    Notes = "Started working on task 102",
                    LogDateTime = DateTime.Now
                },
                new LogStatusRequest
                {
                    CaseworkerId = 1,
                    TaskId = 103,
                    StatusId = 2,
                    Notes = "Started working on task 103",
                    LogDateTime = DateTime.Now
                }
            };
        }

        /// <summary>
        /// Gets the bulk task update.
        /// </summary>
        /// <returns>The <see cref="BulkTaskUpdate"/></returns>
        public static BulkTaskUpdate GetBulkTaskUpdate()
        {
            return new BulkTaskUpdate
            {
                IsSuccess = true,
                InsertedCount = 3
            };
        }

        /// <summary>
        /// Gets the task update.
        /// </summary>
        /// <returns>The <see cref="TaskUpdate"/></returns>
        public static TaskUpdate GetTaskUpdate()
        {
            return new TaskUpdate
            {
                IsSuccess = true,
                TaskStatusId = 3
            };
        }

        /// <summary>
        /// Gets the new task.
        /// </summary>
        /// <returns>The <see cref="NewTask"/></returns>
        public static NewTask GetNewTask()
        {
            return new NewTask()
            {
                IsSuccess = true,
                TaskId = 102
            };
        }

        /// <summary>
        /// Gets the create task request.
        /// </summary>
        /// <returns>The <see cref="CreateTaskRequest"/></returns>
        public static CreateTaskRequest GetCreateTaskRequest()
        {
            return new CreateTaskRequest()
            {
                Title = "Review Document",
                Description = "Review the submitted document for accuracy",
                CaseworkerId = 4,
                DueDateTime = DateTime.Now.AddMonths(3)
            };
        }

        /// <summary>
        /// Gets the task record.
        /// </summary>
        /// <returns>The <see cref="CreateTaskRequest"/></returns>
        public static TaskRecord GetTaskRecord() 
        {
            return new TaskRecord()
            {
                Tasks = new List<CaseTask>
                {
                    new CaseTask
                    {
                        TaskId = 101,
                        Title = "Review Document",
                        Description = "Review the submitted document for accuracy",
                        Status = "Pending",
                        DueDateTime = DateTime.Now.AddMonths(1),
                    },
                    new CaseTask
                    {
                        TaskId = 102,
                        Title = "Approve Application",
                        Description = "Approve the application after review",
                        Status = "Awaiting Hearing",
                        DueDateTime = DateTime.Now.AddMonths(2),
                    }
                },
                TotalNoOfRecords = 2,
            };
        }

        /// <summary>
        /// Gets the statuses.
        /// </summary>
        /// <returns>The <see cref="IEnumerable{T}"/></returns>
        public static IEnumerable<Status> GetStatuses()
        {
            return new List<Status>
            {
                new Status { Id = 1, Title = "Pending" },
                new Status { Id = 2, Title = "In Progress" },
                new Status { Id = 3, Title = "Completed" },
                new Status { Id = 4, Title = "Awaiting Hearing" }
            };
        }

        /// <summary>
        /// Gets the task statuses.
        /// </summary>
        /// <returns>The <see cref="IEnumerable{T}"/></returns>
        public static IEnumerable<CaseTaskStatus> GetTaskStatuses()
        {
            return new List<CaseTaskStatus>
            {
                new CaseTaskStatus { Id = 1, CaseWorker = "John Doe", TaskId = 101, StatusId = 1, 
                    Status = "Pending", LogDateTime = DateTime.Today, Notes = "Test Notes" },
                new CaseTaskStatus { Id = 2, CaseWorker = "Jane Doe", TaskId = 102, StatusId = 4, 
                    Status = "Awaiting Hearing", LogDateTime = DateTime.Today, Notes = "Test Notes 2"  }
            };
        }
    }
}