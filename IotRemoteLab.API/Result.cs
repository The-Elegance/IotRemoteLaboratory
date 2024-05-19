namespace IotRemoteLab.API;

public struct Result<T>
{
    public Result(string error, T value = default(T))
    {
        Error = error;
        Value = value;
    }
    public Result(string error, int statusCode, T value)
    {
        Error = error;
        Value = value;
        statusCode = statusCode;
    }
    public string Error { get; }
    
    public int? StatusCode { get; set; } 
    
    public T Value { get; }
    public T GetValueOrThrow()
    {
        if (IsSuccess) return Value;
        throw new InvalidOperationException($"No value. Only Error {Error}");
    }
    public bool IsSuccess => Error == null;


    public static implicit operator Result<T>(T value)
    {
        return Result.Ok(value);
    }
}

public static class Result
{
    public static Result<T> AsResult<T>(this T value)
    {
        return Ok(value);
    }

    public static Result<T> Ok<T>(T value)
    {
        return new Result<T>(null, value);
    }

    public static Result<T> Fail<T>(string e, int? status = null)
    {
        return new Result<T>(e);
    }

    public static Result<T> Of<T>(Func<T> f, string error = null)
    {
        try
        {
            return Ok(f());
        }
        catch (Exception e)
        {
            return Fail<T>(error ?? e.Message);
        }
    }

    public static Result<TOutput> Then<TInput, TOutput>(
        this Result<TInput> input,
        Func<TInput, TOutput> continuation)
    {
        return Of(() => continuation(input.GetValueOrThrow()));
        //return input.Then()
    }

    public static Result<TOutput> Then<TInput, TOutput>(
        this Result<TInput> input,
        Func<TInput, Result<TOutput>> continuation)
    {
        return input.IsSuccess
            ? continuation(input.GetValueOrThrow())
            : Fail<TOutput>(input.Error);
    }

    public static Result<TInput> OnFail<TInput>(
        this Result<TInput> input,
        Action<string> handleError)
    {
        if (!input.IsSuccess)
            handleError(input.Error);
        return input;
    }

    public static Result<TInput> ReplaceError<TInput>(
        this Result<TInput> input,
        Func<string, string> errorHandler)
    {
        return input.IsSuccess
            ? input
            : Of(() => input.GetValueOrThrow(), errorHandler(input.Error));
    }

    public static Result<TInput> RefineError<TInput>(
        this Result<TInput> input,
        string error)
    {
        return input.ReplaceError((err) => $"{error}. {input.Error}");
    }
}

