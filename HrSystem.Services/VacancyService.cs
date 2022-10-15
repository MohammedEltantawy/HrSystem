using HrSystem.Data.Models;
using HrSystem.InfraStructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace HrSystem.Services
{
    public class VacancyService
    {
        IUnitOfWork<Vacancy> _unitOfVacancy;


        private List<Expression<Func<Vacancy, bool>>> predicateList = null;

        public VacancyService(IUnitOfWork<Vacancy> unitOfVacancy)
        {
            _unitOfVacancy = unitOfVacancy;
            predicateList = new List<Expression<Func<Vacancy, bool>>>();
        }

        public int GetCount()
        {
            var Vacancys = _unitOfVacancy.GenericsRepo.GetAll(predicateList).Count();
            return Vacancys;
        }


        public void AddToPredicateList(Expression<Func<Vacancy, bool>> predicate)
        {
            predicateList.Add(predicate);
        }
        public void ClearPredicateList()
        {
            predicateList.Clear();
        }
        public IQueryable<Vacancy> GetAll(int? pageIndex, int? pageSize, string? orderBy)
        {
            var Vacancys = _unitOfVacancy.GenericsRepo
                .GetAllWithRelatedandPaginated(false, orderBy, pageIndex, pageSize, predicateList, o => o.CreatedByNavigation);
            return Vacancys;
        }

        public Vacancy? GetById(int id)
        {
            predicateList.Add(c => c.Id == id);
            var Vacancy = _unitOfVacancy.GenericsRepo.GetAllWithRelated(predicateList,q => q.CreatedByNavigation).FirstOrDefault();
            return Vacancy;
        }


        public bool CreateVacancy(Vacancy Vacancy)
        {
            try
            {
                _unitOfVacancy.GenericsRepo.Insert(Vacancy);
                _unitOfVacancy.SaveChanges();
                return true;
            }
            catch (Exception x)
            {
                return false;
            }
        }

        public bool UpdateVacancy(Vacancy Vacancy)
        {
            try
            {
                _unitOfVacancy.GenericsRepo.Update(Vacancy);
                _unitOfVacancy.ChangeState(Vacancy);
                _unitOfVacancy.SaveChanges();
                return true;
            }
            catch (Exception x)
            {
                return false;
            }
        }


    }
}
