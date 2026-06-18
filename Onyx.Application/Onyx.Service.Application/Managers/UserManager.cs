using Microsoft.Extensions.Logging;
using Onyx.Service.Contracts.Dtos.Users;
using Onyx.Service.Infrastructure.DataAccess.DbModels.Contacts.Employees;
using Onyx.Service.Infrastructure.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Onyx.Service.Application.Managers
{
    public class UserManager
    {
        #region Private Fields
        private IUserRepo _userRepo { get; }
        private ILogger<UserManager> _logger { get; }
        #endregion

        #region Constructor
        public UserManager(IUserRepo userRepo, ILogger<UserManager> logger)
        {
            _userRepo = userRepo;
            _logger = logger;
        }
        #endregion

        #region Public Methods
        public async Task<List<EmployeeDto>> GetActiveTechnicians()
        {
            List<EmployeeDto> activeTechnicians = [];

            try
            {
                List<EmployeeDb> employeeDbs = await _userRepo.GetActiveTechnicians();

                activeTechnicians = employeeDbs.Select(x => x.ToDto()).ToList();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return activeTechnicians;
        }

        #endregion


        #region Private Methods

        #endregion

    }
}
