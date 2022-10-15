using System;
using System.Collections.Generic;

namespace HrSystemApi.ViewModels
{
    public partial class VacancyVM
    { 
        public string? JobName { get; set; }
        public string? Department { get; set; }
        public string? Qualifications { get; set; }
        public DateTime? CreatedAt { get; set; }
 
    }
}
