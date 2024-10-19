using Microsoft.AspNetCore.Mvc;
using Oberserver.Constants;
using Oberserver.Domain.Interfaces;
using Oberserver.Presentation.Models.Requests;
using Oberserver.Presentation.Models.Responses;
using System.Net;

namespace Oberserver.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserServices _userServices;

        public UserController(IUserServices userServices)
        {
            _userServices = userServices;
        }

        /// <summary>
        /// GET endpoint to retrieve some user data.
        /// </summary>
        /// <param name="userId">User identification.</param>
        /// <returns>Object UserResponse</returns>
        [HttpGet]
        [Route("/{userId}")]
        [ProducesResponseType(typeof(UserResponse),StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(UserResponse),StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status201Created)]
        public async Task<ActionResult<UserResponse>> Details(int userId)
        {
            try
            {
                if (userId <= 0)
                {
                    var responseError = UserResponseErrors.InvalidUserId;

                    return StatusCode((int)UserResponseErrors.InvalidUserId.ResponseCode, UserResponseErrors.InvalidUserId);
                }

                var response = await _userServices.RetrieveUser(userId);

                return response;
            }
            catch(Exception ex)
            {
                var resposeError = UserResponseErrors.InternalServerError;

                return StatusCode((int)resposeError.ResponseCode, resposeError);
            }
        }

        /// <summary>
        /// POST endpoint to create a new user in the sistem.
        /// </summary>
        /// <param name="user">Object UserRequest with user data.</param>
        /// <returns>Object UserResponse</returns>
        [HttpPost]
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
        public async Task<ActionResult<UserResponse>> Create(UserRequest user)
        {
            try
            {
                var validation = user.IsValid();

                if (!validation.ResponseCode.Equals(HttpStatusCode.Continue))
                        return StatusCode((int)validation.ResponseCode, validation);

                var response = await _userServices.CreateUser(user);

                return response;
            }
            catch
            {
                var resposeError = UserResponseErrors.InternalServerError;

                return StatusCode((int)resposeError.ResponseCode, resposeError);
            }
        }

        /// <summary>
        /// PUT endpoint to edit data for a specific user.
        /// </summary>
        /// <param name="userId">User identification.</param>
        /// <param name="user">Object UserRequest with user data.</param>
        /// <returns>Object UserResponse</returns>
        [HttpPut]
        [Route("/{userId}")]
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
        public async Task<ActionResult<UserResponse>> Edit(int userId, UserRequest user)
        {
            try
            {
                if (userId <= 0)
                {
                    var responseError = UserResponseErrors.InvalidUserId;

                    return StatusCode((int)UserResponseErrors.InvalidUserId.ResponseCode, UserResponseErrors.InvalidUserId);
                }

                var validation = user.IsValid();

                if (!validation.ResponseCode.Equals(HttpStatusCode.Continue))
                    return StatusCode((int)validation.ResponseCode, validation);

                var response = await _userServices.UpdateUser(userId, user);

                return response;
            }
            catch
            {
                var resposeError = UserResponseErrors.InternalServerError;

                return StatusCode((int)resposeError.ResponseCode, resposeError);
            }
        }

        /// <summary>
        /// DELETE endpoint to delete some user into sistem.
        /// </summary>
        /// <param name="userId">User identification.</param>
        /// <returns>Object UserResponse</returns>
        [HttpDelete]
        [Route("/{userId}")]
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
        public async Task<ActionResult<UserResponse>> Delete(int userId)
        {
            try
            {
                if (userId <= 0)
                {
                    var responseError = UserResponseErrors.InvalidUserId;

                    return StatusCode((int)UserResponseErrors.InvalidUserId.ResponseCode, UserResponseErrors.InvalidUserId);
                }

                var response = await _userServices.DeleteUser(userId);

                return Ok(response);
            }
            catch
            {
                var resposeError = UserResponseErrors.InternalServerError;

                return StatusCode((int)resposeError.ResponseCode, resposeError);
            }
        }
    }
}