using System;
using System.Collections.Generic;

namespace HrSystem.Data.Models
{
    public partial class Vacancy
    {
        public Vacancy()
        {
            Requisitions = new HashSet<Requisition>();
        }

        public int Id { get; set; }
        public string? JobName { get; set; }
        public string? Department { get; set; }
        public string? Qualifications { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? CreatedBy { get; set; }

        public virtual User? CreatedByNavigation { get; set; }
        public virtual ICollection<Requisition> Requisitions { get; set; }
    }
}
