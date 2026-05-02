using System;
using System.Collections.Generic;
using System.Text;

namespace Onyx.Service.Contracts.Dtos.Jobs
{
    public class JobDto
    {
        public long Id { get; set; }
        public Guid JobGuid { get; set; }
        public long TechnicianId { get; set; }
        public long CustomerId { get; set; }
        public DateTime ScheduledStartTime { get; set; }
        public DateTime ScheduledEndTime { get; set; }
        public string JobDescription { get; set; } = "";
        public string Status { get; set; } = "";
        public DateTime ServiceDate { get; set; }
    }
}
