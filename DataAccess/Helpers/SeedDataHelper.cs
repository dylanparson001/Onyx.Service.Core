using Microsoft.Data.SqlClient;
using onyx_services_core.Common.Enums;
using onyx_services_core.DataAccess.DbModels.Contacts.Customers;
using onyx_services_core.DataAccess.DbModels.Contacts.Employees;
using System.Security.AccessControl;

namespace onyx_services_core.DataAccess.Helpers
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

                Console.WriteLine("Starting to create employees...");
                
                foreach (var employee in testEmployess)
                {
                    string query = CreateTestEmployeeQuery(employee);


                    using var command = new SqlCommand(query, connection);

                    await command.ExecuteNonQueryAsync();

                }
                Console.WriteLine("Starting to create customers...");

                foreach (var customer in CreateCustomerModels())
                {
                    string query = CreateTestCustomerQuery(customer);

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

        private static List<Customer> CreateCustomerModels()
        {
            return
            [
                new Customer() {FirstName = "John", LastName = "Test", Address = "Doessnt matter", PhoneNumber="1", City = "xx", State = "lfe", ZipCode="123", Email="2er2qr2"},
                new Customer() {FirstName = "Jacob", LastName = "Test", Address = "Doessnt matter", PhoneNumber="1", City = "xx", State = "lfe", ZipCode="123", Email="2er2qr2",},
                new Customer() {FirstName = "Jingle", LastName = "Test", Address = "Doessnt matter", PhoneNumber="1", City = "xx", State = "lfe", ZipCode="123", Email="2er2qr2", },
                new Customer() {FirstName = "Heimer", LastName = "Test", Address = "Doessnt matter", PhoneNumber="1", City = "xx", State = "lfe", ZipCode="123", Email="2er2qr2", },
                new Customer() {FirstName = "Schmidt", LastName = "Test", Address = "Doessnt matter", PhoneNumber="1", City = "xx", State = "lfe", ZipCode="123", Email="2er2qr2"},
            ];
        }

        private static string CreateTestEmployeeQuery(EmployeeDb employee)
        {
            return $"INSERT INTO Employees (FirstName, LastName, Address, PhoneNumber, City, State, ZipCode, Email, HireDate, Access, Role)" +
                $"VALUES ('{employee.FirstName}', '{employee.LastName}', '{employee.Address}'," +
                $" '{employee.PhoneNumber}', '{employee.City}', '{employee.State}', '{employee.ZipCode}', '{employee.Email}'," +
                $" DATEADD(month, -6, GETDATE()), '{employee.Access}', '{employee.Access}');";
        }

        private static string CreateTestCustomerQuery(Customer employee)
        {
            return $"INSERT INTO Customers (FirstName, LastName, Address, PhoneNumber, City, State, ZipCode, Email)" +
                $"VALUES ('{employee.FirstName}', '{employee.LastName}', '{employee.Address}'," +
                $" '{employee.PhoneNumber}', '{employee.City}', '{employee.State}', '{employee.ZipCode}', '{employee.Email}')";
        }
    }
}
