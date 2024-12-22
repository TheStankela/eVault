using System.Net;

namespace eVault.Domain.ResultWrapper
{
    public class Result<T>
    {
        private Result(T data, ResultType? resultType, HttpStatusCode statusCode)
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
        public ResultType? ResultType { get; }
        public HttpStatusCode? StatusCode { get; }
        public bool IsSuccess => Error == null;

        public static Result<T> Success(T value) => new Result<T>(value, ResultWrapper.ResultType.Success, HttpStatusCode.OK);
        public static Result<T> Failure(Error error) => new Result<T>(error, ResultWrapper.ResultType.Failure, HttpStatusCode.InternalServerError);
        public static Result<T> NotFound(Error error) => new Result<T>(error, ResultWrapper.ResultType.NotFound, HttpStatusCode.NotFound);
        public static Result<T> BadRequest(Error error) => new Result<T>(error, ResultWrapper.ResultType.BadRequest, HttpStatusCode.BadRequest);
        public static Result<T> Conflict(Error error) => new Result<T>(error, ResultWrapper.ResultType.Conflict, HttpStatusCode.Conflict);
        public static Result<T> Unauthorized(Error error) => new Result<T>(error, ResultWrapper.ResultType.Unauthorized, HttpStatusCode.Unauthorized);
        public static Result<T> Forbidden(Error error) => new Result<T>(error, ResultWrapper.ResultType.Forbidden, HttpStatusCode.Forbidden);
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
