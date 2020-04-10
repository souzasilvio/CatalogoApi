using CatalogoApi.Model.View;
using System.Collections.Generic;

namespace CatalogoApi.Dominio
{
    public interface ICatalogo
    {
        IEnumerable<ProdutoView> ListarProdutos();
        void Inserir(ProdutoView produto);
    }
}