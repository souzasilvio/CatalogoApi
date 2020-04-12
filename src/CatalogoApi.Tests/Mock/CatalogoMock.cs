using CatalogoApi.Dominio;
using CatalogoApi.Model.View;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace CatalogoApi.Tests.Mock
{
    public class MockCatalogo : Mock<ICatalogo>
    {
        public MockCatalogo MockListarProdutos(IEnumerable<ProdutoView> results)
        {
            Setup(x => x.ListarProdutos())
            .Returns(results);
            return this;
        }

        public MockCatalogo MockAlterar(ProdutoView registro)
        {
            Setup(x => x.Alterar(registro));            
            return this;
        }

        public MockCatalogo MockInserir(ProdutoView registro)
        {
            Setup(x => x.Inserir(registro));
            return this;
        }

        public static IEnumerable<ProdutoView> ProdutosTeste()
        {
            var lista = new List<ProdutoView>();
            lista.Add(new ProdutoView() { Id = Guid.NewGuid(), Nome = "Produto  1" });
            lista.Add(new ProdutoView() { Id = Guid.NewGuid(), Nome = "Produto  2" });
            return lista;
        }
    }
}
