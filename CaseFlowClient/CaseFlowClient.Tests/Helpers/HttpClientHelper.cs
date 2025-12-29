using Moq;
using Moq.Protected;
using System.Net;
using System.Text;
using System.Text.Json;

namespace IMotionSoftware.CaseFlowClient.Tests.Helpers
{
    /// <summary>
    /// The HttpClientHelper
    /// </summary>
    public static class HttpClientHelper
    {
        /// <summary>
        /// Creates the HTTP client.
        /// </summary>
        /// <param name="statusCode">The status code.</param>
        /// <param name="responseBody">The response body.</param>
        /// <returns>The <see cref="HttpClient"/></returns>
        public static HttpClient CreateHttpClient(HttpStatusCode statusCode, object? responseBody = null)
        {
            var handler = new Mock<HttpMessageHandler>();

            handler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = statusCode,
                    Content = responseBody is null
                        ? new StringContent("")
                        : new StringContent(
                            JsonSerializer.Serialize(responseBody),
                            Encoding.UTF8,
                            "application/json")
                });

            return new HttpClient(handler.Object)
            {
                BaseAddress = new Uri("https://localhost/")
            };
        }

    }
}