using Microsoft.Extensions.Logging;
using Onyx.Service.Application.Constants;
using Onyx.Service.Infrastructure.DataAccess.DbModels.Jobs;
using Onyx.Service.Infrastructure.DataAccess.Interfaces;
using Onyx.Shared.Contracts.Jobs;
using Onyx.Shared.Contracts.Responses;
using Onyx.Shared.Enums;

namespace Onyx.Service.Application.Managers
{
    public class JobsManager(IJobsRepo jobsRepo, ILogger<JobsManager> logger)
    {
        #region Private Properties
        private IJobsRepo _jobsRepo { get; } = jobsRepo;
        private ILogger _logger { get; } = logger;

        #endregion
        #region Constructor
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
        public async Task<NewJobResponse> CreateJob(Job job)
        {
            try
            {
                if (job == null)
                    return new NewJobResponse(JobExceptionMessageConstants.NullJobError);

                if (job.ScheduledStartTime >= job.ScheduledEndTime)
                    return new NewJobResponse(JobExceptionMessageConstants.StartTimeGreaterError);

                if (string.IsNullOrEmpty(job.JobDescription))
                    return new NewJobResponse(JobExceptionMessageConstants.JobDescriptionEmptyError);

                if (job.Status != JobStatus.Scheduled && job.Status != JobStatus.Pending)
                    throw new Exception(JobExceptionMessageConstants.JobStatusShouldBeScheduledOrPending);

                await _jobsRepo.CreateJob(JobDb.ConvertFromJobModel(job));

                return new NewJobResponse();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "JobsManager Create Job Error");

                return new NewJobResponse(ex.Message);
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
                    throw new Exception(JobExceptionMessageConstants.IdInvalid);

                if (string.IsNullOrEmpty(serviceDate))
                    throw new Exception(JobExceptionMessageConstants.ServiceDateWasEmpty);

                bool isValidDate = DateTime.TryParse(serviceDate, out DateTime dateTimeService);

                if (!isValidDate)
                    throw new Exception(JobExceptionMessageConstants.ServiceDateWasInvalid);

                List<JobDb> result = await _jobsRepo.GetJobsByTechnicianIdAndDate(id, dateTimeService);

                return result.Select(j => j.ToJobDto()).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "JobsManager GetActiveJobsByTechnicianIdAndServiceDate Error");
                throw;
            }
        }


        public async Task CancelJob(long id, CancellationReason removalReason)
        {
            try
            {
                if (id <= 0)
                    throw new Exception("Id must be greater than zero");

                await _jobsRepo.CancelJob(id, removalReason);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "JobsManager RemoveJob Error");
            }
        }

        public async Task<NewJobResponse> CreateJob(object value)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Private Methods

        #endregion
    }
}
