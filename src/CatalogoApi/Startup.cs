using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.AzureAD.UI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System.Data;
using System.Data.SqlClient;
using AssinaturaDocumento.DataAcess;
using CatalogoApi.Dominio;
using CatalogoApi.Model.View;
using CatalogoApi.Model.Db;
using AutoMapper;
using Microsoft.Extensions.Logging.Console;

namespace CatalogoApi
{
    public class Startup
    {
        public const string AppSettingsDir = "APPSETTINGS_DIR";

        public IConfiguration Configuration { get; }
        //public static ILogger<ConsoleLoggerProvider> AppLogger = null;
        //public static ILoggerFactory loggerFactory = null;

        public Startup(IWebHostEnvironment env)
        {
            var appSettingsFolder = Environment.GetEnvironmentVariable(AppSettingsDir);

            if (string.IsNullOrWhiteSpace(appSettingsFolder))
                appSettingsFolder = ".";

            var builder = new ConfigurationBuilder()
                .AddJsonFile($"{appSettingsFolder}/appsettings.json", reloadOnChange: true, optional: true)
                .AddJsonFile($"{appSettingsFolder}/appsettings.{env.EnvironmentName}.json", reloadOnChange: true, optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
           

            //services.AddLogging(builder => builder
            //       .AddConsole()
            //       .AddFilter(level => level >= LogLevel.Trace)
            //    );
            //loggerFactory = services.BuildServiceProvider().GetService<ILoggerFactory>();
            //AppLogger = loggerFactory.CreateLogger<ConsoleLoggerProvider>();

            services.AddAuthentication(AzureADDefaults.BearerAuthenticationScheme)
                .AddAzureADBearer(options => Configuration.Bind("AzureAd", options));
            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Catalogo API", Version = "v1" });
            });

            services.AddAutoMapper(typeof(Catalogo));

            services.AddSingleton<IDbConnection>(db => new SqlConnection(Configuration["CatalogoApi:ConnectionStrings:Default"]));
            services.AddSingleton<ICatalogo, Catalogo>();
            services.AddSingleton<IProdutoRepository, ProdutoRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Catalogo API V1");
                c.RoutePrefix = "";
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
