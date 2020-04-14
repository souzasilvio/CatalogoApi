using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatalogoApi.Dominio;
using CatalogoApi.Model.View;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CatalogoApi.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly ILogger<CategoriaController> _logger;
        private readonly ICategoria dominio;

        public CategoriaController(ILogger<CategoriaController> logger, ICategoria categoria)
        {
            _logger = logger;
            dominio = categoria;
        }

        [AllowAnonymous]
        [HttpGet("listar")]
        public IEnumerable<CategoriaView> Listar()
        {
            return dominio.Listar();
        }

        [AllowAnonymous]
        [HttpPost("inserir")]
        public IActionResult Inserir([FromBody]CategoriaView registro)
        {
            if (string.IsNullOrEmpty(registro.Nome))
            {
                return BadRequest("Nome deve ser informado");
            }
            try
            {
                dominio.Inserir(registro);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost("alterar")]
        public IActionResult Alterar([FromBody]CategoriaView registro)
        {
            if (registro.Id == 0)
            {
                return BadRequest("Id deve ser informado");
            }
            try
            {
                dominio.Alterar(registro);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}