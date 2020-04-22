using CatalogoApi.Model.Db;
using Dapper;
using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogApi.DataAcess
{
    public class BaseDataAccess<T> where T : BaseDbModel
    {
        protected IDbConnection Connection { get; }

        public BaseDataAccess(IDbConnection connection)
        {
            Connection = connection;
        }

        public async Task<T> Obter(string query, Guid Id)
        {
            var parametros = new DynamicParameters();
            if (Id != Guid.Empty)
            {
                parametros.Add("@Id", Id, DbType.Guid);
                query += " where c.Id = @Id";
            }

            try
            {
                Connection.Open();
                return await Connection.QueryFirstOrDefaultAsync<T>(query, parametros);
            }
            finally
            {
                Connection.Close();
            }
        }

        public int Execute(string query, DynamicParameters parametros)
        {
            try
            {
                Connection.Open();
                return Connection.Execute(query, parametros);
            }
            finally
            {
                Connection.Close();
            }
        }

        public List<T> Listar()
        {
            try
            {
                Connection.Open();
                return Connection.GetAll<T>().ToList();
            }
            finally
            {
                Connection.Close();
            }
        }

        public void Inserir(T registro)
        {
            try
            {
                Connection.Open();
                registro.DataCriacao = DateTime.Now;
                registro.DataModificacao = DateTime.Now;
                Connection.Insert<T>(registro);
            }
            finally
            {
                Connection.Close();
            }
        }

        public void Alterar(T registro)
        {
            try
            {
                Connection.Open();
                Connection.Update<T>(registro);
            }
            finally
            {
                Connection.Close();
            }
        }
    }
}
