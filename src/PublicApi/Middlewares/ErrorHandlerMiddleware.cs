using ApplicationCore.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace PublicApi.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlerMiddleware> _logger;
        private readonly Dictionary<Type, int> _httpStatusCodes = new()
        {
            { typeof(AppException), (int)HttpStatusCode.BadRequest },
            { typeof(KeyNotFoundException), (int)HttpStatusCode.NotFound },
        };


        public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
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

                response.StatusCode = _httpStatusCodes.TryGetValue(error.GetType(), out int statusCode) ?
                    statusCode : (int)HttpStatusCode.InternalServerError;

                _logger.LogTrace(error, error.Message);

                var result = JsonConvert.SerializeObject(new { message = error?.Message });
                await response.WriteAsync(result);
            }
        }
    }
}
