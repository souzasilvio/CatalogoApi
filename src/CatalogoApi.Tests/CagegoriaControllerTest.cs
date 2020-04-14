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
    public class CategoriaControllerTest
    {
        private readonly MockCategoriaRepository mock;
        private readonly IMapper mapper;
        private readonly ICategoria dominio;
        private readonly CategoriaController controller;
        
        /// <summary>
        /// Setup
        /// </summary>
        public CategoriaControllerTest()
        {
            var registro = new Categoria() { Id = 1, Nome = "Eletronicos"};
            var registro1 = new Categoria() { Id = 2, Nome = "Som" };
            var lista = new List<Categoria>();
            lista.Add(registro);
            lista.Add(registro1);

            mock = new MockCategoriaRepository()
                .MockListar(lista);

            //auto mapper configuration
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapping());
            });
            mapper = mockMapper.CreateMapper();

            dominio = new CategoriaDomain(mock.Object, mapper);            
            controller = new CategoriaController(null, dominio);

        }

        [Fact]
        public void Listar_VerificaResultado_Sucess()
        {
            //Act
            var lista  = (List<CategoriaView>)controller.Listar();

            //Assert
            Assert.True(lista.Count > 0, "Lista deveria conter valores na coleção.");

        }

        [Fact]
        public void Inserir_Sucess()
        {
            var controller = new CategoriaController(null, dominio);
            var prod = new CategoriaView() { Nome = "Categoria 1"};

            //Act
            var result = controller.Inserir(prod);

            Assert.True(result is OkResult,  "Request deveria ser Sucesso");

        }

        [Fact]
        public void Inserir_ValidaNomeNaoInformado_Sucess()
        {
            //Arrange
            var prod = new CategoriaView() { Nome = string.Empty};

            //Act
            var result = controller.Inserir(prod);

            Assert.True(result is BadRequestObjectResult, "Request Result deveria ser BadRequest");

        }

        [Fact]
        public void Alterar_Sucess()
        {
            //Arrange
            var prod = new CategoriaView() {Id = 1,  Nome = "Nome xxx"};

            //Act
            var result =  controller.Alterar(prod);
            Assert.True(result is OkResult, "Request Result deveria ser Sucesso");
        }

        [Fact]
        public void Alterar_ValidaAlteracaoIdNaoInformado_Sucess()
        {
            //Arrange - Prepara request com id vazio
            var prod = new CategoriaView() { Id = 0, Nome = "xxx" };

            //Act
            var result = controller.Alterar(prod);
            Assert.True(result is BadRequestObjectResult, "Request deveria ser BadRequest");
        }
    }
}
