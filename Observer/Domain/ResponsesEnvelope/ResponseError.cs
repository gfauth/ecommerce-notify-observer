﻿namespace Observer.Domain.ResponsesEnvelope
{
    internal class ResponseError<T> : IResponse<T>
    {
        public bool IsSuccess { get; } = false;

        public T Data { get; } = default!;

        public string Details { get; } = default!;

        public ResponseError(T data, string details)
        {
            Data = data;
            Details = details;
        }

        public ResponseError(T data)
        {
            Data = data;
        }
    }
}
