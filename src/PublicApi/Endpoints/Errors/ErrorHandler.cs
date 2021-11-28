using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace PublicApi.Endpoints.Errors
{
    [AllowAnonymous]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorHandler : EndpointBaseAsync
        .WithoutRequest
        .WithResult<ErrorResult>
    {
        private readonly ILogger<ErrorHandler> _logger;

        public ErrorHandler(ILogger<ErrorHandler> logger)
        {
            _logger = logger;
        }

        [Route("error")]
        public override Task<ErrorResult> HandleAsync(CancellationToken cancellationToken = default)
        {
            var context = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            var exception = context.Error;
            var code = HttpStatusCode.InternalServerError;

            _logger.LogError(exception.Message);
            _logger.LogTrace(exception, exception.Message);

            var errorResult = new ErrorResult()
            {
                StatusCode = code,
                Message = exception.Message
            };

            return Task.FromResult(errorResult);
        }
    }
}
