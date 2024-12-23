using System.Net;
using System.Text.Json.Serialization;
using eVault.Domain.Constants;

namespace eVault.Domain.ResultWrapper
{
    public class Result<T>
    {
        private Result(T data, ResultType resultType, HttpStatusCode statusCode)
        {
            Data = data;
            Error = null;
            ResultType = resultType;
            StatusCode = statusCode;
        }

        private Result(Error error, ResultType resultType, HttpStatusCode statusCode)
        {
            Error = error;
            Data = default;
            ResultType = resultType;
            StatusCode = statusCode;
        }

        public T? Data { get; }

        public Error? Error { get; }

        [JsonIgnore]
        public ResultType ResultType { get; }

        public HttpStatusCode StatusCode { get; }

        public bool IsSuccess => Error == null;

        public static Result<T> Success(T value) => new Result<T>(value, ResultType.Success, HttpStatusCode.OK);
        public static Result<T> Failure(string? message = null, string? code = null) => new Result<T>(new Error(message ?? ApplicationResources.FailureMessage, code ?? ApplicationResources.FailureCode), ResultType.Failure, HttpStatusCode.InternalServerError);
        public static Result<T> NotFound(string? message = null, string? code = null) => new Result<T>(new Error(message ?? ApplicationResources.NotFoundMessage, code ?? ApplicationResources.NotFoundCode), ResultType.NotFound, HttpStatusCode.NotFound);
        public static Result<T> BadRequest(string? message = null, string? code = null) => new Result<T>(new Error(message ?? ApplicationResources.BadRequestMessage, code ?? ApplicationResources.BadRequestCode), ResultType.BadRequest, HttpStatusCode.BadRequest);
        public static Result<T> Conflict(string? message = null, string? code = null) => new Result<T>(new Error(message ?? ApplicationResources.ConflictMessage, code ?? ApplicationResources.ConflictCode), ResultType.Conflict, HttpStatusCode.Conflict);
        public static Result<T> Unauthorized() => new Result<T>(new Error(ApplicationResources.UnauthorizedMessage, ApplicationResources.UnauthorizedCode), ResultType.Unauthorized, HttpStatusCode.Unauthorized);
        public static Result<T> Forbidden() => new Result<T>(new Error(ApplicationResources.ForbiddenMessage, ApplicationResources.ForbiddenCode), ResultType.Forbidden, HttpStatusCode.Forbidden);
    }

    public enum ResultType
    {
        NotSet = 0, 
        Success = 1,
        Failure = 2,
        NotFound = 3,
        BadRequest = 4,
        Conflict = 5,
        Unauthorized = 6,
        Forbidden = 7
    }
}
