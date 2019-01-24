using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NetPeakTestAssigment.Models;
using NetPeakTestAssigment.Models.HttpClients;
using NetPeakTestAssigment.Models.Parser;
using NetPeakTestAssigment.Sockets;

namespace NetPeakTestAssigment
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            //origins for development while running spa on different server
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAnyOrigin",
                    builder => builder.AllowAnyHeader()
                                      .AllowAnyMethod()
                                      .AllowAnyOrigin());
            });
            //Registering IHttpClientFactory, 
            //HttpClient is intended to be instantiated once and reused throughout the life of an application
            //Details:
            //https://docs.microsoft.com/en-us/dotnet/standard/microservices-architecture/implement-resilient-applications/use-httpclientfactory-to-implement-resilient-http-requests
            services.AddHttpClient();
            //Parser is created each time when it requested
            services.AddTransient<IHtmlParser, HtmlAgilityPackParser>();
            //'HttpClient' is created only once.
            services.AddSingleton<IHttpClient, HttpClientAdapter>();
            //History of parsing requests
            services.AddSingleton<IParsedSitesRepository, ParsedSiteRepository>();

            services.AddSingleton<NotificationSocketManager>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseCors("AllowAnyOrigin");
            }
            else
            {
                app.UseHsts();
            }

            app.UseWebSockets();
            //app.UseHttpsRedirection();

            app.UseMiddleware<NotificationSocketMiddleware>();
            //using static files for SPA, make access to it from '/'
            app.UseStaticFiles();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action}/{id?}",
                    defaults: new { controller = "Home", action = "Index" });
            });

        }
    }
}
