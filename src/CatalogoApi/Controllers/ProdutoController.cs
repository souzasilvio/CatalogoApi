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
        }

        [AllowAnonymous]
        [HttpPost("inserir")]
        public IActionResult Inserir([FromBody]ProdutoView produto)
        {
            if (produto.Id == Guid.Empty || string.IsNullOrEmpty(produto.Nome) || produto.Preco <= 0)
            {
                return BadRequest("Id, Nome e Preço devem ser informados");
            }
            
            Catalogo.Inserir(produto);
            return Ok();
        }
    }
}
