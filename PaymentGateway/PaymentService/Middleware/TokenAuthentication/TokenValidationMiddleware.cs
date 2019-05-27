using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using PaymentService.Managers.Token;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace PaymentService.Middleware.TokenAuthentication
{
    public class TokenValidationMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly ITokenManager _tokenManager;

        private readonly ILogger<TokenValidationMiddleware> _logger;

        public TokenValidationMiddleware(RequestDelegate next, ITokenManager tokenManager, ILogger<TokenValidationMiddleware> logger)
        {
            _next = next;
            _tokenManager = tokenManager;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                var token = context.Request.Headers.FirstOrDefault(h => h.Key.Equals("Authorization")).Value;

                _logger.LogInformation($"Request token: {token}");

                var isValidToken = await _tokenManager.ValidateTokenAsync(token);

                if (!isValidToken)
                    throw new UnauthorizedAccessException();

                await _next.Invoke(context);
            }
            catch (UnauthorizedAccessException ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                _logger.LogError(ex.ToString());
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                _logger.LogError(ex.ToString());
            }
        }
    }
}