
using System;


namespace HrSystem.InfraStructure.Repositories
{
    public interface IUnitOfWork<T> : IDisposable where T : class 
    {
     
        IGenericRepository<T> GenericsRepo { get; }
        void ChangeState(T t);
        int SaveChanges();     
    }
}
