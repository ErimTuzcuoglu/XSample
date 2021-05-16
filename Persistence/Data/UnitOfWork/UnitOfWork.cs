using System;
using System.Data;
using Domain.Entity;
using Domain.Interface.Persistence.Repository;
using Microsoft.Extensions.Configuration;
using Persistence.Data.Repository;
using Persistence.Data.Repository.Contract;

namespace Persistence.Data.UnitOfWork
{
    public class UnitOfWork : ConnectionHelper, IUnitOfWork
    {
        private IDbTransaction _transaction;
        private IDbConnection _connection;
        private readonly IConfiguration _configuration;
        private IGenericRepository<Category> _categoryRepository;
        private IGenericRepository<Country> _countryRepository;
        private IGenericRepository<SubCategory> _subCategoryRepository;
        private IExtendedRepository<Product> _productRepository;
        private bool _disposed;

        public UnitOfWork(IConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
            _connection = CreateConnection();
            _transaction = _connection.BeginTransaction();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public IGenericRepository<Category> CategoryRepository
        {
            get { return _categoryRepository ??= new GenericRepository<Category>(_connection, _transaction, _configuration); }
        }

        public IGenericRepository<Country> CountryRepository
        {
            get { return _countryRepository ??= new GenericRepository<Country>(_connection, _transaction, _configuration); }
        }

        public IGenericRepository<SubCategory> SubCategoryRepository
        {
            get { return _subCategoryRepository ??= new GenericRepository<SubCategory>(_connection, _transaction, _configuration); }
        }

        public IExtendedRepository<Product> ProductRepository
        {
            get { return _productRepository ??= new ExtendedRepository<Product>(_connection, _transaction, _configuration); }
        }

        public void Commit()
        {
            try
            {
                _transaction.Commit();
            }
            catch
            {
                _transaction.Rollback();
                throw;
            }
            finally
            {
                _transaction.Dispose();
                _transaction = _connection.BeginTransaction();
                ResetRepositories();
            }
        }

        private void ResetRepositories()
        {
            _categoryRepository = null;
            _countryRepository = null;
            _productRepository = null;
            _subCategoryRepository = null;
        }

        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (_transaction != null)
                    {
                        _transaction.Dispose();
                        _transaction = null;
                    }

                    if (_connection != null)
                    {
                        _connection.Dispose();
                        _connection = null;
                    }
                }

                _disposed = true;
            }
        }

        ~UnitOfWork()
        {
            Dispose(false);
        }
    }
}