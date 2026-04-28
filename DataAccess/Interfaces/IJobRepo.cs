using onyx_services_core.DataAccess.DbModels.Jobs;

namespace onyx_services_core.DataAccess.Interfaces
{
    public interface IJobRepo
    {
        Task CreateJob(JobDb job);
        Task RemoveJob(long id, string removalReason);
        Task UpdateJob(JobDb job);
    }
}
