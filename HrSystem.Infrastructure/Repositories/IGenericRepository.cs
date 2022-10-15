using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
 

namespace HrSystem.InfraStructure.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        IQueryable<T> GetAll(List<Expression<Func<T, bool>>> predicateList);
        IQueryable<T> GetAllWithRelated(List<Expression<Func<T, bool>>> predicateList, params Expression<Func<T, object>>[] expressionList);
        IQueryable<T> GetAllWithRelatedandPaginated(bool? sortOrderDir,string? orderBy, int? pageIndex, int? pageSize,
                               List<Expression<Func<T, bool>>> predicateList, params Expression<Func<T, object>>[] expressionList);
        Task<T> SingleAsync(Expression<Func<T, bool>> predicate);
        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);
        T GetById(object id);     
        void Insert(T obj);
        void Update(T obj);
        void Delete(object id);
        void DeleteAll();
        void DeleteComposite(object first,object second);      
    }
}
