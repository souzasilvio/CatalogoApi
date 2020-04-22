using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace CatalogoApi.Dominio
{
    public class DomainBase<T> where T : class
    {
        private readonly IMapper mapper;
        public IMapper Mapper
        {
            get { return mapper; }
        }
        public DomainBase(IMapper _mapper)
        {
            mapper = _mapper;
        }
    }
}
