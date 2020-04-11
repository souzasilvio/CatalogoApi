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

        public void Alterar(Produto registro)
        {
            base.Alterar(registro);
        }

        public void Inserir(Produto registro)
        {
            base.Inserir(registro);
        }

        //public bool AtualizarIdEnvelope(Guid id, string idEnvelope, int status)
        //{
        //    string query = "Update Documento Set IdEnvelope = @IdEnvelope, Status = @Status Where Id = @Id";
        //    var parametros = new DynamicParameters();
        //    parametros.Add("@Id", id, DbType.Guid);
        //    parametros.Add("@IdEnvelope", new Guid(idEnvelope), DbType.Guid);
        //    parametros.Add("@Status", status, DbType.Int32);
        //    Execute(query, parametros);
        //    return true;
        //}

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
