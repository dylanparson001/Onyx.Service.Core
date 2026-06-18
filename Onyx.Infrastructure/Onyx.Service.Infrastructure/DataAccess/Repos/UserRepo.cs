using Microsoft.Data.SqlClient;
using Onyx.Service.Domain.Auth;
using Onyx.Service.Infrastructure.DataAccess.Constants;
using Onyx.Service.Infrastructure.DataAccess.DbModels.Contacts.Employees;
using Onyx.Service.Infrastructure.DataAccess.Enums.ColumnEnums;
using Onyx.Service.Infrastructure.DataAccess.Extensions;
using Onyx.Service.Infrastructure.DataAccess.Helpers;
using Onyx.Service.Infrastructure.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Onyx.Service.Infrastructure.DataAccess.Repos
{
    public class UserRepo : IUserRepo
    {
        public async Task CreateEmployee(EmployeeDb employeeDb)
        {
            try
            {
                using var sqlConnection = new SqlConnection(ConfigHelper.GetDefaultConnection());

                await sqlConnection.OpenAsync();

                string query = EmployeeDbConstants.CreateEmployeeQuery(employeeDb);

                using var sqlCommand = new SqlCommand(query, sqlConnection);

                await sqlCommand.ExecuteNonQueryAsync();

                await sqlConnection.CloseAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<EmployeeDb>> GetActiveTechnicians()
        {
            List<EmployeeDb> activeTechnicians = [];

            try
            {
                using var sqlConnection = new SqlConnection(ConfigHelper.GetDefaultConnection());


                await sqlConnection.OpenAsync();

                var sqlCommand = new SqlCommand(EmployeeDbConstants.GetActiveTechniciansQuery, sqlConnection);

                await using var reader = await sqlCommand.ExecuteReaderAsync();

                if (reader.HasRows)
                {
                    while (await reader.ReadAsync())
                    {
                        long id = reader.Get<long, EmployeeColumns>(EmployeeColumns.Id);
                        string firstName = reader.Get<string, EmployeeColumns>(EmployeeColumns.FirstName)!;
                        string lastName = reader.Get<string, EmployeeColumns>(EmployeeColumns.LastName)!;
                        string address = reader.Get<string, EmployeeColumns>(EmployeeColumns.Address)!;
                        string phone = reader.Get<string, EmployeeColumns>(EmployeeColumns.PhoneNumber)!;
                        string city = reader.Get<string, EmployeeColumns>(EmployeeColumns.City)!;
                        string state = reader.Get<string, EmployeeColumns>(EmployeeColumns.State)!;
                        string zip = reader.Get<string, EmployeeColumns>(EmployeeColumns.ZipCode)!;
                        string email = reader.Get<string, EmployeeColumns>(EmployeeColumns.Email)!;
                        string username = reader.Get<string, EmployeeColumns>(EmployeeColumns.Username)!;
                        DateTime hiredDate = reader.Get<DateTime, EmployeeColumns>(EmployeeColumns.HireDate);
                        DateTime? terminationDate = reader.Get<DateTime, EmployeeColumns>(EmployeeColumns.TerminationDate);
                        string accessLevel = reader.Get<string, EmployeeColumns>(EmployeeColumns.Access);

                        AccessLevel level = Enum.Parse<AccessLevel>(accessLevel);

                        activeTechnicians.Add(new EmployeeDb()
                        {
                            Id = id,
                            FirstName = firstName,
                            LastName = lastName,
                            Address = address,
                            PhoneNumber = phone,
                            City = city,
                            State = state,
                            ZipCode = zip,
                            Email = email,
                            Username = username,
                            HireDate = hiredDate,
                            TerminationDate = terminationDate,
                            Access = level
                        });
                    }
                }

                await sqlConnection.CloseAsync();
            }
            catch (Exception ex)
            {
                throw;
            }

            return activeTechnicians;
        }

        public Task<List<EmployeeDb>> GetOfficeStaff()
        {
            throw new NotImplementedException();
        }
    }
}
