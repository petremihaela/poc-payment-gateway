using Microsoft.AspNetCore.Builder;
using PaymentService.Middleware.TokenAuthentication;

namespace PaymentService.Middleware
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseTokenAuthentication(this IApplicationBuilder app)
        {
            return app.UseMiddleware<TokenValidationMiddleware>();
        }
    }
}