using AutoMapper;
using CatalogoApi.Controllers;
using CatalogoApi.Dominio;
using CatalogoApi.Dominio.Mapper;
using CatalogoApi.Model.Db;
using CatalogoApi.Model.View;
using CatalogoApi.Tests.Mock;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Xunit;

namespace CatalogoApi.Tests
{
    public class ProdutoControllerTest
    {
        private readonly MockProdutoRepository mockProdutoRepository;
        private readonly IMapper mapper;
        private readonly ICatalogo catalogo;
        private readonly ProdutoController controller;

        /// <summary>
        /// Setup
        /// </summary>
        public ProdutoControllerTest()
        {
            var produto = new Produto() { Id = Guid.NewGuid(), Nome = "Nome 1", Preco = 100 };
            var lista = new List<Produto>();
            lista.Add(produto);

            mockProdutoRepository = new MockProdutoRepository()
                .MockListar(lista);

            //auto mapper configuration
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapping());
            });
            mapper = mockMapper.CreateMapper();
            catalogo = new Catalogo(mockProdutoRepository.Object, mapper);            
            controller = new ProdutoController(null, catalogo);
        }

        [Fact]
        public void Listar_VerificaResultado_Sucess()
        {
            //Act
            var lista  = (List<ProdutoView>)controller.Listar();

            //Assert
            Assert.True(lista.Count > 0, "Lista deveria conter valores na cole��o.");

        }

        [Fact]
        public void Inserir_Sucess()
        {
            //Arrange            
            var prod = new ProdutoView() { Nome = "Nome 1", Preco = 100 };

            //Act
            var result = controller.Inserir(prod);

            Assert.True(result is OkResult,  "Request deveria ser Sucesso");

        }

        [Fact]
        public void Inserir_NomeNaoInformado_Sucess()
        {
            //Arrange
            var prod = new ProdutoView() { Nome = string.Empty, Preco = 100 };

            //Act
            var result = controller.Inserir(prod);

            Assert.True(result is BadRequestObjectResult, "Request Result deveria ser BadRequest");

        }

        [Fact]
        public void Alterar_Sucess()
        {
            //Arrange
            var prod = new ProdutoView() {Id = Guid.NewGuid(),  Nome = "Nome xxx", Preco = 100 };

            //Act
            var result =  controller.Alterar(prod);
            Assert.True(result is OkResult, "Request Result deveria ser Sucesso");
        }

        [Fact]
        public void Alterar_IdNaoInformado_Sucess()
        {
            //Arrange - Prepara request com id vazio
            var prod = new ProdutoView() { Id = Guid.Empty, Nome = "xxx", Preco = 100 };

            //Act
            var result = controller.Alterar(prod);
            Assert.True(result is BadRequestObjectResult, "Request deveria ser BadResquest");
        }
    }
}
