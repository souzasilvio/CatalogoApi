using AssinaturaDocumento.DataAcess;
using AutoMapper;
using CatalogoApi.Model.Db;
using CatalogoApi.Model.View;
using System;
using System.Collections.Generic;

namespace CatalogoApi.Dominio
{
    public class CategoriaDomain : DomainBase<Categoria>, ICategoria
    {
        private readonly ICategoriaRepository categoriaRepository;
        public CategoriaDomain(ICategoriaRepository repository, IMapper _mapper) : base(_mapper)
        {
            categoriaRepository = repository;
        }

        public void Inserir(CategoriaView registro)
        {
            var registroCt = Mapper.Map<Categoria>(registro);
            categoriaRepository.Inserir(registroCt);
        }

        public void Alterar(CategoriaView rg)
        {
            var registro = Mapper.Map<Categoria>(rg);
            categoriaRepository.Alterar(registro);
        }

        public IEnumerable<CategoriaView> Listar()
        {
            var lista = categoriaRepository.Listar();
            var result = new List<CategoriaView>();
            foreach (Categoria p in lista)
            {
                var registro = Mapper.Map<CategoriaView>(p);
                result.Add(registro);
            }
            return result;
        }

    }
}
