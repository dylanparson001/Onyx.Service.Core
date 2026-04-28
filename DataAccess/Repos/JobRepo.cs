using Microsoft.Data.SqlClient;
using onyx_services_core.DataAccess.DbModels.Jobs;
using onyx_services_core.DataAccess.Helpers;
using onyx_services_core.DataAccess.Interfaces;

namespace onyx_services_core.DataAccess.Repos
{
    public class JobRepo : IJobRepo
    {
        public JobRepo()
        {

        }
        public async Task CreateJob(JobDb job)
        {
            if (job == null)
                return;

            try
            {
                var connectionString = ConfigHelper.GetDefaultConnection();

                using var sqlConnection = new SqlConnection(connectionString);

                await sqlConnection.OpenAsync();

                string query = $"INSERT INTO Jobs  (JobGuid, TechnicianId, CustomerId, ScheduledStartTime, ScheduledEndTime" +
                    $" JobDescription, Status, ServiceDate)" +
                    $"VALUES ('{job.JobGuid}', '{job.TechnicianId}', '{job.CustomerId}', '{job.ScheduledStartTime}', " +
                    $"'{job.ScheduledEndTime}', '{job.JobDescription}', '{job.Status}', '{job.ServiceDate}')";

                using var command = new SqlCommand(query, sqlConnection);

                await command.ExecuteNonQueryAsync();

                await sqlConnection.CloseAsync();
            }
            catch (Exception ex)
            {

            }

        }

        public Task RemoveJob(long id, string removalReason)
        {
            throw new NotImplementedException();
        }

        public Task UpdateJob(JobDb job)
        {
            throw new NotImplementedException();
        }
    }
}
