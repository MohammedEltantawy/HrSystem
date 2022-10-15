using System.Threading.Tasks;
using HrSystem.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace HrSystem.InfraStructure.Repositories
{
    public class UnitOfWork<T> : IUnitOfWork<T> where T : class
    {
        private readonly HrSystemContext _hrSystemContext  ;
        public UnitOfWork(HrSystemContext HrSystemContext)
        {
            _hrSystemContext = HrSystemContext;
        }
        private IGenericRepository<T> _GenericsRepo;
        public IGenericRepository<T> GenericsRepo
        {
            get
            {
                if (this._GenericsRepo == null)
                {
                    this._GenericsRepo = new GenericRepository<T>(_hrSystemContext); 
                }
                return this._GenericsRepo;
            }
        }      
        public void ChangeState(T obj)
        {
            _hrSystemContext.Entry(obj).State = EntityState.Modified;
        }    
        public async Task<int> SaveChangesAsync()
        {       
            return await _hrSystemContext.SaveChangesAsync();
        }
        public int SaveChanges()
        {          
            return _hrSystemContext.SaveChanges();
        }
        public void Dispose()
        {
            _hrSystemContext.Dispose();
        }     
    }
}
