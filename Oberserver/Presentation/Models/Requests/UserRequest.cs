using Oberserver.Presentation.Models.Responses;
using System.Net;
using System.Text.RegularExpressions;

namespace Oberserver.Presentation.Models.Requests
{
    /// <summary>
    /// Request record for use on UserController.
    /// </summary>
    public record UserRequest
    {
        public UserRequest(string Name, string LastName, DateTime Birthdate, string Document, string Login, string Password)
        {
            this.Name = Name;
            this.LastName = LastName;
            this.Birthdate = Birthdate;
            this.Document = Document;
            this.Login = Login;
            this.Password = Password;
        }

        public string Name { get; }
        public string LastName { get; }
        public DateTime Birthdate { get; }
        public string Document { get; }
        public string Login { get; }
        public string Password { get; }

        /// <summary>
        /// Validate User data and return a object record for return to requester.
        /// </summary>
        /// <returns>Record UserResponse.</returns>
        public UserResponse IsValid()
        {
            if (Name is null || Name.Equals(string.Empty))
                return new UserResponse(HttpStatusCode.BadRequest, "Informe um nome válido para o usuário.");
            
            if (LastName is null || LastName.Equals(string.Empty))
                return new UserResponse(HttpStatusCode.BadRequest, "Informe um sobrenome válido para o usuário.");

            if (Birthdate > DateTime.Now.AddYears(-18) || Birthdate < DateTime.Now.AddYears(-100))
                return new UserResponse(HttpStatusCode.BadRequest, "Informe uma data de nascimento válida para o usuário.");

            if (string.IsNullOrEmpty(Login) || Login.Length < 4)
                return new UserResponse(HttpStatusCode.BadRequest, "Login precisa conter ao menos 5 dígitos para o usuário.");

            if (string.IsNullOrEmpty(Login) || Login.Length < 4)
                return new UserResponse(HttpStatusCode.BadRequest, "Login precisa conter ao menos 5 dígitos para o usuário.");

            var regex = new Regex("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$");
            
            if (string.IsNullOrEmpty(Password) || Password.Length < 4 && (regex.IsMatch(Password)))
                return new UserResponse(HttpStatusCode.BadRequest, "Login precisa conter ao menos 5 dígitos para o usuário.");

            return new UserResponse(HttpStatusCode.Continue);
        }
    }
}
