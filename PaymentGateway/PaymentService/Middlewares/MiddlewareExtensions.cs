using Microsoft.AspNetCore.Builder;
using PaymentService.Middlewares.TokenAuthentication;

namespace PaymentService.Middlewares
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseTokenAuthentication(this IApplicationBuilder app)
        {
            return app.UseMiddleware<TokenValidationMiddleware>();
        }
    }
}