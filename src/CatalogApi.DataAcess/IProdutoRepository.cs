using CatalogoApi.Model.Db;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AssinaturaDocumento.DataAcess
{
    public interface IProdutoRepository
    {
        Task<List<Produto>> Listar();
        void Inserir(Produto prod);
        void Alterar(Produto registro);
    }
}
