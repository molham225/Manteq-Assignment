using Api.Controllers;
using Api.Model.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace Api.MiddleWares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var result = ResultModel<bool>.GetExceptionResult(new ResultErrorModel(error));
                result.Code = (int)HttpStatusCode.InternalServerError;
                result.Status = HttpStatusCode.InternalServerError.ToString(); 

                await response.WriteAsync(JsonSerializer.Serialize(result));
            }
        }
    }
}
