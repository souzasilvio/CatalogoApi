using CatalogoApi.Model.View;
using System.Collections.Generic;

namespace CatalogoApi.Dominio
{
    public interface ICategoria
    {
        IEnumerable<CategoriaView> Listar();
        void Inserir(CategoriaView registro);

        void Alterar(CategoriaView registro);
    }
}