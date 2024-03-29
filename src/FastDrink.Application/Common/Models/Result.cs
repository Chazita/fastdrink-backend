﻿namespace FastDrink.Application.Common.Models;

public class Result
{
    public Result(bool succeeded, IDictionary<string, string> errors)
    {
        Succeeded = succeeded;
        Errors = errors;
    }

    public bool Succeeded { get; set; }
    public IDictionary<string, string> Errors { get; set; }

    public static Result Success()
    {
        return new Result(true, new Dictionary<string, string>());
    }

    public static Result Failure(IDictionary<string, string> errors)
    {
        return new Result(false, errors);
    }
}
