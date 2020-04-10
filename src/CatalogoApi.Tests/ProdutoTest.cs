using CatalogoApi.Controllers;
using CatalogoApi.Dominio;
using CatalogoApi.Model.View;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using Xunit;

namespace CatalogoApi.Tests
{
    public class ProdutoTest
    {
        ICatalogo catalogo;

        public ProdutoTest()
        {
            catalogo = new Mock.CatalogoMock();
        }
        [Fact]
        public void Lista_Produto_Sucess()
        {
            //Act
            var produtoController = new ProdutoController(null, catalogo);

            var lista  = (List<ProdutoView>)produtoController.Listar();

            //Assert
            Assert.True(lista.Count > 0);

        }
    }
}
