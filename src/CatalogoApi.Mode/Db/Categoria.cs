using Dapper.Contrib.Extensions;
using System;
namespace CatalogoApi.Model.Db
{
    [Table("Categoria")]
    public class Categoria : BaseDbModel
    {
        [Key]
        public int Id { get; set; }

        public string Nome { get; set; }

    }
}
