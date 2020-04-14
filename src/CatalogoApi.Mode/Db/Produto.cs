using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace CatalogoApi.Model.Db
{
    [Table("Produto")]
    public class Produto : BaseDbModel
    {
        [ExplicitKey]
        public Guid Id { get; set; }

        public string Nome { get; set; }

        public decimal Preco { get; set; }
    }
}
