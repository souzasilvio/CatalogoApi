using CatalogApi.DataAcess;
using CatalogoApi.Model.Db;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace AssinaturaDocumento.DataAcess
{
    public class ProdutoRepository : BaseDataAccess<Produto>, IProdutoRepository
    {
        public ProdutoRepository(IDbConnection connection) : base(connection)
        {
        }

    }
}
