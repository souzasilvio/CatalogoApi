using AssinaturaDocumento.DataAcess;
using CatalogoApi.Model.Db;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CatalogoApi.Tests.Mock
{
    public class MockCategoriaRepository : Mock<ICategoriaRepository>
    {

        public MockCategoriaRepository MockListar(List<Categoria> results)
        {
            Setup(x => x.Listar())
            .Returns(results);
            return this;
        }

        public MockCategoriaRepository MockInserir(Categoria registro)
        {
            Setup(x => x.Inserir(registro));
            return this;
        }

        public MockCategoriaRepository MockAlterar(Categoria registro)
        {
            Setup(x => x.Alterar(registro));
            return this;
        }
    }
}
