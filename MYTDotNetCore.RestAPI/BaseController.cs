﻿using Microsoft.AspNetCore.Mvc;
using MYTDotNetCore.Domain.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MYTDotNetCore.RestAPI;

[Route("api/[controller]")]
[ApiController]
public class BaseController : ControllerBase
{

    [HttpPost("execute-object")] 
    public IActionResult Execute(object model)
    {
        JObject jObj = JObject.Parse(JsonConvert.SerializeObject(model));
        if (jObj.ContainsKey("Response"))
        {
            BaseResponseModel baseResponseModel =
                JsonConvert.DeserializeObject<BaseResponseModel>(jObj["Response"]!.ToString())!;
            if (baseResponseModel.RespType == EnumRespType.pending)
                return StatusCode(201, model);
            if (baseResponseModel.RespType == EnumRespType.ValidationError)
                return BadRequest(model);
            if (baseResponseModel.RespType == EnumRespType.SystemError)
                return StatusCode(500, model);
            return Ok(model);
        }
        return StatusCode(500, "Internal Response Model Error. Please add BaseResponseModel to your ResponseModel");
    }
    
    
    [HttpPost("execute-generic")]
    public IActionResult Execute<T>( Result<T> model)
    {
        if (model.IsValidationError)
            return BadRequest(model);
        if (model.IsSystemError)
            return StatusCode(500, model);
        return Ok(model);
    }
}
