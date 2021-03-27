using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Test.Core.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;

        Task Commit();
    }
}
