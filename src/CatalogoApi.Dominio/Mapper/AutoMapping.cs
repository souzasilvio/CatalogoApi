using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using CatalogoApi.Model.Db;
using CatalogoApi.Model.View;

namespace CatalogoApi.Dominio.Mapper
{

    /// <summary>
    /// Configura os mapeamentos de classes DTO e View
    /// </summary>
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<ProdutoView, Produto>();
            CreateMap<Produto, ProdutoView>();
        }
    }
}
