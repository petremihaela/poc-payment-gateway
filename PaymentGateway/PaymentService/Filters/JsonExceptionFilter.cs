using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using PaymentService.Models;

namespace PaymentService.Filters
{
    public class JsonExceptionFilter : IExceptionFilter
    {
        private readonly IHostingEnvironment _env;

        private readonly ILogger<JsonExceptionFilter> _logger;

        public JsonExceptionFilter(IHostingEnvironment env, ILogger<JsonExceptionFilter> logger)
        {
            _env = env;
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            var error = new PaymentSeviceError();

            _logger.LogError(context.Exception.ToString());

            if (_env.IsDevelopment())
            {
                error.Message = context.Exception.Message;
                error.Details = context.Exception.StackTrace;
            }
            else
            {
                error.Message = "A server error occured";
                error.Details = context.Exception.Message;
            }

            context.Result = new ObjectResult(error)
            {
                StatusCode = 500
            };
            
        }
    }
}