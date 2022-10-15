using System;
using System.Collections.Generic;

namespace  HrSystemApi.ViewModels
{ 
    public partial class RequisitionVM
    {
        public int VacancyId { get; set; }
        public int CandidateId { get; set; }
        public bool? ManagerApproval { get; set; }
        public bool? DirectorApproval { get; set; }
        public int? Status { get; set; }
 
    }
}
