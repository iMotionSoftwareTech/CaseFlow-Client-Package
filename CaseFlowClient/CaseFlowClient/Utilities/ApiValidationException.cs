using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace IMotionSoftware.CaseFlowClient.Utilities
{
    /// <summary>
    /// The ApiValidationException
    /// </summary>
    /// <seealso cref="System.Exception" />
    public sealed class ApiValidationException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApiValidationException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public ApiValidationException(string message) : base(message)
        {
        }
    }

    /// <summary>
    /// The ApiServerException
    /// </summary>
    /// <seealso cref="System.Exception" />
    public sealed class ApiServerException : Exception
    {
        /// <summary>
        /// Gets the status code.
        /// </summary>
        /// <value>
        /// The status code.
        /// </value>
        public HttpStatusCode StatusCode { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiServerException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="statusCode">The status code.</param>
        public ApiServerException(HttpStatusCode statusCode, string message)
            : base(message)
        {
            StatusCode = statusCode;
        }
    }
}