using Emarketing_AP.Models;
using Emarketing_API.DataAccess.Data;
using Emarketing_API.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emarketing_API.DataAccess.Repository
{
    public class RepositoryStock : BaseRepository<Stocks>, IRepositoryStock
    {
        public RepositoryStock(Context db) : base(db)
        {
        }
    }
}
