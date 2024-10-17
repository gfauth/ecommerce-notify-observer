using System.Net;

namespace Oberserver.Presentation.Models.Responses
{
    /// <summary>
    /// Response record for use on UserController.
    /// </summary>
    public record UserResponse
    {
        public HttpStatusCode ResponseCode { get; }
        public string? Details { get; }

        public UserResponse(HttpStatusCode responseCode, string details)
        {
            ResponseCode = responseCode;
            Details = details;
        }

        public UserResponse(HttpStatusCode responseCode)
        {
            ResponseCode = responseCode;
        }
    }
}