using CatalogoApi.Dominio;
using CatalogoApi.Model.View;
using System;
using System.Collections.Generic;
using System.Text;

namespace CatalogoApi.Tests.Mock
{
    public class CatalogoMock : ICatalogo
    {
        public void Alterar(ProdutoView produto)
        {
            throw new NotImplementedException();
        }

        public void Inserir(ProdutoView produto)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ProdutoView> ListarProdutos()
        {
            var lista = new List<ProdutoView>();
            lista.Add(new ProdutoView() { Id = Guid.NewGuid(), Nome = "Produto  1" });
            lista.Add(new ProdutoView() { Id = Guid.NewGuid(), Nome = "Produto  2" });
            return lista;
        }
    }
}
