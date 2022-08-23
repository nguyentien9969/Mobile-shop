using Base.DL.IRepository;
using Base.DL.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.DL.DbAccess
{
    public interface IUnitOfWork : IDisposable
    {
        IProductCategoryRepository ProductCategoryRepository { get; }
        IProductRepository ProductRepository { get; }
        ICustomerRepository CustomerRepository { get; }
        IOrderRepository OrderRepository { get; }
        IOrderItemRepository OrderItemRepository { get; }

        void BeginTransaction();
        void Commit();
        int SaveChanges();
    }

    public class UnitOfWork : IUnitOfWork
    {
        private IDbConnection _connection;
        private IDbTransaction _transaction;
        public IApplicationDbContext _dbContext { get; }
        private bool _disposed;


        private IProductCategoryRepository _productCategoryRepository;
        private IProductRepository _productRepository;
        private ICustomerRepository _customerRepository;
        private IOrderRepository _orderRepository;
        private IOrderItemRepository _orderItemRepository;

        public UnitOfWork(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IProductCategoryRepository ProductCategoryRepository
        {
            get { return _productCategoryRepository ?? (_productCategoryRepository = new ProductCategoryRepository(_dbContext)); }
        }

        public IProductRepository ProductRepository
        {
            get { return _productRepository ?? (_productRepository = new ProductRepository(_dbContext)); }
        }

        public ICustomerRepository CustomerRepository
        {
            get { return _customerRepository ?? (_customerRepository = new CustomerRepository(_dbContext)); }
        }

        public IOrderRepository OrderRepository
        {
            get { return _orderRepository ?? (_orderRepository = new OrderRepository(_dbContext)); }
        }

        public IOrderItemRepository OrderItemRepository
        {
            get { return _orderItemRepository ?? (_orderItemRepository = new OrderItemRepository(_dbContext)); }
        }

        public int SaveChanges()
        {
            return _dbContext.SaveChanges();
        }

        public void BeginTransaction()
        {
            _connection = _dbContext.Connection;
            _connection.Open();
            _transaction = _connection.BeginTransaction();
            _dbContext.Database.UseTransaction((DbTransaction)_transaction);
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
                _connection.Close();
                resetRepositories();
            }
        }

        private void resetRepositories()
        {
            _productCategoryRepository = null;
        }

        public void Dispose()
        {
            dispose(true);
            GC.SuppressFinalize(this);
        }

        private void dispose(bool disposing)
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
            dispose(false);
        }
    }
}
