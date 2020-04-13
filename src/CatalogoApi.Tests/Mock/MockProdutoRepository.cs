using AssinaturaDocumento.DataAcess;
using CatalogoApi.Model.Db;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CatalogoApi.Tests.Mock
{
    public class MockProdutoRepository : Mock<IProdutoRepository>
    {

        public MockProdutoRepository MockListar(List<Produto> results)
        {
            Setup(x => x.Listar())
            .Returns(results);
            return this;
        }

        public MockProdutoRepository MockInserir(Produto registro)
        {
            Setup(x => x.Inserir(registro));
            return this;
        }

        public MockProdutoRepository MockAlterar(Produto registro)
        {
            Setup(x => x.Alterar(registro));
            return this;
        }
    }
}
