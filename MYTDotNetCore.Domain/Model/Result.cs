﻿using Azure.Core;

namespace MYTDotNetCore.Domain.Model;

public class Result<T>
{
    public bool IsSuccess { get; set; }

    public bool IsError
    {
        get { return !IsSuccess; }
    }

    public bool IsValidationError
    {
        get { return Type == EnumRespType.ValidationError; }
    }

    public bool IsSystemError
    {
        get { return Type == EnumRespType.SystemError; }
    }

    private EnumRespType Type { get; set; }
    public string Message { get; set; }
    public T Data { get; set; }

    public static Result<T> Success(T data, string message = "Success")
    {
        return new Result<T>
        {
            IsSuccess = true,
            Type = EnumRespType.Success,
            Data = data,
            Message = message
        };
    }

    public static Result<T> ValidationError(string message, T? data = default)
    {
        return new Result<T>
        {
            IsSuccess = false,
            Type = EnumRespType.ValidationError,
            Message = message,
            Data = data
        };
    }

    public static Result<T> SystemError(string message, T? data = default)
    {
        return new Result<T>
        {
            IsSuccess = false,
            Type = EnumRespType.SystemError,
            Message = message,
            Data = data
        };
    }
}