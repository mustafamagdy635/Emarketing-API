using Emarketing_API.DataAccess.Data;
using Emarketing_API.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emarketing_API.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Context _db;
        public  IRepositoryCategories _repositoryCategories { get; private set; }
        public IRepositroyBrands _repositroyBrands { get; private set; }
        public IRepositoryProduct repositoryProduct { get; private set; }
        public IRepositoryStock _repositoryStock { get; private set; }
        public IRepositoryApplicationUser _repositoryApplicationUser { get; private set; }
        public IRepositoryShoppingCart _repositoryShoppingCart { get; private set; }
        public IRepositoryOrderDetail _repositoryOrderDetail { get; private set; }
        public IRepositoryOrderHeader _repositoryOrderHeader { get; private set; }


        public UnitOfWork(Context db) 
        {
            this._db = db;
            _repositoryCategories =new RepositoryCategories(_db);
            _repositroyBrands = new RepositroyBrands(_db);
            repositoryProduct = new RepositoryProduct(_db);
            _repositoryStock = new RepositoryStock(_db);
            _repositoryApplicationUser = new RepositoryApplicationUser(_db);
            _repositoryShoppingCart = new RepositoryShoppingCart(_db);
            _repositoryOrderDetail = new RepositoryOrderDetail(_db);
            _repositoryOrderHeader = new RepositoryOrderHeader(_db);
        }


        public void Save()
        {
           _db.SaveChanges();
        }
    }
}
