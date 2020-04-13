using AssinaturaDocumento.DataAcess;
using AutoMapper;
using CatalogoApi.Model.Db;
using CatalogoApi.Model.View;
using System;
using System.Collections.Generic;

namespace CatalogoApi.Dominio
{
    public class Catalogo : ICatalogo
    {
        private readonly IProdutoRepository produtoRepository;
        private readonly IMapper mapper;
        public Catalogo(IProdutoRepository repository, IMapper _mapper)
        {
            produtoRepository = repository;
            mapper = _mapper;
        }

        public void Inserir(ProdutoView produto)
        {
            var registro = mapper.Map<Produto>(produto);
            if (registro.Id == Guid.Empty)
            {
                registro.Id = Guid.NewGuid();
            }
            registro.DataCriacao = DateTime.Now;
            registro.DataModificacao = DateTime.Now;
            produtoRepository.Inserir(registro);
        }

        public void Alterar(ProdutoView produto)
        {
            var registro = mapper.Map<Produto>(produto);
            registro.DataModificacao = DateTime.Now;
            produtoRepository.Alterar(registro);
        }

        public IEnumerable<ProdutoView> ListarProdutos()
        {
            var lista = produtoRepository.Listar();
            var result = new List<ProdutoView>();
            foreach (Produto p in lista)
            {
                var registro = mapper.Map<ProdutoView>(p);
                result.Add(registro);
            }
            return result;
        }
    }
}
