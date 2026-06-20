using Onyx.Service.Infrastructure.DataAccess.DbModels.Contacts.Customers;
using Onyx.Service.Infrastructure.DataAccess.DbModels.Contacts.Employees;
using Onyx.Service.Infrastructure.DataAccess.DbModels.Jobs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Onyx.Service.Infrastructure.DataAccess.Constants
{
    internal class EmployeeDbConstants
    {
        internal static string CreateEmployeeQuery(EmployeeDb employee)
        {
            return $"INSERT INTO Employees (FirstName, LastName, Address, PhoneNumber, City, State, ZipCode, Email, HireDate, Access, Role)" +
                $"VALUES ('{employee.FirstName}', '{employee.LastName}', '{employee.Address}'," +
                $" '{employee.PhoneNumber}', '{employee.City}', '{employee.State}', '{employee.ZipCode}', '{employee.Email}'," +
                $" DATEADD(month, -6, GETDATE()), '{employee.Access}', '{employee.Access}');";
        }

        internal static string CreateCustomerQuery(CustomerDb employee)
        {
            return $"INSERT INTO Customers (FirstName, LastName, Address, PhoneNumber, City, State, ZipCode, Email)" +
                $"VALUES ('{employee.FirstName}', '{employee.LastName}', '{employee.Address}'," +
                $" '{employee.PhoneNumber}', '{employee.City}', '{employee.State}', '{employee.ZipCode}', '{employee.Email}')";
        }

        internal static string GetActiveTechniciansByDateQuery(DateTime date)
        {
            return @$"
                    SELECT employee.Id,
			                schedule.ScheduledDates,
			                employee.FirstName,
			                employee.LastName,
			                employee.Address,
			                employee.PhoneNumber,
			                employee.City,
			                employee.State,
			                employee.ZipCode,
			                employee.Email,
			                employee.HireDate,
			                employee.TerminationDate,
			                employee.Access,
			                employee.Role,
			                employee.Username
		                FROM Employees employee JOIN EmployeeSchedules schedule
		                ON (employee.Id = schedule.EmployeeId)
		                WHERE schedule.ScheduledDates IS NOT NULL
		                AND schedule.ScheduledDates LIKE  '%{date:d}%'";
        }

        // TODO: Move to Job Db Constants
        internal static string CreateJobsQuery(JobDb job)
        {
            return $"INSERT INTO Jobs (JobGuid, TechnicianId, CustomerId, ScheduledStartTime, ScheduledEndTime, IsCompleted, JobDescription, Status, ServiceDate) " +
                $"VALUES ('{job.JobGuid}', {job.TechnicianId}, {job.CustomerId}, '{job.ScheduledStartTime}', '{job.ScheduledEndTime}', '{job.IsCompleted}', '{job.JobDescription}', '{job.Status}', '{job.ServiceDate}');";
        }
    }
}
