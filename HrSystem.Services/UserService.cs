using HrSystem.Data.Models;
using HrSystem.InfraStructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace HrSystem.Services
{
    public class UserService
    {
        IUnitOfWork<User> _unitOfUser;


        private List<Expression<Func<User, bool>>> predicateList = null;

        public UserService(IUnitOfWork<User> unitOfUser)
        {
            _unitOfUser = unitOfUser;
            predicateList = new List<Expression<Func<User, bool>>>();
        }    

        public void AddToPredicateList(Expression<Func<User, bool>> predicate)
        {
            predicateList.Add(predicate);
        }
        public void ClearPredicateList()
        {
            predicateList.Clear();
        }
        public IQueryable<User> GetAll(int? pageIndex, int? pageSize, string? orderBy)
        {
            var Users = _unitOfUser.GenericsRepo
                .GetAllWithRelatedandPaginated(false, orderBy, pageIndex, pageSize, predicateList, u => u.Roles);
            return Users;
        }

        public User? GetById(string id)
        {
            predicateList.Add(c => c.Id == id);
            var User = _unitOfUser.GenericsRepo.GetAllWithRelated(predicateList, u => u.Roles).FirstOrDefault();
            return User;
        }

 
    }
}
