using System;
using System.Collections.Generic;

namespace HrSystem.Data.Models
{
    public partial class Status
    {
        public Status()
        {
            Requisitions = new HashSet<Requisition>();
        }

        public int Id { get; set; }
        public string? StatusName { get; set; }
        public string? StatusValue { get; set; }

        public virtual ICollection<Requisition> Requisitions { get; set; }
    }
}
