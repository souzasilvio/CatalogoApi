using AssinaturaDocumento.DataAcess;
using CatalogoApi.Model.Db;
using CatalogoApi.Model.View;
using System;
using System.Collections.Generic;

namespace CatalogoApi.Dominio
{
    public class Catalogo : ICatalogo
    {
        private readonly IProdutoRepository produtoRepository;
        public Catalogo(IProdutoRepository repository)
        {
            produtoRepository = repository; 
        }

        public void Inserir(ProdutoView produto)
        {
            var prod = new Produto()
            {
                Id = produto.Id,
                Nome = produto.Nome,
                Preco = produto.Preco,
                DataCriacao = DateTime.Now,
                DataModificacao = DateTime.Now
                
            };

            produtoRepository.Inserir(prod);
        }

        public IEnumerable<ProdutoView> ListarProdutos()
        {
            var lista = produtoRepository.Listar().GetAwaiter().GetResult();
            var result = new List<ProdutoView>();
            foreach (Produto p in lista)
            {
                result.Add(new ProdutoView() { Id = p.Id, Nome = p.Nome, Preco = p.Preco });
            }
            return result;
        }
    }
}
