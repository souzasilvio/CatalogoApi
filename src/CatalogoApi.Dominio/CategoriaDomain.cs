using AssinaturaDocumento.DataAcess;
using AutoMapper;
using CatalogoApi.Model.Db;
using CatalogoApi.Model.View;
using System;
using System.Collections.Generic;

namespace CatalogoApi.Dominio
{
    public class CategoriaDomain : ICategoria
    {
        private readonly ICategoriaRepository categoriaRepository;
        private readonly IMapper mapper;
        public CategoriaDomain(ICategoriaRepository repository, IMapper _mapper)
        {
            categoriaRepository = repository;
            mapper = _mapper;
        }

        public void Inserir(CategoriaView registro)
        {
            var registroCt = mapper.Map<Categoria>(registro);
            registroCt.DataCriacao = DateTime.Now;
            registroCt.DataModificacao = DateTime.Now;
            categoriaRepository.Inserir(registroCt);
        }

        public void Alterar(CategoriaView rg)
        {
            var registro = mapper.Map<Categoria>(rg);
            registro.DataModificacao = DateTime.Now;
            categoriaRepository.Alterar(registro);
        }

        public IEnumerable<CategoriaView> Listar()
        {
            var lista = categoriaRepository.Listar();
            var result = new List<CategoriaView>();
            foreach (Categoria p in lista)
            {
                var registro = mapper.Map<CategoriaView>(p);
                result.Add(registro);
            }
            return result;
        }

    }
}
