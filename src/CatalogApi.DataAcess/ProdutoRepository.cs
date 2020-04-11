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
        private readonly string Query = @"Select * From Produto c ";
        public ProdutoRepository(IDbConnection connection) : base(connection)
        {
        }

        //public void Alterar(Produto registro)
        //{
        //    base.Alterar(registro);
        //}

        public void Inserir(Produto registro)
        {
            base.Inserir(registro);
        }

        public void Alterar(Produto registro)
        {
            string query = "Update Produto Set Nome = @Nome, Preco = @Preco, DataModificacao = @DataModificacao Where Id = @Id";
            var parametros = new DynamicParameters();
            parametros.Add("@Id", registro.Id, DbType.Guid);
            parametros.Add("@Nome", registro.Nome, DbType.String);
            parametros.Add("@Preco", registro.Preco, DbType.Decimal);
            parametros.Add("@DataModificacao", registro.DataModificacao, DbType.Date);
            Execute(query, parametros);
        }

        //public void MarcarEnvelopeCompleto(string idEnvelope)
        //{
        //    string query = "Update Documento Set Status = @Status Where IdEnvelope = @IdEnvelope";
        //    var parametros = new DynamicParameters();
        //    parametros.Add("@IdEnvelope", new Guid(idEnvelope), DbType.Guid);
        //    parametros.Add("@Status", (int)DocumentoDbModel.StatusCode.AssinadoClientes, DbType.Int32);
        //    Execute(query, parametros);
        //}
    }
}
