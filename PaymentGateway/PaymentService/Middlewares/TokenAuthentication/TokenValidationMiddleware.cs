using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using PaymentService.Managers.Token;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace PaymentService.Middlewares.TokenAuthentication
{
    public class TokenValidationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IHostingEnvironment _env;

        private readonly ITokenManager _tokenManager;

        public TokenValidationMiddleware(RequestDelegate next, IHostingEnvironment env, ITokenManager tokenManager)
        {
            _next = next;
            _env = env;
            _tokenManager = tokenManager;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                var token = context.Request.Headers.Where(h => h.Key.Equals("Authorization")).FirstOrDefault().Value;

                var isValidToken = await _tokenManager.ValidateTokenAsync(token);

                if (!isValidToken)
                    throw new UnauthorizedAccessException();

                await _next.Invoke(context);
            }
            catch (UnauthorizedAccessException)
            {
                //TODO Logging
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            }
            catch (Exception)
            {
                //TODO Logging
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
        }
    }
}