﻿using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace CatalogoApi.Model.Db
{
    public class BaseDbModel
    {
        public DateTime DataCriacao { get; set; }
        public DateTime DataModificacao { get; set; }

    }
}
