using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Onyx.Service.Application.Managers;
using Onyx.Service.Contracts.Dtos.Jobs;
using Onyx.Service.Contracts.Responses;
using Onyx.Service.Domain.Enums;

namespace Onyx.Service.Api.Controllers
{
    [Route("[controller]")]
    public class JobsController : BaseController
    {
        private JobsManager _jobsManager { get; set; }
        public JobsController(ILogger<JobsController> logger, JobsManager jobsManager) : base(logger)
        {
            _jobsManager = jobsManager;
        }
        [Authorize]
        [HttpGet("get-active-jobs")]
        public async Task<ActionResult<List<JobDto>>> GetJobsForTechnicianForServiceDate(long id, string serviceDate)
        {
            try
            {
                var jobDtos = await _jobsManager.GetActiveJobsByTechnicianIdAndServiceDate(id, serviceDate);

                return Ok(jobDtos);
            }
            catch (Exception ex)    
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest($"Error retrieving jobs: {ex.Message}");
            }
        }
        [Authorize(Roles = "Office, Manager, Admin")]
        [HttpPost("create-job")]
        public async Task<ActionResult<NewJobResponse>> CreateNewJob(NewJobRequest jobDto)
        {
            try
            {
                if (jobDto == null)
                    return BadRequest("Job sent was null");

                NewJobResponse newJobResponse = await _jobsManager.CreateJob(jobDto.ToJob());

                return newJobResponse.IsSuccess ? Ok(newJobResponse) : BadRequest(newJobResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(new NewJobResponse(ex.Message));
            }
        }

        [Authorize(Roles = "Office, Manager, Admin")]
        [HttpPost("cancel-job")]
        public async Task<ActionResult> CancelJob(long id, CancellationReason removalReason)
        {
            try
            {
                await _jobsManager.CancelJob(id, removalReason);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest($"Error removing job: {ex.Message}");
            }
        }
    }
}
