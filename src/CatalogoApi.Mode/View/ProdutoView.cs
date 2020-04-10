using System;

namespace CatalogoApi.Model.View
{
    public class ProdutoView: ModelViewBase
    {
        public string Nome { get; set; }

        public decimal Preco { get; set; }
    }
}
