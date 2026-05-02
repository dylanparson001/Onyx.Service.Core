using Microsoft.Extensions.Logging;
using Onyx.Service.Contracts.Dtos.Jobs;
using Onyx.Service.Domain.Enums;
using Onyx.Service.Domain.Models;
using Onyx.Service.Infrastructure.DataAccess.DbModels.Jobs;
using Onyx.Service.Infrastructure.DataAccess.Interfaces;

namespace Onyx.Service.Application.Managers
{
    public class JobsManager
    {
        #region Private Properties
        private IJobsRepo _jobsRepo { get; }
        private ILogger _logger { get; }
        #endregion

        #region Constructor
        public JobsManager(IJobsRepo jobsRepo, ILogger<JobsManager> logger)
        {
            _jobsRepo = jobsRepo;
            _logger = logger;
        }
        #endregion

        #region Public Properties

        #endregion


        #region Public Methods
        /// <summary>
        /// Creates a new job entry in the data store using the specified job details.
        /// </summary>
        /// <param name="job">The job information to create. The job must have a non-empty description, a scheduled start time earlier
        /// than the scheduled end time, and a status of either Scheduled or Pending.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public async Task CreateJob(Job job)
        {
            try
            {
                if (job == null)
                    throw new Exception("No job was sent");

                if (job.ScheduledStartTime >= job.ScheduledEndTime)
                    throw new Exception("Start time cannot be greater than end time");

                if (string.IsNullOrEmpty(job.JobDescription))
                    throw new Exception("Job Description cannot be empty");

                if (job.Status != JobStatus.Scheduled && job.Status != JobStatus.Pending)
                    throw new Exception("New job status should be either scheduled or pending");

                await _jobsRepo.CreateJob(JobDb.ConvertFromJobModel(job));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "JobsManager Create Job Error");
                throw;
            }

        }

        /// <summary>
        /// Retrieves a list of active jobs assigned to the specified technician on the given service date.
        /// </summary>
        /// <param name="id">The unique identifier of the technician. Must be greater than zero.</param>
        /// <param name="serviceDate">The service date to filter jobs by, in a format recognized by DateTime.Parse. Cannot be null or empty.</param>
        /// <returns>A list of NewJobDto objects representing the active jobs for the technician on the specified date. The list
        /// is empty if no jobs are found.</returns>
        public async Task<List<JobDto>> GetActiveJobsByTechnicianIdAndServiceDate(long id, string serviceDate)
        {
            try
            {
                if (id <= 0)
                    throw new Exception($"{id} is not valid");

                if (string.IsNullOrEmpty(serviceDate))
                    throw new Exception("Service date was empty");

                bool isValidDate = DateTime.TryParse(serviceDate, out DateTime dateTimeService);

                if (!isValidDate)
                    throw new Exception($"Service date was not valid");

                List<JobDb> result = await _jobsRepo.GetJobsByTechnicianIdAndDate(id, dateTimeService);

                return result.Select(j => j.ToJobDto()).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "JobsManager GetActiveJobsByTechnicianIdAndServiceDate Error");
                throw;
            }
        }


        public async Task CancelJob(long id, string removalReason)
        {
            try
            {
                if (id <= 0)
                    throw new Exception("Id must be greater than zero");

                if (string.IsNullOrEmpty(removalReason))
                    throw new Exception("Cancelling a job must have a reason described");

                await _jobsRepo.CancelJob(id, removalReason);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "JobsManager RemoveJob Error");
            }
        }
        #endregion

        #region Private Methods

        #endregion
    }
}
