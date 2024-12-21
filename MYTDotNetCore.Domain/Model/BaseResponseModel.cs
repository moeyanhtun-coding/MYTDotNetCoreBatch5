namespace MYTDotNetCore.Domain.Model;

public class BaseResponseModel
{
    public string RespCode { get; set; }
    public string RespDesc { get; set; }
    public EnumRespType RespType { get; set; }
    public bool IsSuccess { get; set; }

    public bool IsError
    {
        get { return !IsSuccess; }
    }

    public static BaseResponseModel Success(string respCode, string respDesc)
    {
        return new BaseResponseModel
        {
            RespCode = respCode,
            RespDesc = respDesc,
            IsSuccess = true,
            RespType = EnumRespType.Success
        };
    }

    public static BaseResponseModel ValidationError(string respCode, string respDesc)
    {
        return new BaseResponseModel
        {
            RespCode = respCode,
            RespDesc = respDesc,
            IsSuccess = false,
            RespType = EnumRespType.ValidationError
        };
    }

    public static BaseResponseModel SystemError(string respCode, string respDesc)
    {
        return new BaseResponseModel
        {
            RespCode = respCode,
            RespDesc = respDesc,
            IsSuccess = false,
            RespType = EnumRespType.SystemError
        };
    }
}

public enum EnumRespType
{
    None,
    Success,
    ValidationError,
    SystemError,
}