using CatalogoApi.Model.Db;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AssinaturaDocumento.DataAcess
{
    public interface ICategoriaRepository
    {
        List<Categoria> Listar();
        void Inserir(Categoria registro);
        void Alterar(Categoria registro);
    }
}
