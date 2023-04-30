
namespace Shorten.io.Application.Common;

public class Result
{
    public Result(bool isSuccess, string? message)
    {
        IsSuccess = isSuccess;
        Message = message;
    }
    public bool IsSuccess { get; init; }
    public string? Message { get; init; }
    public bool IsFailure => !IsSuccess;


    public static Result Success() => new(true, null);
    public static Result<TValue> Success<TValue>(TValue value) => new(value, true, null);
    public static Result Failure(string message) => new(false, message);
    public static Result<TValue> Failure<TValue>(string message) => new(default!, false, message);
}

public class Result<TValue> : Result
{
    private readonly TValue _value;

    protected internal Result(TValue value, bool isSuccess, string? message)
            : base(isSuccess, message)
            => _value = value;


    public TValue Value() => _value;


}
