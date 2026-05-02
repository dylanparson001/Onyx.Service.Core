using Onyx.Service.Domain.Enums;
using Onyx.Service.Domain.Models;

namespace Onyx.Service.Contracts.Dtos.Jobs
{
    public class NewJobRequest
    {
        public Guid JobGuid { get; set; }
        public long TechnicianId { get; set; }
        public long CustomerId { get; set; }
        public DateTime ScheduledStartTime { get; set; }
        public DateTime ScheduledEndTime { get; set; }
        public string JobDescription { get; set; } = "";
        public string Status { get; set; } = "";
        public DateTime ServiceDate { get; set; }

        public Job ToJob()
        {
            return new Job()
            {
                JobGuid = JobGuid,
                TechnicianId = TechnicianId,
                CustomerId = CustomerId,
                ScheduledStartTime = ScheduledStartTime,
                ScheduledEndTime = ScheduledEndTime,
                IsCompleted = false,
                JobDescription = JobDescription,
                Status = string.IsNullOrEmpty(Status) ? Enum.Parse<JobStatus>(Status) : JobStatus.Scheduled,
                ServiceDate = ServiceDate
            };
        }
    }
}
