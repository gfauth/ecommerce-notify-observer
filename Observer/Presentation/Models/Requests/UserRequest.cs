﻿using Observer.Constants;
using Observer.Presentation.Models.Responses;
using System.Net;
using System.Text.RegularExpressions;

namespace Observer.Presentation.Models.Requests
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

        /// <summary>
        /// User first name.
        /// </summary>
        /// <example>Jhon</example>
        public string Name { get; }

        /// <summary>
        /// User last name.
        /// </summary>
        /// <example>Blevers</example>
        public string LastName { get; }

        /// <summary>
        /// User birthdate.
        /// </summary>
        /// <example>2000-05-12</example>
        public DateTime Birthdate { get; }

        /// <summary>
        /// User identification document.
        /// </summary>
        /// <example>12345678912</example>
        public string Document { get; }

        /// <summary>
        /// User login.
        /// </summary>
        /// <example>mycustomlogin</example>
        public string Login { get; }

        /// <summary>
        /// User password.
        /// </summary>
        /// <example>123!@Best</example>
        public string Password { get; }

        /// <summary>
        /// Validate User data and return a object record for return to requester.
        /// </summary>
        /// <returns>Record UserResponse.</returns>
        public UserResponse IsValid()
        {
            if (Name is null || Name.Equals(string.Empty) || Name.Length <= 2)
                return UserResponseErrors.UserValidationErrorMessage("Informe um nome válido para o usuário.");

            if (LastName is null || LastName.Equals(string.Empty) || LastName.Length <= 2)
                return UserResponseErrors.UserValidationErrorMessage("Informe um sobrenome válido para o usuário.");

            if (Birthdate > DateTime.Now.AddYears(-18) || Birthdate < DateTime.Now.AddYears(-100))
                return UserResponseErrors.UserValidationErrorMessage("Informe uma data de nascimento válida para o usuário. Apenas maiores de 18 anos.");

            if (string.IsNullOrEmpty(Login) || Login.Length < 4)
                return UserResponseErrors.UserValidationErrorMessage("Login precisa conter ao menos 5 dígitos para o usuário.");

            var regex = new Regex("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$");

            if (string.IsNullOrEmpty(Password) || Password.Length < 8 && !regex.IsMatch(Password))
                return UserResponseErrors.UserValidationErrorMessage("Password precisa conter ao menos 8 dígitos para o usuário.");

            return new UserResponse(HttpStatusCode.Continue);
        }
    }
}
