using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Api.Enums;
using Api.Model.Common;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.Controllers
{
    [Route("api/[controller]")]
    public class BaseController : Controller
    {
        public BaseController()
        {

        }
        public async Task<IActionResult> GetResult<T>(ResultModel<T> result, HttpStatusCode? code = null)
        {
            if (code == null)
            {
                switch (result.ResultStatus)
                {
                    case ResultStatusEnum.Success:
                        result.Code = (int)HttpStatusCode.OK;
                        result.Status = HttpStatusCode.OK.ToString();
                        return Ok(JsonConvert.SerializeObject(result));
                    case ResultStatusEnum.BadRequest:
                        result.Code = (int)HttpStatusCode.BadRequest;
                        result.Status = HttpStatusCode.BadRequest.ToString();
                        return BadRequest(JsonConvert.SerializeObject(result));
                    case ResultStatusEnum.ServerError:
                        result.Code = (int)HttpStatusCode.InternalServerError;
                        result.Status = HttpStatusCode.InternalServerError.ToString();
                        return StatusCode((int)HttpStatusCode.InternalServerError, JsonConvert.SerializeObject(result));
                    default:
                        result.Code = (int)HttpStatusCode.OK;
                        result.Status = HttpStatusCode.OK.ToString();
                        return Ok(JsonConvert.SerializeObject(result));
                }

            }
            else
            {
                result.Code = (int)code;
                result.Status = code.ToString();
                return StatusCode((int)code, JsonConvert.SerializeObject(result));
            }
        }
    }
}
