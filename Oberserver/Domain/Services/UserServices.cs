using Oberserver.Constants;
using Oberserver.Data.Entities;
using Oberserver.Data.Interfaces;
using Oberserver.Domain.Interfaces;
using Oberserver.Presentation.Models.Requests;
using Oberserver.Presentation.Models.Responses;
using System.Net;

namespace Oberserver.Domain.Services
{
    public class UserServices : IUserServices
    {
        private readonly IUserRepository _userRepository;

        public UserServices(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserResponse> CreateUser(UserRequest user)
        {
            Users userData = new Users(user);

            var result = await _userRepository.InsertUser(userData);

            if (result is not null && result?.Id <= 0)
                return UserResponseErrors.CreateUserError;

            return new UserResponse(HttpStatusCode.Created, $"Usuário {user.Name} criado com sucesso.", result!);
        }

        public async Task<UserResponse> RetrieveUser(int userId)
        {
            var result = await _userRepository.SelectUser(userId);

            if (result is null)
                return UserResponseErrors.UserNotFound;

            var user = new UsersEnvelope(result);

            return new UserResponse(HttpStatusCode.OK, $"Usuário recuperado com sucesso.", user);
        }

        public async Task<UserResponse> DeleteUser(int userId)
        {
            var result = await _userRepository.DeleteUser(userId);

            if (!result)
                return UserResponseErrors.UserNotFound;

            return new UserResponse(HttpStatusCode.OK, $"Usuário {userId} foi deletado com sucesso.");
        }

        public async Task<UserResponse> UpdateUser(int userId, UserRequest user)
        {
            Users userData = new Users(userId, user);

            var result = await _userRepository.UpdateUser(userData);

            if (!result)
                return UserResponseErrors.UserNotFound;

            return new UserResponse(HttpStatusCode.OK, $"Dados do usuário {user.Name} foram alterados com sucesso.");
        }
    }
}
