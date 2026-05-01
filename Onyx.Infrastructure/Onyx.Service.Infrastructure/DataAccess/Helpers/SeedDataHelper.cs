using Microsoft.Data.SqlClient;
using Onyx.Service.Contracts.Enums;
using Onyx.Service.Domain.Auth;
using Onyx.Service.Infrastructure.DataAccess.DbModels.Contacts.Customers;
using Onyx.Service.Infrastructure.DataAccess.DbModels.Contacts.Employees;
using Onyx.Service.Infrastructure.DataAccess.DbModels.Jobs;

namespace Onyx.Service.Infrastructure.DataAccess.Helpers
{
    public static class SeedDataHelper
    {
        /// <summary>
        /// Use to create test customers and technicians
        /// </summary>
        /// <returns></returns>
        public static async Task CreateTestProfiles()
        {
            try
            {
                var _connectionString = ConfigHelper.GetDefaultConnection();

                using var connection = new SqlConnection(_connectionString);

                var testEmployess = CreateEmployeeModels();

                await connection.OpenAsync();

                //Console.WriteLine("Starting to create employees...");

                //foreach (var employee in testEmployess)
                //{
                //    string query = CreateTestEmployeeQuery(employee);


                //    using var command = new SqlCommand(query, connection);

                //    await command.ExecuteNonQueryAsync();

                //}
                //Console.WriteLine("Starting to create customers...");

                //foreach (var customer in CreateCustomerModels())
                //{
                //    string query = CreateTestCustomerQuery(customer);

                //    using var command = new SqlCommand(query, connection);

                //    await command.ExecuteNonQueryAsync();
                //}

                Console.WriteLine("Starting to create jobs...");

                foreach (var job in CreateJobModels())
                {
                    string query = CreateTestJobsQuery(job);
                    using var command = new SqlCommand(query, connection);
                    await command.ExecuteNonQueryAsync();
                }

                await connection.CloseAsync();

            }
            catch (Exception ex)
            {

            }
        }

        private static List<EmployeeDb> CreateEmployeeModels()
        {
            return
            [
                new EmployeeDb() {FirstName = "John", LastName = "Test", Address = "Doessnt matter", PhoneNumber="1", City = "xx", State = "lfe", ZipCode="123", Email="2er2qr2", Access = AccessLevel.Technician},
                new EmployeeDb() {FirstName = "Jacob", LastName = "Test", Address = "Doessnt matter", PhoneNumber="1", City = "xx", State = "lfe", ZipCode="123", Email="2er2qr2", Access = AccessLevel.Technician},
                new EmployeeDb() {FirstName = "Jingle", LastName = "Test", Address = "Doessnt matter", PhoneNumber="1", City = "xx", State = "lfe", ZipCode="123", Email="2er2qr2", Access = AccessLevel.Technician},
                new EmployeeDb() {FirstName = "Heimer", LastName = "Test", Address = "Doessnt matter", PhoneNumber="1", City = "xx", State = "lfe", ZipCode="123", Email="2er2qr2", Access = AccessLevel.Technician},
                new EmployeeDb() {FirstName = "Schmidt", LastName = "Test", Address = "Doessnt matter", PhoneNumber="1", City = "xx", State = "lfe", ZipCode="123", Email="2er2qr2", Access = AccessLevel.Technician},
            ];
        }

        private static List<CustomerDb> CreateCustomerModels()
        {
            return
            [
                new CustomerDb() {FirstName = "John", LastName = "Test", Address = "Doessnt matter", PhoneNumber="1", City = "xx", State = "lfe", ZipCode="123", Email="2er2qr2"},
                new CustomerDb() {FirstName = "Jacob", LastName = "Test", Address = "Doessnt matter", PhoneNumber="1", City = "xx", State = "lfe", ZipCode="123", Email="2er2qr2",},
                new CustomerDb() {FirstName = "Jingle", LastName = "Test", Address = "Doessnt matter", PhoneNumber="1", City = "xx", State = "lfe", ZipCode="123", Email="2er2qr2", },
                new CustomerDb() {FirstName = "Heimer", LastName = "Test", Address = "Doessnt matter", PhoneNumber="1", City = "xx", State = "lfe", ZipCode="123", Email="2er2qr2", },
                new CustomerDb() {FirstName = "Schmidt", LastName = "Test", Address = "Doessnt matter", PhoneNumber="1", City = "xx", State = "lfe", ZipCode="123", Email="2er2qr2"},
            ];
        }

        private static List<JobDb> CreateJobModels()
        {
            return [
                new JobDb() {JobGuid = Guid.NewGuid(), TechnicianId = 1, CustomerId = 1, ScheduledStartTime = DateTime.Now.AddHours(-2), ScheduledEndTime = DateTime.Now, IsCompleted = false, JobDescription = "This is a test job", Status = JobStatus.Open, ServiceDate = DateTime.Today },
                new JobDb() {JobGuid = Guid.NewGuid(), TechnicianId = 2, CustomerId = 2, ScheduledStartTime = DateTime.Now.AddHours(-2), ScheduledEndTime = DateTime.Now, IsCompleted = false, JobDescription = "This is a test job", Status = JobStatus.Open, ServiceDate = DateTime.Today },
                new JobDb() {JobGuid = Guid.NewGuid(), TechnicianId = 3, CustomerId = 3, ScheduledStartTime = DateTime.Now.AddHours(-2), ScheduledEndTime = DateTime.Now, IsCompleted = false, JobDescription = "This is a test job", Status = JobStatus.Open, ServiceDate = DateTime.Today },
                new JobDb() {JobGuid = Guid.NewGuid(), TechnicianId = 4, CustomerId = 4, ScheduledStartTime = DateTime.Now.AddHours(-2), ScheduledEndTime = DateTime.Now, IsCompleted = false, JobDescription = "This is a test job", Status = JobStatus.Open, ServiceDate = DateTime.Today },
                ];
        }

        private static string CreateTestEmployeeQuery(EmployeeDb employee)
        {
            return $"INSERT INTO Employees (FirstName, LastName, Address, PhoneNumber, City, State, ZipCode, Email, HireDate, Access, Role)" +
                $"VALUES ('{employee.FirstName}', '{employee.LastName}', '{employee.Address}'," +
                $" '{employee.PhoneNumber}', '{employee.City}', '{employee.State}', '{employee.ZipCode}', '{employee.Email}'," +
                $" DATEADD(month, -6, GETDATE()), '{employee.Access}', '{employee.Access}');";
        }

        private static string CreateTestCustomerQuery(CustomerDb employee)
        {
            return $"INSERT INTO Customers (FirstName, LastName, Address, PhoneNumber, City, State, ZipCode, Email)" +
                $"VALUES ('{employee.FirstName}', '{employee.LastName}', '{employee.Address}'," +
                $" '{employee.PhoneNumber}', '{employee.City}', '{employee.State}', '{employee.ZipCode}', '{employee.Email}')";
        }
        private static string CreateTestJobsQuery(JobDb job)
        {
            return $"INSERT INTO Jobs (JobGuid, TechnicianId, CustomerId, ScheduledStartTime, ScheduledEndTime, IsCompleted, JobDescription, Status, ServiceDate) " +
                $"VALUES ('{job.JobGuid}', {job.TechnicianId}, {job.CustomerId}, '{job.ScheduledStartTime}', '{job.ScheduledEndTime}', '{job.IsCompleted}', '{job.JobDescription}', '{job.Status}', '{job.ServiceDate}');";
        }
    }
}
