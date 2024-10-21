using Observer.Presentation.Models.Responses;
using System.Net;

namespace Observer.Constants
{
    public static class UserResponseErrors
    {
        internal static UserResponse UserValidationErrorMessage(string customMessage) => new UserResponse(HttpStatusCode.BadRequest, customMessage);

        internal static readonly UserResponse CreateUserError = new UserResponse(HttpStatusCode.InternalServerError, "Ocorreu um erro durante a criação do usuário.");
        internal static readonly UserResponse UserNotFound = new UserResponse(HttpStatusCode.NotFound, "Nenhum usuário encontrado. O identificador informado não resultou em dados nesta ação.");
        internal static readonly UserResponse InternalServerError = new UserResponse(HttpStatusCode.InternalServerError, "Ocorreu um erro durante a execução da requisição.");
        internal static readonly UserResponse InvalidUserId = new UserResponse(HttpStatusCode.BadRequest, "Informe um 'userId' válido para a requisição.");
    }
}
