using Base.DL.DbAccess;
using Base.DL.IRepository;
using Base.Entity.MappedEntities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.DL.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : MappedEntities
    {
        protected IApplicationDbContext _dbContext;
        protected IDbConnection _connection;
        //private DbContextTransaction _transaction;
        private DbSet<T> _table = null;
        public BaseRepository(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _connection = _dbContext.Connection;
            //_transaction = _dbContext.Database.BeginTransaction();
            _table = _dbContext.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return _table.ToList();
        }
        public T GetById(object id)
        {
            return _table.Find(id);
        }
        public void Insert(T obj)
        {
            _table.Add(obj);
        }

        public void InsertRange(IEnumerable<T> objs)
        {
            _table.AddRange(objs);
        }

        public void Update(T obj)
        {
            _table.Attach(obj);
            _dbContext.Entry(obj).State = EntityState.Modified;
        }
        public void Delete(object id)
        {
            T existing = _table.Find(id);
            _table.Remove(existing);
        }
    }
}
