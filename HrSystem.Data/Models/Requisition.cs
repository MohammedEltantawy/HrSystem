using System;
using System.Collections.Generic;

namespace HrSystem.Data.Models
{
    public partial class Requisition
    {
        public int Id { get; set; }
        public int VacancyId { get; set; }
        public int CandidateId { get; set; }
        public bool? ManagerApproval { get; set; }
        public bool? DirectorApproval { get; set; }
        public int? Status { get; set; }
        public string? CreatedBy { get; set; }

        public virtual Candidate Candidate { get; set; } = null!;
        public virtual User? CreatedByNavigation { get; set; }
        public virtual Status? StatusNavigation { get; set; }
        public virtual Vacancy Vacancy { get; set; } = null!;
    }
}
