using Emarketing_API.DataAccess.Data;
using Emarketing_API.DataAccess.Repository.IRepository;
using Emarketing_API.Modles.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emarketing_API.DataAccess.Repository
{
    internal class RepositoryShoppingCart : BaseRepository<ShoppingCart>, IRepositoryShoppingCart
    {
        public RepositoryShoppingCart(Context db) : base(db)
        {
        }
    }
}
