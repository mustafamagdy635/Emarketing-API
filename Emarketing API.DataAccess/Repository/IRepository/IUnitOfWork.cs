using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emarketing_API.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IRepositoryCategories _repositoryCategories { get; }
        IRepositroyBrands _repositroyBrands { get; }
        IRepositoryProduct repositoryProduct { get; }   
        IRepositoryStock _repositoryStock { get; }
        IRepositoryApplicationUser _repositoryApplicationUser { get; }
        IRepositoryShoppingCart _repositoryShoppingCart { get; }
        IRepositoryOrderDetail _repositoryOrderDetail { get; }
        IRepositoryOrderHeader _repositoryOrderHeader { get; }
        public void Save();

    }
}
