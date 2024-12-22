using eVault.Domain.ResultWrapper;
using Microsoft.AspNetCore.Mvc;

public static class ResultExtensions
{
    public static IActionResult ToObjectResult<T>(this Result<T?> result)
    {
        return result.ResultType switch
        {
            ResultType.Success => new OkObjectResult(result),
            ResultType.BadRequest => new BadRequestObjectResult(result),
            ResultType.NotFound => new NotFoundObjectResult(result),
            ResultType.Failure => new BadRequestObjectResult(result),
            ResultType.Conflict => new ConflictObjectResult(result),
            ResultType.Unauthorized => new UnauthorizedObjectResult(result),
            ResultType.Forbidden => new ForbidResult(),
            _ => new StatusCodeResult(500),
        };
    }

    public static async Task<IActionResult> ToObjectResult<T>(this Task<Result<T?>> taskResult)
    {
        var result = await taskResult.ConfigureAwait(false);
        return result.ToObjectResult();
    }

    public static Result<U?> Map<T, U>(this Result<T?> result, Func<T?, U?> mapFunc)
    {
        switch (result.ResultType)
        {
            case ResultType.Success:
                return Result<U?>.Success(mapFunc(result.Data));
            case ResultType.BadRequest:
                return Result<U?>.BadRequest(result?.Error);
            case ResultType.NotFound:
                return Result<U?>.NotFound(result?.Error);
            case ResultType.Failure:
                return Result<U?>.Failure(result?.Error);
            case ResultType.Conflict:
                return Result<U?>.Conflict(result?.Error);
            case ResultType.Unauthorized:
                return Result<U?>.Unauthorized(result?.Error);
            case ResultType.Forbidden:
                return Result<U?>.Forbidden(result?.Error);
        }

        return Result<U?>.Failure(new Error("Internal server error", "CRITSYSF"));
    }

    public static async Task<Result<U?>> Map<T, U>(this Task<Result<T?>> taskResult, Func<T?, U?> mapFunc)
    {
        var result = await taskResult.ConfigureAwait(false);
        return result.Map(mapFunc);
    }
}
