using System.Net;

namespace Observer.Presentation.Models.Responses
{
    /// <summary>
    /// Response record for use on UserController.
    /// </summary>
    public record UserResponse
    {
        /// <summary>
        /// HttpStatusCode who represent the request response and error code.
        /// </summary>
        /// <example>201</example>
        public HttpStatusCode ResponseCode { get; }

        /// <summary>
        /// Message with details of erro or success.
        /// </summary>
        /// <example>something happened.</example>
        public string Details { get; } = string.Empty;

        /// <summary>
        /// Requested data will be return here.
        /// </summary>
        public UsersEnvelope? Data { get; }

        /// <summary>
        /// Constructor with 3 arguments.
        /// </summary>
        /// <param name="responseCode">HttpStatusCode value.</param>
        /// <param name="details">Text with details.</param>
        /// <param name="data">Object who be requested.</param>
        public UserResponse(HttpStatusCode responseCode, string details, UsersEnvelope data)
        {
            ResponseCode = responseCode;
            Details = details;
            Data = data;
        }

        /// <summary>
        /// Constructor with 2 arguments.
        /// </summary>
        /// <param name="responseCode">HttpStatusCode value.</param>
        /// <param name="details">Text with details.</param>
        public UserResponse(HttpStatusCode responseCode, string details)
        {
            ResponseCode = responseCode;
            Details = details;
        }

        /// <summary>
        /// Constructor with 1 arguments.
        /// </summary>
        /// <param name="responseCode">HttpStatusCode value.</param>
        public UserResponse(HttpStatusCode responseCode)
        {
            ResponseCode = responseCode;
        }
    }
}