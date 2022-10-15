using HrSystem.Data.Models;
using HrSystem.InfraStructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace HrSystem.Services
{
    public class RequisitionService
    {
        IUnitOfWork<Requisition> _unitOfRequisition;


        private List<Expression<Func<Requisition, bool>>> predicateList = null;

        public RequisitionService(IUnitOfWork<Requisition> unitOfRequisition)
        {
            _unitOfRequisition = unitOfRequisition;
            predicateList = new List<Expression<Func<Requisition, bool>>>();
        }

        public int GetCount()
        {
            var Requisitions = _unitOfRequisition.GenericsRepo.GetAll(predicateList).Count();
            return Requisitions;
        }


        public void AddToPredicateList(Expression<Func<Requisition, bool>> predicate)
        {
            predicateList.Add(predicate);
        }
        public void ClearPredicateList()
        {
            predicateList.Clear();
        }
        public IQueryable<Requisition> GetAll(int? pageIndex, int? pageSize, string? orderBy)
        {
            var Requisitions = _unitOfRequisition.GenericsRepo
                .GetAllWithRelatedandPaginated(false, orderBy, pageIndex, pageSize, predicateList, o => o.CreatedByNavigation);
            return Requisitions;
        }

        public Requisition? GetById(int id)
        {
            predicateList.Add(c => c.Id == id);
            var Requisition = _unitOfRequisition.GenericsRepo.GetAllWithRelated(predicateList,q => q.CreatedByNavigation).FirstOrDefault();
            return Requisition;
        }


        public bool CreateRequisition(Requisition Requisition)
        {
            try
            {
                _unitOfRequisition.GenericsRepo.Insert(Requisition);
                _unitOfRequisition.SaveChanges();
                return true;
            }
            catch (Exception x)
            {
                return false;
            }
        }

        public bool UpdateRequisition(Requisition Requisition)
        {
            try
            {
                _unitOfRequisition.GenericsRepo.Update(Requisition);
                _unitOfRequisition.ChangeState(Requisition);
                _unitOfRequisition.SaveChanges();
                return true;
            }
            catch (Exception x)
            {
                return false;
            }
        }


    }
}
