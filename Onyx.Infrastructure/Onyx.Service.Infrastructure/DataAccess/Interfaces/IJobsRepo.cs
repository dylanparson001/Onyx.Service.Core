using Onyx.Service.Infrastructure.DataAccess.DbModels.Jobs;
using Onyx.Shared.Enums;

namespace Onyx.Service.Infrastructure.DataAccess.Interfaces
{
    public interface IJobsRepo
    {
        Task CreateJob(JobDb job);
        Task CancelJob(long id, CancellationReason removalReason);
        Task UpdateJobDescription(long id, string newDescription);
        Task<List<JobDb>> GetJobsByTechnicianIdAndDate(long  technicianId, DateTime serviceDate);
    }
}
