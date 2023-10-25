using System.Net;

namespace OurFuss.Api.Infrastructure.Models.ApiResponse;

public class Result
{
    public object? Data { get { return null; } }

    public IEnumerable<string> Errors { get; set; }

    public bool Success { get; set; }

    public Result() : this(Enumerable.Empty<string>()) { }
    public Result(string error) : this(new List<string> { error }) { }

    public Result(IEnumerable<string> errors)
    {
        Errors = errors;
        Success = errors.Any() == false;
    }
}

public class Result<T> : Result
{
    public new T? Data { get; set; }

    public Result(string error) : base(error) { }
    public Result(IEnumerable<string> errors) : base(errors) { }
    public Result(T data) : this(data, Enumerable.Empty<string>()) { }
    public Result(T data, string error) : this(data, new List<string> { error }) { }

    public Result(T data, IEnumerable<string> errors) : base(errors)
    {
        Data = data;
    }
}