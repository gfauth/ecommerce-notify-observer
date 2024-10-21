using Observer.Presentation.Models.Responses;
using Observer.Constants;
using Observer.Data.Entities;
using Observer.Data.Interfaces;
using Observer.Domain.Interfaces;
using Observer.Presentation.Logs;
using Observer.Presentation.Models.Requests;
using SingleLog.Interfaces;
using SingleLog.Models;
using System.Net;

namespace Observer.Domain.Services
{
    internal class UserServices : IUserServices
    {
        private readonly IUserRepository _userRepository;
        private readonly ISingleLog<LogModel> _singleLog;

        /// <summary>
        /// Users services constructor.
        /// </summary>
        /// <param name="userRepository">Service class of repository based on IUserRepository.</param>
        /// <param name="singleLog">Service class of log based on ISingleLog.</param>
        public UserServices(IUserRepository userRepository, ISingleLog<LogModel> singleLog)
        {
            _userRepository = userRepository;
            _singleLog = singleLog;
        }

        public async Task<UserResponse> CreateUser(UserRequest user)
        {
            var baseLog = await _singleLog.GetBaseLogAsync();
            var sublog = new SubLog();

            await baseLog.AddStepAsync(LogSteps.USER_SERVICE_CREATE_USER_PROCESSING, sublog);
            sublog.StopwatchStart();

            try
            {
                Users userData = new Users(user);

                var result = await _userRepository.InsertUser(userData);

                if (result is not null && result?.Id <= 0)
                    return UserResponseErrors.CreateUserError;

                return new UserResponse(HttpStatusCode.Created, $"Usuário {user.Name} criado com sucesso.", result!);
            }
            finally
            {
                sublog.StopwatchStop();
            }
        }

        public async Task<UserResponse> RetrieveUser(int userId)
        {
            var baseLog = await _singleLog.GetBaseLogAsync();
            var sublog = new SubLog();

            await baseLog.AddStepAsync(LogSteps.USER_SERVICE_RETRIEVE_USER_PROCESSING, sublog);
            sublog.StopwatchStart();

            try
            {
                var result = await _userRepository.SelectUser(userId);

                if (result is null)
                    return UserResponseErrors.UserNotFound;

                var user = new UsersEnvelope(result);

                return new UserResponse(HttpStatusCode.OK, "Usuário recuperado com sucesso.", user);
            }
            finally
            {
                sublog.StopwatchStop();
            }
        }

        public async Task<UserResponse> DeleteUser(int userId)
        {
            var baseLog = await _singleLog.GetBaseLogAsync();
            var sublog = new SubLog();

            await baseLog.AddStepAsync(LogSteps.USER_SERVICE_DELETE_USER_PROCESSING, sublog);
            sublog.StopwatchStart();

            try
            {
                var result = await _userRepository.DeleteUser(userId);

                if (!result)
                    return UserResponseErrors.UserNotFound;

                return new UserResponse(HttpStatusCode.OK, $"Usuário {userId} foi deletado com sucesso.");
            }
            finally
            {
                sublog.StopwatchStop();
            }
        }

        public async Task<UserResponse> UpdateUser(int userId, UserRequest user)
        {
            var baseLog = await _singleLog.GetBaseLogAsync();
            var sublog = new SubLog();

            await baseLog.AddStepAsync(LogSteps.USER_SERVICE_UPDATE_USER_PROCESSING, sublog);
            sublog.StopwatchStart();

            try
            {
                Users userData = new Users(userId, user);

                var result = await _userRepository.UpdateUser(userData);

                if (!result)
                    return UserResponseErrors.UserNotFound;

                return new UserResponse(HttpStatusCode.OK, $"Dados do usuário {user.Name} foram alterados com sucesso.", new UsersEnvelope(userData));
            }
            finally
            {
                sublog.StopwatchStop();
            }
        }
    }
}
