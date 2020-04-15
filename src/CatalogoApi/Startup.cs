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
using Microsoft.Extensions.Configuration.AzureKeyVault;
using CatalogoApi.Util;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Azure.KeyVault;

namespace CatalogoApi
{
    public class Startup
    {
        public const string AppSettingsDir = "APPSETTINGS_DIR";

        public IConfiguration Configuration { get; }
        
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

            //Configuração de keyvault
            var useAzureIdm = Configuration["AzureKeyVault:UseAzureManagedServiceIdentity"];
            if (useAzureIdm.Equals("true"))
            {
                var azureServiceTokenProvider = new AzureServiceTokenProvider();
                var keyVaultClient = new KeyVaultClient(
                   new KeyVaultClient.AuthenticationCallback(
                      azureServiceTokenProvider.KeyVaultTokenCallback));
                builder.AddAzureKeyVault(
                   Configuration["AzureKeyVault:KeyVaultEndPoint"], keyVaultClient, new DefaultKeyVaultSecretManager());
            }
            else
            {
                builder.AddAzureKeyVault(
                  Configuration["AzureKeyVault:KeyVaultEndPoint"],
                  Configuration["AzureKeyVault:AppClientId"],
                  Configuration["AzureKeyVault:AppClientSecret"],
                  new DefaultKeyVaultSecretManager());
            }
            Configuration = builder.Build();
        }

        

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(AzureADDefaults.BearerAuthenticationScheme)
                .AddAzureADBearer(options => Configuration.Bind("AzureAd", options));
            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Catalogo API", Version = "v1" });
            });

            //Mapper e Banco
            services.AddAutoMapper(typeof(Catalogo));

            services.AddSingleton<IDbConnection>(db => new SqlConnection(ObterConnectionString()));
            
            //Domains
            services.AddSingleton<ICatalogo, Catalogo>();
            services.AddSingleton<ICategoria, CategoriaDomain>();
            
            //Repositories
            services.AddSingleton<ICategoriaRepository, CategoriaRepository>();
            services.AddSingleton<IProdutoRepository, ProdutoRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
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

        private string ObterConnectionString()
        {
            return $"Server=tcp:{Configuration[Constantes.DataBase_Address]};Initial Catalog={Configuration[Constantes.DataBase_Name]};Persist Security Info=False;User ID={Configuration[Constantes.DataBase_Login]};Password={Configuration[Constantes.DataBase_Password]};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        }
    }
}
