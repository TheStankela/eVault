using eVault.Domain.Constants;
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
            ResultType.Failure => new ObjectResult(new {result}) { StatusCode = 500},
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
                return Result<U?>.BadRequest(result?.Error.Message, result?.Error.Code);
            case ResultType.NotFound:
                return Result<U?>.NotFound(result?.Error.Message, result?.Error.Code);
            case ResultType.Failure:
                return Result<U?>.Failure(result?.Error.Message, result?.Error.Code);
            case ResultType.Conflict:
                return Result<U?>.Conflict(result?.Error.Message, result?.Error.Code);
            case ResultType.Unauthorized:
                return Result<U?>.Unauthorized();
            case ResultType.Forbidden:
                return Result<U?>.Forbidden();
        }

        return Result<U?>.Failure(code: ApplicationResources.ResultSystemFailure);
    }

    public static async Task<Result<U?>> Map<T, U>(this Task<Result<T?>> taskResult, Func<T?, U?> mapFunc)
    {
        var result = await taskResult.ConfigureAwait(false);
        return result.Map(mapFunc);
    }
}
