using AssinaturaDocumento.DataAcess;
using AutoMapper;
using CatalogoApi.Model.Db;
using CatalogoApi.Model.View;
using System;
using System.Collections.Generic;

namespace CatalogoApi.Dominio
{
    public class Catalogo : DomainBase<Produto>, ICatalogo
    {
        private readonly IProdutoRepository produtoRepository;
        public Catalogo(IProdutoRepository repository, IMapper _mapper) : base(_mapper)
        {
            produtoRepository = repository;            
        }

        public void Inserir(ProdutoView produto)
        {
            var registro = Mapper.Map<Produto>(produto);
            if (registro.Id == Guid.Empty)
            {
                registro.Id = Guid.NewGuid();
            }
            produtoRepository.Inserir(registro);
        }

        public void Alterar(ProdutoView produto)
        {
            var registro = Mapper.Map<Produto>(produto);
            produtoRepository.Alterar(registro);
        }

        public IEnumerable<ProdutoView> ListarProdutos()
        {
            var lista = produtoRepository.Listar();
            var result = new List<ProdutoView>();
            foreach (Produto p in lista)
            {
                var registro = Mapper.Map<ProdutoView>(p);
                result.Add(registro);
            }
            return result;
        }
    }
}
