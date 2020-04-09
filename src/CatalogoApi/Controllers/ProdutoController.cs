using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatalogoApi.Dominio;
using CatalogoApi.Model.View;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CatalogoApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ProdutoController : ControllerBase
    {

        private readonly ILogger<ProdutoController> _logger;
        private readonly ICatalogo Catalogo;

        public ProdutoController(ILogger<ProdutoController> logger, ICatalogo catalogo)
        {
            _logger = logger;
            Catalogo = catalogo;
        }

        [AllowAnonymous]
        [HttpGet("listar")]
        public IEnumerable<ProdutoView> Listar()
        {
            return Catalogo.ListarProdutos();
            //var lista = new List<ProdutoView>();
            //for (int i = 1; i <= 20; i++)
            //{
            //    lista.Add(new ProdutoView()
            //    {
            //        Id = Guid.NewGuid(),
            //        Nome = $"Produto {i} - date {DateTime.Now}"
                   
            //    });
            //}

            //return lista;
        }
    }
}
