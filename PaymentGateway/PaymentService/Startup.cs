using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PaymentService.Core.Context;
using PaymentService.Core.Managers;
using PaymentService.Core.Repositories;
using PaymentService.Core.Services;
using PaymentService.Managers.Token;
using PaymentService.Middlewares;
using Serilog;
using System;

namespace PaymentService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(configuration).CreateLogger();
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            //In memory - current poc's scope
            services.AddDbContext<PaymentServiceDbContext>(options => options.UseInMemoryDatabase("PaymentServiceDb"));

            services.AddTransient<ITokenManager, TokenManager>();
            services.AddTransient<IPaymentRepository, PaymentRepository>();
            services.AddTransient<IPaymentManager, PaymentManager>();

            services.AddHttpClient("tokensProvider", c =>
            {
                c.BaseAddress = new Uri(Configuration["TokensProvider"]);
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseTokenAuthentication();

            loggerFactory.AddSerilog();
            app.UseMvc();
        }
    }
}