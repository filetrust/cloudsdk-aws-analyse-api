﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Glasswall.CloudSdk.AWS.Common.Web
{
    public abstract class AwsCommonStartup
    {
        public static IConfiguration Configuration { get; private set; }

        protected AwsCommonStartup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public virtual void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddLogging();

            ConfigureAdditionalServices(services);
        }

        public virtual void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.Use((context, next) =>
            {
                context.Response.Headers["Access-Control-Allow-Origin"] = "*";
                return next.Invoke();
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            ConfigureAdditional(app, env);
        }

        protected abstract void ConfigureAdditionalServices(IServiceCollection services);
        protected abstract void ConfigureAdditional(IApplicationBuilder app, IWebHostEnvironment env);
    }
}
