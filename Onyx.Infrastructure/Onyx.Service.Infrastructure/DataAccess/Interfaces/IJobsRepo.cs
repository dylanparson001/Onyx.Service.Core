using Onyx.Service.Domain.Enums;
using Onyx.Service.Infrastructure.DataAccess.DbModels.Jobs;

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
