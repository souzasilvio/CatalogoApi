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
        private readonly string Query = @"Select * From Categoria c ";
        public CategoriaRepository(IDbConnection connection) : base(connection)
        {
        }

        public void Inserir(Categoria registro)
        {
            base.Inserir(registro);
        }

        public void Alterar(Categoria registro)
        {
            string query = "Update Categoria Set Nome = @Nome, DataModificacao = @DataModificacao Where Id = @Id";
            var parametros = new DynamicParameters();
            parametros.Add("@Id", registro.Id, DbType.Guid);
            parametros.Add("@Nome", registro.Nome, DbType.String);
            parametros.Add("@DataModificacao", registro.DataModificacao, DbType.Date);
            Execute(query, parametros);
        }
      
    }
}
