using Castle.Core.Logging;
using FakeItEasy;
using Microsoft.Extensions.Logging;
using Onyx.Service.Application.Constants;
using Onyx.Service.Application.Managers;
using Onyx.Service.Domain.Enums;
using Onyx.Service.Domain.Models;
using Onyx.Service.Infrastructure.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Onyx.Service.Application.Tests.JobsManagerTests
{
    [TestClass]
    public class JobsManagerTests
    {
        private static IJobsRepo? _mockJobsRepo;
        private static ILogger<JobsManager>? _mockLogger;

        [ClassInitialize]
        public static void SetupTests(TestContext testContext)
        {
            _mockJobsRepo = A.Fake<IJobsRepo>();
            _mockLogger = A.Fake<ILogger<JobsManager>>();
        }

        [TestMethod]
        public async Task CreateJob_StartTimeIsAfterEndDate_ReturnsValidationError()
        {
            JobsManager jobsManager = new(_mockJobsRepo!, _mockLogger!);

            var response = await jobsManager.CreateJob(
                new Job()
                {
                    JobGuid = Guid.NewGuid(),
                    TechnicianId = 1,
                    CustomerId = 2,
                    ScheduledStartTime = DateTime.Now.AddHours(2),
                    ScheduledEndTime = DateTime.Now.AddHours(1),
                    JobDescription = "Test Job",
                    Status = JobStatus.Scheduled,
                    ServiceDate = DateTime.Today
                });

            Assert.IsFalse(response.IsSuccess);
            Assert.IsNotEmpty(response.ErrorMessage!);
            Assert.AreEqual(response.ErrorMessage, JobExceptionConstants.StartTimeGreaterError);

        }

        [TestMethod]
        public async Task CreateJob_EmptyDescription_ReturnsValidationError()
        {
            JobsManager jobsManager = new(_mockJobsRepo!, _mockLogger!);

            var response = await jobsManager.CreateJob(
                new Job()
                {
                    JobGuid = Guid.NewGuid(),
                    TechnicianId = 1,
                    CustomerId = 2,
                    ScheduledStartTime = DateTime.Now,
                    ScheduledEndTime = DateTime.Now.AddHours(2),
                    JobDescription = "",
                    Status = JobStatus.Scheduled,
                    ServiceDate = DateTime.Today
                });

            Assert.IsFalse(response.IsSuccess);
            Assert.IsNotEmpty(response.ErrorMessage!);
            Assert.AreEqual(response.ErrorMessage, JobExceptionConstants.JobDescriptionEmptyError);
        }

    }
}
