using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Test.Core.Interfaces;

namespace Test.Core.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _dbContext;

        private Dictionary<string, object> _repositories;

        public UnitOfWork(DbContext dbContext)
        {
            _repositories = new Dictionary<string, object>();
            _dbContext = dbContext;
        }

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            var name = typeof(TEntity).Name;

            if (!_repositories.ContainsKey(name))
            {
                _repositories[name] = new Repository<TEntity>(_dbContext);
            }

            return (IRepository<TEntity>)_repositories[name];
        }

        public Task Commit()
        {
            return _dbContext.SaveChangesAsync();
        }
    }
}
