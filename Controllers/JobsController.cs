using Microsoft.AspNetCore.Mvc;
using onyx_services_core.DataAccess.DbModels.Jobs;
using onyx_services_core.DataAccess.Interfaces;
using onyx_services_core.Dtos.Jobs;
using onyx_services_core.Managers;

namespace onyx_services_core.Controllers
{
    [Route("[controller]")]
    public class JobsController : BaseController
    {
        private JobsManager _jobsManager { get; set; }
        public JobsController(ILogger<JobsController> logger, JobsManager jobsManager) : base(logger)
        {
            _jobsManager = jobsManager;
        }

        [HttpGet]
        [Route("get-active-jobs")]
        public async Task<ActionResult<List<JobsDto>>> GetJobsForTechnicianForServiceDate(long id, string serviceDate)
        {
            try
            {
                if (id <= 0)
                    return BadRequest("Enter a valid Id");

                if (string.IsNullOrEmpty(serviceDate))
                    return BadRequest("Service date was empty");

                bool isValidDate = DateTime.TryParse(serviceDate, out DateTime dateTimeService);

                if (!isValidDate)
                    return BadRequest("Enter a Valid Date");

                var jobDtos = await _jobsManager.GetActiveJobsByTechnicianIdAndServiceDate(id, dateTimeService);

                return Ok(jobDtos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest("Error retrieving jobs");
            }
        }

        [HttpPost]
        [Route("remove-job")]
        public async Task<ActionResult> RemoveJob(long id, string removalReason)
        {
            try
            {
                await _jobsManager.RemoveJob(id, removalReason);
                return NoContent();

            }
            catch (Exception ex)
            {
                return BadRequest("Error removing job");
            }
        }
    }
}
