using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using Onyx.Service.Api.Controllers;
using Onyx.Service.Application.Managers;
using Onyx.Service.Infrastructure.DataAccess.DbModels.Contacts.Employees;
using Onyx.Shared.Contracts.Users;
using Assert = NUnit.Framework.Assert;

namespace Onyx.Service.Api.Tests
{
    public class UserControllerTests
    {
        ILogger<UserController> mockLogger = A.Fake<ILogger<UserController>>();

        // Uses actual UserManager, will mock in future currently using real data
        // Also Auth is a bit of an issue, need to figure out way around that, or
        // just stick to testinng logic in Managers
        [Test]
        public async Task UserController_GetActiveTechnicians()
        {
            var repoReturnData = new List<EmployeeDb>()
            {
                new() {},
                new() {},
                new() {},

            };
            var mockUserManager = A.Fake<UserManager>();


            var userController = new UserController(mockUserManager, mockLogger);


            ActionResult<List<EmployeeDto>> response = await userController.GetAciveTechnicians(DateTime.Parse("7/13/2026"));

            Assert.That(response, Is.Not.Null);
        }
    }
}
