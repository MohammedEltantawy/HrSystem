using System;
using System.Collections.Generic;

namespace HrSystemApi.ViewModels
{
    public partial class CandidateVM
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public decimal? Mobile { get; set; }
        public DateTime? CreatedAt { get; set; }
 
    }
}
