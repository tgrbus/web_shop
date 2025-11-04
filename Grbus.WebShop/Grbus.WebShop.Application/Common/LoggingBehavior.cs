using Grbus.WebShop.Domain.Common;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Grbus.WebShop.Application.Common
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
    {
        private readonly Stopwatch _timer = new();
        private readonly ILogger<TRequest> _logger;

        public LoggingBehavior(ILogger<TRequest> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            _timer.Start();
            var response = await next();
            _timer.Stop();

            var milliseconds = _timer.ElapsedMilliseconds; 
            var requestName = typeof(TRequest).Name;

            if(response is Result result)
            {
                if (result.IsSuccess)
                {
                    _logger.LogInformation("Request {requestName} handled in {milliseconds} ms. Request body: {request}, response: {response}",
                    requestName, milliseconds, request, response);
                } else
                {
                    _logger.LogError("Error for Request {requestName}, duration {milliseconds} ms. Request body: {request}, response: {response}",
                    requestName, milliseconds, request, response);
                }
            }           

            return response;            
        }
    }
}
