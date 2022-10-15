using System;
using System.Collections.Generic;

namespace HrSystem.Data.Models
{
    public partial class Candidate
    {
        public Candidate()
        {
            Requisitions = new HashSet<Requisition>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public decimal? Mobile { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? CreatedBy { get; set; }

        public virtual User? CreatedByNavigation { get; set; }
        public virtual ICollection<Requisition> Requisitions { get; set; }
    }
}
