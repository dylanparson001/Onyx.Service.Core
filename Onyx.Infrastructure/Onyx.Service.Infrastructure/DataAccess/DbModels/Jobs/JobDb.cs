using Onyx.Service.Contracts.Dtos.Jobs;
using Onyx.Service.Contracts.Enums;

namespace Onyx.Service.Infrastructure.DataAccess.DbModels.Jobs
{
    public class JobDb 
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
        public DateTime? RemovedAt { get; set; }
        public string? RemovedReason { get; set; } = "";
        public DateTime ServiceDate { get; set; }

        public JobsDto ToDto()
        {
            return new JobsDto()
            {
                Id = Id,
                JobGuid = JobGuid,
                TechnicianId = TechnicianId,
                CustomerId = CustomerId,
                ScheduledEndTime = ScheduledEndTime,
                ScheduledStartTime = ScheduledStartTime,
                ActualEndTime = ActualEndTime,
                ActualStartTime = ActualStartTime,
                IsCompleted = IsCompleted,
                JobDescription = JobDescription,
                ServiceDate = ServiceDate,
                Status = Status,
            };
        }
    }
}
