using CatalogApi.DataAcess;
using CatalogoApi.Model.Db;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace AssinaturaDocumento.DataAcess
{
    public class CategoriaRepository : BaseDataAccess<Categoria>, ICategoriaRepository
    {
        public CategoriaRepository(IDbConnection connection) : base(connection)
        {
        }
    }
}
