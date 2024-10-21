﻿using Microsoft.AspNetCore.Mvc;
using Observer.Constants;
using Observer.Domain.Interfaces;
using Observer.Presentation.Logs;
using Observer.Presentation.Models.Requests;
using Observer.Presentation.Models.Responses;
using SingleLog.Enums;
using SingleLog.Interfaces;
using SingleLog.Models;
using System.Net;

namespace Observer.Controllers
{
    /// <summary>
    /// User Controller.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserServices _userServices;
        private readonly ISingleLog<LogModel> _singleLog;

        /// <summary>
        /// General constructor.
        /// </summary>
        /// <param name="userServices">Service class based on IUserServices.</param>
        /// <param name="singleLog">Service class of log based on ISingleLog.</param>
        public UserController(IUserServices userServices, ISingleLog<LogModel> singleLog)
        {
            _userServices = userServices ?? throw new ArgumentNullException(nameof(userServices));
            _singleLog = singleLog ?? throw new ArgumentNullException(nameof(singleLog));
        }

        /// <summary>
        /// GET endpoint to retrieve some user data.
        /// </summary>
        /// <param name="userId">User identification.</param>
        /// <returns>Object UserResponse</returns>
        [HttpGet]
        [Route("/{userId}")]
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status201Created)]
        public async Task<ActionResult<UserResponse>> Details(int userId)
        {
            var baseLog = await _singleLog.CreateBaseLogAsync();
            baseLog.Request = new { userId };

            var sublog = new SubLog();
            await baseLog.AddStepAsync(LogSteps.GET_USER_BY_ID, sublog);

            sublog.StopwatchStart();

            try
            {
                if (userId <= 0)
                {
                    var responseError = UserResponseErrors.InvalidUserId;
                    baseLog.Response = responseError;
                    baseLog.Level = LogTypes.WARN;

                    return StatusCode((int)UserResponseErrors.InvalidUserId.ResponseCode, UserResponseErrors.InvalidUserId);
                }

                var response = await _userServices.RetrieveUser(userId);

                baseLog.Response = response;
                baseLog.Level = response.ResponseCode.Equals(HttpStatusCode.OK) ? LogTypes.INFO : LogTypes.WARN;

                return response;
            }
            catch (Exception ex)
            {
                sublog.Exception = ex;
                baseLog.Level = LogTypes.ERROR;

                var responseError = UserResponseErrors.InternalServerError;
                baseLog.Response = responseError;

                return StatusCode((int)responseError.ResponseCode, responseError);
            }
            finally
            {
                sublog.StopwatchStop();

                await _singleLog.WriteLogAsync(baseLog);
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
            var baseLog = await _singleLog.CreateBaseLogAsync();

            baseLog.Request = user;

            var sublog = new SubLog();
            await baseLog.AddStepAsync(LogSteps.CREATE_NEW_USER, sublog);

            sublog.StopwatchStart();

            try
            {
                var validation = user.IsValid();

                if (!validation.ResponseCode.Equals(HttpStatusCode.Continue))
                {
                    baseLog.Response = validation;
                    baseLog.Level = LogTypes.WARN;

                    return StatusCode((int)validation.ResponseCode, validation);
                }

                var response = await _userServices.CreateUser(user);

                baseLog.Response = response;
                baseLog.Level = response.ResponseCode.Equals(HttpStatusCode.Created) ? LogTypes.INFO : LogTypes.WARN;

                return response;
            }
            catch (Exception ex)
            {
                sublog.Exception = ex;
                baseLog.Level = LogTypes.ERROR;

                var responseError = UserResponseErrors.InternalServerError;
                baseLog.Response = responseError;

                return StatusCode((int)responseError.ResponseCode, responseError);
            }
            finally
            {
                sublog.StopwatchStop();

                await _singleLog.WriteLogAsync(baseLog);
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
            var baseLog = await _singleLog.CreateBaseLogAsync();
            baseLog.Request = new { userId, user };

            var sublog = new SubLog();
            await baseLog.AddStepAsync(LogSteps.EDIT_USER_BY_ID, sublog);

            sublog.StopwatchStart();

            try
            {
                if (userId <= 0)
                {
                    var responseError = UserResponseErrors.InvalidUserId;
                    baseLog.Response = responseError;
                    baseLog.Level = LogTypes.WARN;

                    return StatusCode((int)UserResponseErrors.InvalidUserId.ResponseCode, UserResponseErrors.InvalidUserId);
                }

                var validation = user.IsValid();

                if (!validation.ResponseCode.Equals(HttpStatusCode.Continue))
                {
                    baseLog.Response = validation;
                    baseLog.Level = LogTypes.WARN;

                    return StatusCode((int)validation.ResponseCode, validation);
                }

                var response = await _userServices.UpdateUser(userId, user);

                baseLog.Response = response;
                baseLog.Level = response.ResponseCode.Equals(HttpStatusCode.OK) ? LogTypes.INFO : LogTypes.WARN;

                return response;
            }
            catch (Exception ex)
            {
                sublog.Exception = ex;
                baseLog.Level = LogTypes.ERROR;

                var responseError = UserResponseErrors.InternalServerError;
                baseLog.Response = responseError;

                return StatusCode((int)responseError.ResponseCode, responseError);
            }
            finally
            {
                sublog.StopwatchStop();

                await _singleLog.WriteLogAsync(baseLog);
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
            var baseLog = await _singleLog.CreateBaseLogAsync();
            baseLog.Request = new { userId };

            var sublog = new SubLog();
            await baseLog.AddStepAsync(LogSteps.DELETE_USER_BY_ID, sublog);

            sublog.StopwatchStart();

            try
            {
                if (userId <= 0)
                {
                    var responseError = UserResponseErrors.InvalidUserId;
                    baseLog.Response = responseError;
                    baseLog.Level = LogTypes.WARN;

                    return StatusCode((int)UserResponseErrors.InvalidUserId.ResponseCode, UserResponseErrors.InvalidUserId);
                }

                var response = await _userServices.DeleteUser(userId);

                baseLog.Response = response;
                baseLog.Level = response.ResponseCode.Equals(HttpStatusCode.OK) ? LogTypes.INFO : LogTypes.WARN;

                return Ok(response);
            }
            catch (Exception ex)
            {
                sublog.Exception = ex;
                baseLog.Level = LogTypes.ERROR;

                var responseError = UserResponseErrors.InternalServerError;
                baseLog.Response = responseError;

                return StatusCode((int)responseError.ResponseCode, responseError);
            }
            finally
            {
                sublog.StopwatchStop();

                await _singleLog.WriteLogAsync(baseLog);
            }
        }
    }
}