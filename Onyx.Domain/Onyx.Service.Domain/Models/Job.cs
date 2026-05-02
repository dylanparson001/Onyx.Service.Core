using Onyx.Service.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Onyx.Service.Domain.Models
{
    public class Job
    {
        public long Id { get; set; }
        public Guid JobGuid { get; set; }
        public long TechnicianId { get; set; }
        public long CustomerId { get; set; }
        public DateTime ScheduledStartTime { get; set; }
        public DateTime ScheduledEndTime { get; set; }
        public DateTime? ActualStartTime { get; set; }
        public DateTime? ActualEndTime { get; set; }
        public bool? IsCompleted { get; set; }
        public string JobDescription { get; set; } = "";
        public JobStatus Status { get; set; }
        public DateTime ServiceDate { get; set; }

        #region Conversion Methods
  
        #endregion
    }
}
