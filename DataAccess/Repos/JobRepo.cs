using Microsoft.Data.SqlClient;
using onyx_services_core.Common.Enums;
using onyx_services_core.DataAccess.DbModels.Jobs;
using onyx_services_core.DataAccess.Helpers;
using onyx_services_core.DataAccess.Interfaces;
using System.Data;

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

        public async Task<List<JobDb>> GetJobsByTechnicianIdAndDate(long technicianId, DateTime serviceDate)
        {
            try
            {
                List<JobDb> jobs = [];

                var connectionString = ConfigHelper.GetDefaultConnection();

                using var connection = new SqlConnection(connectionString);

                string query = $"SELECT Id, JobGuid, TechnicianId, CustomerId, ScheduledStartTime, ScheduledEndTime," +
                               $"ActualStartTime, ActualEndTime, IsCompleted, JobDescription, Status, RemovedAt, RemovedReason, " +
                               $"ServiceDate " +
                               $"FROM Jobs " +
                               $"WHERE TechnicianId = @TechnicianId AND ServiceDate = @ServiceDate AND RemovedAt IS NULL";

                await connection.OpenAsync();

                using var command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@TechnicianId", technicianId);
                command.Parameters.AddWithValue("@ServiceDate", serviceDate);


                await using var reader = await command.ExecuteReaderAsync();

                if (reader.HasRows)
                {
                    while (await reader.ReadAsync())
                    {
                        var id = (long)reader["Id"];
                        var jobGuid = (Guid)reader["JobGuid"];
                        var techId = (long)reader["TechnicianId"];
                        var customerId = (long)reader["CustomerId"];

                        var scheduledStartTime = (DateTime)reader["ScheduledStartTime"];
                        var scheduledEndTime = (DateTime)reader["ScheduledEndTime"];
                        var dateOfService = (DateTime)reader["ServiceDate"];

                        var isCompleted = (bool)reader["IsCompleted"];

                        var jobDescription = reader["JobDescription"].ToString();

                        var status = Enum.Parse<JobStatus>(reader["Status"].ToString());

                        DateTime actualStartTime = new();
                        if (!string.IsNullOrEmpty(reader["ActualStartTime"].ToString()))
                            actualStartTime = (DateTime)reader["ActualStartTime"];

                        DateTime actualEndTime = new();
                        if (!string.IsNullOrEmpty(reader["ActualStartTime"].ToString()))
                            actualStartTime = (DateTime)reader["ActualStartTime"];

                        JobDb jobToAdd = new()
                        {
                            Id = id,
                            JobGuid = jobGuid,
                            TechnicianId = techId,
                            CustomerId = customerId,
                            ScheduledStartTime = scheduledStartTime,
                            ScheduledEndTime = scheduledEndTime,
                            ActualStartTime = actualStartTime == DateTime.MinValue ? null : actualStartTime,
                            ActualEndTime = actualEndTime == DateTime.MinValue ? null : actualEndTime,
                            IsCompleted = isCompleted,
                            JobDescription = jobDescription ??= "",
                            Status = status,
                            ServiceDate = dateOfService,
                        };

                        jobs.Add(jobToAdd);
                    }
                }

                await connection.CloseAsync();

                return jobs;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task RemoveJob(long id, string removalReason)
        {
            try
            {
                string connectionString = ConfigHelper.GetDefaultConnection();

                using var sqlConnection = new SqlConnection(connectionString);

                DateTime removedAt = DateTime.Now;

                string query = "UPDATE Jobs " +
                    "SET RemovedAt = @RemovedAtDate, RemovedReason = @RemovedReason" +
                    "WHERE Id = @Id";

                using var command = new SqlCommand(query, sqlConnection);

                command.Parameters.AddWithValue("@RemovedAtDate", removedAt);
                command.Parameters.AddWithValue("@RemovedReason", removalReason);

                await sqlConnection.OpenAsync();

                await command.ExecuteNonQueryAsync();

                await sqlConnection.CloseAsync();
            }
            catch (Exception ex)
            {

            }
        }

        public async Task UpdateJobDescription(long id, string newDescription)
        {
            try
            {
                string connectionString = ConfigHelper.GetDefaultConnection();

                using var sqlConnection = new SqlConnection(connectionString);

                string query = "UPDATE Jobs " +
                    "SET JobDescription = @NewDescription" +
                    "WHERE Id = @Id";

                using var command = new SqlCommand(query, sqlConnection);

                command.Parameters.AddWithValue("@NewDescription", newDescription);
                command.Parameters.AddWithValue("@Id", id);

                await sqlConnection.OpenAsync();

                await command.ExecuteNonQueryAsync();

                await sqlConnection.CloseAsync();
            }
            catch (Exception ex)
            {

            }
        }
    }
}
