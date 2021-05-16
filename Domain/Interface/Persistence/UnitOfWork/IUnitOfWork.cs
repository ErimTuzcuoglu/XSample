using System;
using Domain.Entity;
using Domain.Interface.Persistence.Repository;
using Persistence.Data.Repository.Contract;

namespace Persistence.Data.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Category> CategoryRepository { get; }
        IGenericRepository<Country> CountryRepository { get; }
        IGenericRepository<SubCategory> SubCategoryRepository { get; }
        IExtendedRepository<Product> ProductRepository { get; }

        void Commit();
    }
}