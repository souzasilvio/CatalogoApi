using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace CatalogoApi.Integration.Tests
{
    public class CatalogoApiTest
    {
        public HttpClient Client { get; set; }
        

        public CatalogoApiTest()
        {
            SetupClient();
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

        [Fact]
        public async Task Produto_Listar_ReturnsOkResponse()
        {
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
