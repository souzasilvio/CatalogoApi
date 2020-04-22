using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace CatalogoApi.Integration.Tests
{
    public class CatalogoApiTest
    {
        public HttpClient Client { get; set; }
        private IConfigurationRoot Config; 
        

        public CatalogoApiTest()
        {
            SetupClient();
            SetupConfig();
        }

        private string ObterToken()
        {
            string clientId = Config["ClientId"];
            string appKey = Config["ClientSecret"];
            string tenantId = Config["TenantId"];
            string apiurl = Config["Apiurl"];

            var clientCredential = new ClientCredential(clientId, appKey);
            var authenticationContext = new AuthenticationContext($"https://login.microsoftonline.com/{tenantId}");
            var result = authenticationContext.AcquireTokenAsync(apiurl, clientCredential).GetAwaiter().GetResult();
            return result.AccessToken;
        }

        private void SetupClient()
        {
            var hostBuilder = new HostBuilder()
            .ConfigureWebHost(webHost =>
            {
                // Add TestServer
                webHost.UseTestServer();
                webHost.UseStartup<CatalogoApi.Startup>();
            });

            var host = hostBuilder.StartAsync().GetAwaiter().GetResult();
            
            Client = host.GetTestClient();            
        }

        private void SetupConfig()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                  .AddJsonFile("appTest.json", optional: false, reloadOnChange: true)
                  .AddEnvironmentVariables();
            Config = builder.Build();
        }

        [Fact]
        public async Task Produto_Listar_ReturnsOkResponse()
        {
            string token = ObterToken();
            Client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            var response = await Client.GetAsync("api/produto/listar");
            response.EnsureSuccessStatusCode();
            Assert.True(response.StatusCode == System.Net.HttpStatusCode.OK, "Response deveria ser Ok");
        }

        [Fact]
        public async Task Categoria_Listar_ReturnsOkResponse()
        {
            var response = await Client.GetAsync("api/categoria/listar");
            response.EnsureSuccessStatusCode();
            Assert.True(response.StatusCode == System.Net.HttpStatusCode.OK, "Response deveria ser Ok");
        }

    }
}
