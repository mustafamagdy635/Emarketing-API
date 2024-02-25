﻿using Emarketing_API.DataAccess.Data;
using Emarketing_API.DataAccess.Repository.IRepository;
using Emarketing_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emarketing_API.DataAccess.Repository
{
    public class RepositroyBrands : BaseRepository<Brands>, IRepositroyBrands
    {
        public RepositroyBrands(Context db) : base(db)
        {
        }
    }
}
