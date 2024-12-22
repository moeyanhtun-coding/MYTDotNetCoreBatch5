namespace MYTDotNetCore.Domain.Model;

public class Result<T>
{
    public bool IsSuccess { get; set; }

    public bool IsError
    {
        get { return !IsSuccess; }
    }

    public EnumRespType Type { get; set; }
    public T Data { get; set; }

    public static Result<T> Success<T>(T data, string message = "Success")
    {
        return new Result<T>
        {
            IsSuccess = true,
            Type = EnumRespType.Success,
            Data = data,
        };
    }
    

}