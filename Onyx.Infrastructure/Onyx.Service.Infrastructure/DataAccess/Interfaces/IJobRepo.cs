using Onyx.Service.Infrastructure.DataAccess.DbModels.Jobs;

namespace Onyx.Service.Infrastructure.DataAccess.Interfaces
{
    public interface IJobRepo
    {
        Task CreateJob(JobDb job);
        Task RemoveJob(long id, string removalReason);
        Task UpdateJobDescription(long id, string newDescription);
        Task<List<JobDb>> GetJobsByTechnicianIdAndDate(long  technicianId, DateTime serviceDate);
    }
}
