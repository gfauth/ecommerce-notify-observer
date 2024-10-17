using Microsoft.AspNetCore.Mvc;
using Oberserver.Data.Interfaces;
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

        [HttpGet]
        [Route("/{userId}")]
        public object Details(int userId)
        {
            return new { message = "Hello" };
        }

        [HttpPost]
        public async Task<UserResponse> Create(UserRequest user)
        {
            try
            {
                var validation = user.IsValid();

                if (validation.ResponseCode.Equals(HttpStatusCode.BadRequest))
                    return validation;

                var response = await _userServices.CreateUser(user);

                return response;
            }
            catch
            {
                return new UserResponse(HttpStatusCode.BadRequest, "Ocorreu um erro interno ao validar os dados informados.");
            }
        }

        [HttpPut]
        [Route("/{userId}")]
        public UserResponse Edit(int userId, UserRequest user)
        {
            try
            {
                var validation = user.IsValid();

                if (validation.ResponseCode.Equals(HttpStatusCode.BadRequest))
                    return validation;

                return new UserResponse(HttpStatusCode.Created, $"Usuário {user.Name} atualizado com sucesso.");
            }
            catch
            {
                return new UserResponse(HttpStatusCode.BadRequest, "Ocorreu um erro interno ao validar os dados informados.");
            }
        }

        [HttpDelete]
        [Route("/{userId}")]
        public object Delete(int userId, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return new { message = "Hello" };
            }
        }
    }
}
