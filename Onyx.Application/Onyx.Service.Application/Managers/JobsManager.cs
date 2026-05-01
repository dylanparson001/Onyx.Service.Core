using Microsoft.Extensions.Logging;
using Onyx.Service.Contracts.Dtos.Jobs;
using Onyx.Service.Infrastructure.DataAccess.DbModels.Jobs;
using Onyx.Service.Infrastructure.DataAccess.Interfaces;

namespace Onyx.Service.Application.Managers
{
    public class JobsManager
    {
        #region Private Properties
        private IJobRepo _jobsRepo { get; }
        private ILogger _logger { get; }
        #endregion

        #region Constructor
        public JobsManager(IJobRepo jobsRepo, ILogger<JobsManager> logger)
        {
            _jobsRepo = jobsRepo;
            _logger = logger;
        }
        #endregion

        #region Public Properties

        #endregion


        #region Public Methods
        public async Task<List<JobsDto>> GetActiveJobsByTechnicianIdAndServiceDate(long id, DateTime serviceDate)
        {
            try
            {
                List<JobDb> result = await _jobsRepo.GetJobsByTechnicianIdAndDate(id, serviceDate);

                List<JobsDto> jobDtos = [];

                foreach (var jobDb in result)
                {
                    jobDtos.Add(jobDb.ToDto());
                }

                return jobDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError($"JobsManager GetActiveJobsByTechnicianIdAndServiceDate Error: {ex.Message}");
                throw;
            }
        }

        public async Task RemoveJob(long id, string removalReason)
        {
            try
            {
                if (id <= 0)
                    return;

                if (string.IsNullOrEmpty(removalReason))
                    return;

                await _jobsRepo.RemoveJob(id, removalReason);
            }
            catch (Exception ex)
            {
                _logger.LogError($"JobsManager RemoveJob Error: {ex.Message}");
            }
        }
        #endregion

        #region Private Methods

        #endregion
    }
}
