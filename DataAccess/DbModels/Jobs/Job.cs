using onyx_services_core.Common.Enums;

namespace onyx_services_core.DataAccess.DbModels.Jobs
{
    public class Job : BaseJob
    {
     
        public bool IsCompleted { get; set; }
        public DateTime CancelledDate { get; set; }
        public string JobDescription { get; set; } = "";
        public JobStatus Status { get; set; }

    }
}
