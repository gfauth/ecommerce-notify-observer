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

            if (!result)
                return new UserResponse(HttpStatusCode.InternalServerError, "Ocorreu um erro durante a criação do usuário.");

            return new UserResponse(HttpStatusCode.Created, $"Usuário {user.Name} criado com sucesso.");
        }

        public Task<UserResponse> DeleteUser(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<UserResponse> RetrieveUser(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<UserResponse> UpdateUser(int userId, UserRequest user)
        {
            throw new NotImplementedException();
        }
    }
}
