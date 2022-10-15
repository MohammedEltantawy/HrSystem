using HrSystem.Data.Models;
using HrSystem.Services;
using HrSystemApi.ViewModels;

namespace HrSystemApi.Serives
{
    public class RequisitionManager
    {
        private readonly RequisitionService _requisitionService;

        public RequisitionManager(RequisitionService requisitionService)
        {
            _requisitionService = requisitionService;
        }

        public string CreateRequisition(RequisitionVM requisition)
        {
            var obj = new Requisition
            {
                VacancyId = requisition.VacancyId,
                CandidateId = requisition.CandidateId,
                ManagerApproval = requisition.ManagerApproval,
                DirectorApproval = requisition.DirectorApproval,
                Status = (int)StatusEnum.Pending  //Pending
            };
            var result = _requisitionService.CreateRequisition(obj);
            if (result) return obj.Id.ToString();
            return "Error";
        }

        public string HrManagerApproveRequisition(int requisitionId)
        {
            var obj = _requisitionService.GetById(requisitionId);
            if (obj is not null)
            {
                obj.ManagerApproval = true;
                UpdateRequisitionOverAllStatus(obj);
                var result = _requisitionService.UpdateRequisition(obj);
                if (result) return obj.Id.ToString();
                return "Error";
            }
            return "Error";
        }


        public string HrDirectorApproveRequisition(int requisitionId)
        {
            var obj = _requisitionService.GetById(requisitionId);
            if (obj is not null)
            {
                obj.DirectorApproval = true;
                UpdateRequisitionOverAllStatus(obj);
                var result = _requisitionService.UpdateRequisition(obj);
                if (result) return obj.Id.ToString();
                return "Error";
            }
            return "Error";
        }

        private static void UpdateRequisitionOverAllStatus(Requisition? obj)
        {
            if (obj is not null)
            {
                if (obj.ManagerApproval == true && obj.DirectorApproval == true)
                {
                    obj.Status = (int)StatusEnum.Approved;  //Approved
                }
                else if (obj.ManagerApproval == false || obj.DirectorApproval == false)
                {
                    obj.Status = (int)StatusEnum.Rejected;  //Rejected
                }
            }
        }
    }
}
