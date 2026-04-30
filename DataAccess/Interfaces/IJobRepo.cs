using onyx_services_core.DataAccess.DbModels.Jobs;

namespace onyx_services_core.DataAccess.Interfaces
{
    public interface IJobRepo
    {
        Task CreateJob(JobDb job);
        Task RemoveJob(long id, string removalReason);
        Task UpdateJobDescription(long id, string newDescription);
        Task<List<JobDb>> GetJobsByTechnicianIdAndDate(long  technicianId, DateTime serviceDate);
    }
}
