using Onyx.Service.Infrastructure.DataAccess.DbModels.Contacts.Employees;
using System;
using System.Collections.Generic;
using System.Text;

namespace Onyx.Service.Infrastructure.DataAccess.Interfaces
{
    public interface IUserRepo
    {
        Task<List<EmployeeDb>> GetActiveTechnicians();
        Task CreateEmployee(EmployeeDb employeeDb);
        Task<List<EmployeeDb>> GetOfficeStaff();
    }
}
