using AssinaturaDocumento.DataAcess;
using AutoMapper;
using CatalogoApi.Controllers;
using CatalogoApi.Dominio;
using CatalogoApi.Model.View;
using CatalogoApi.Tests.Mock;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace CatalogoApi.Tests
{
    public class ProdutoControllerTest
    {
        MockCatalogo mockCatalogo;

        /// <summary>
        /// Setup
        /// </summary>
        public ProdutoControllerTest()
        {
            mockCatalogo = new MockCatalogo()
                .MockListarProdutos(MockCatalogo.ProdutosTeste())
                .MockInserir(new ProdutoView())
                .MockAlterar(new ProdutoView());

        }

        [Fact]
        public void Listar_VerificaResultado_Sucess()
        {
            //Arrange
            var produtoController = new ProdutoController(null, mockCatalogo.Object);

            //Act
            var lista  = (List<ProdutoView>)produtoController.Listar();

            //Assert
            Assert.True(lista.Count > 0, "Lista deveria contem valores na coleção.");

        }

        [Fact]
        public void Listar_Inserir_Sucess()
        {
            //Arrange
            var produtoController = new ProdutoController(null, mockCatalogo.Object);

            //Act
            produtoController.Inserir(new ProdutoView());

        }

        [Fact]
        public void Listar_Alterar_Sucess()
        {
            //Arrange
            var produtoController = new ProdutoController(null, mockCatalogo.Object);

            //Act
            produtoController.Alterar(new ProdutoView());

        }
    }
}
