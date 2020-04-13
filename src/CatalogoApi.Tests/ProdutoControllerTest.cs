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

        }

        [Fact]
        public void Listar_VerificaResultado_Sucess()
        {
            ICatalogo catalogo = new Catalogo(mockProdutoRepository.Object, mapper);
            //Arrange
            var produtoController = new ProdutoController(null, catalogo);

            //Act
            var lista  = (List<ProdutoView>)produtoController.Listar();

            //Assert
            Assert.True(lista.Count > 0, "Lista deveria conter valores na coleção.");

        }

        [Fact]
        public void Listar_Inserir_Sucess()
        {
            //Arrange
            ICatalogo catalogo = new Catalogo(mockProdutoRepository.Object, mapper);
            var produtoController = new ProdutoController(null, catalogo);
            var prod = new ProdutoView() { Nome = "Nome 1", Preco = 100 };

            //Act
            var result = produtoController.Inserir(prod);

            Assert.True(result is OkResult,  "Request deveria ser Sucesso");

        }

        [Fact]
        public void Listar_Inserir_NomeNaoInformado()
        {
            //Arrange
            ICatalogo catalogo = new Catalogo(mockProdutoRepository.Object, mapper);
            var produtoController = new ProdutoController(null, catalogo);
            var prod = new ProdutoView() { Nome = string.Empty, Preco = 100 };

            //Act
            var result = produtoController.Inserir(prod);

            Assert.True(result is BadRequestObjectResult, "Request Result deveria ser BadRequest");

        }

        [Fact]
        public void Listar_Alterar_Sucess()
        {
            //Arrange
            ICatalogo catalogo = new Catalogo(mockProdutoRepository.Object, mapper);
            var produtoController = new ProdutoController(null, catalogo);
            var prod = new ProdutoView() {Id = Guid.NewGuid(),  Nome = "Nome xxx", Preco = 100 };

            //Act
            var result =  produtoController.Alterar(prod);
            Assert.True(result is OkResult, "Request Result deveria ser Sucesso");
        }

        [Fact]
        public void Listar_Alterar_IdNaoInformado()
        {
            //Arrange - Prepara request com id vazio
            ICatalogo catalogo = new Catalogo(mockProdutoRepository.Object, mapper);
            var produtoController = new ProdutoController(null, catalogo);
            var prod = new ProdutoView() { Id = Guid.Empty, Nome = "xxx", Preco = 100 };

            //Act
            var result = produtoController.Alterar(prod);
            Assert.True(result is BadRequestObjectResult, "Request deveria ser Sucesso");
        }
    }
}
