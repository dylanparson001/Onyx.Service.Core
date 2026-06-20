using Microsoft.Extensions.Logging;
using Onyx.Service.Infrastructure.DataAccess.DbModels.Contacts.Employees;
using Onyx.Service.Infrastructure.DataAccess.Interfaces;
using Onyx.Shared.Contracts.Users;

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
        public async Task<List<EmployeeDto>> GetActiveTechniciansByDate(DateTime date)
        {

            List<EmployeeDto> activeTechnicians = [];

            if (date == DateTime.MinValue)
                return activeTechnicians;
            try
            {
                List<EmployeeDb> employeeDbs = await _userRepo.GetActiveTechniciansByDate(date);

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
