using HrSystem.Data.Models;
using HrSystem.InfraStructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace HrSystem.Services
{
    public class CandidateService
    {
        IUnitOfWork<Candidate> _unitOfCandidate;


        private List<Expression<Func<Candidate, bool>>> predicateList = null;

        public CandidateService(IUnitOfWork<Candidate> unitOfCandidate)
        {
            _unitOfCandidate = unitOfCandidate;
            predicateList = new List<Expression<Func<Candidate, bool>>>();
        }

        public int GetCount()
        {
            var Candidates = _unitOfCandidate.GenericsRepo.GetAll(predicateList).Count();
            return Candidates;
        }


        public void AddToPredicateList(Expression<Func<Candidate, bool>> predicate)
        {
            predicateList.Add(predicate);
        }
        public void ClearPredicateList()
        {
            predicateList.Clear();
        }
        public IQueryable<Candidate> GetAll(int? pageIndex, int? pageSize, string? orderBy)
        {
            var Candidates = _unitOfCandidate.GenericsRepo
                .GetAllWithRelatedandPaginated(false, orderBy, pageIndex, pageSize, predicateList, o => o.CreatedByNavigation);
            return Candidates;
        }

        public Candidate? GetById(int id)
        {
            predicateList.Add(c => c.Id == id);
            var Candidate = _unitOfCandidate.GenericsRepo.GetAllWithRelated(predicateList,q => q.CreatedByNavigation).FirstOrDefault();
            return Candidate;
        }


        public bool CreateCandidate(Candidate Candidate)
        {
            try
            {
                _unitOfCandidate.GenericsRepo.Insert(Candidate);
                _unitOfCandidate.SaveChanges();
                return true;
            }
            catch (Exception x)
            {
                return false;
            }
        }

        public bool UpdateCandidate(Candidate Candidate)
        {
            try
            {
                _unitOfCandidate.GenericsRepo.Update(Candidate);
                _unitOfCandidate.ChangeState(Candidate);
                _unitOfCandidate.SaveChanges();
                return true;
            }
            catch (Exception x)
            {
                return false;
            }
        }


    }
}
