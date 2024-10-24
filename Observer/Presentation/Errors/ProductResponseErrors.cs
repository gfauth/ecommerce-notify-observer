﻿using Observer.Presentation.Models.Responses;
using System.Net;

namespace Observer.Presentation.Errors
{
    /// <summary>
    /// 
    /// </summary>
    public static class ProductResponseErrors
    {
        internal static ResponseEnvelope ProductValidationErrorMessage(string customMessage) => new ResponseEnvelope(HttpStatusCode.BadRequest, customMessage);

        internal static readonly ResponseEnvelope CreateUserError = new ResponseEnvelope(HttpStatusCode.InternalServerError, "Ocorreu um erro durante a criação do produto.");
        internal static readonly ResponseEnvelope UserNotFound = new ResponseEnvelope(HttpStatusCode.NotFound, "Nenhum produrto encontrado. O identificador informado não resultou em dados nesta ação.");
        internal static readonly ResponseEnvelope InternalServerError = new ResponseEnvelope(HttpStatusCode.InternalServerError, "Ocorreu um erro durante a execução da requisição.");
        internal static readonly ResponseEnvelope InvalidProductId = new ResponseEnvelope(HttpStatusCode.BadRequest, "Informe um 'productId' válido para a requisição.");
    }
}
