using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;
using Onyx.Service.Contracts.Dtos.Invoices;
using Onyx.Service.Domain.Models;
using Onyx.Service.Infrastructure.DataAccess.ColumnEnums;
using Onyx.Service.Infrastructure.DataAccess.DbModels.Contacts.Customers;
using Onyx.Service.Infrastructure.DataAccess.DbModels.Invoices;
using Onyx.Service.Infrastructure.DataAccess.DbModels.Jobs;
using Onyx.Service.Infrastructure.DataAccess.Enums;
using Onyx.Service.Infrastructure.DataAccess.Extensions;
using Onyx.Service.Infrastructure.DataAccess.Helpers;
using Onyx.Service.Infrastructure.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Onyx.Service.Infrastructure.DataAccess.Repos
{
    public class InvoicesRepo : IInvoicesRepo
    {
        public InvoicesRepo()
        {

        }
        public async Task CreateInvoice(CreateInvoiceDto newInvoice)
        {
            if (newInvoice == null)
                return;

            try
            {
                var connectionString = ConfigHelper.GetDefaultConnection();

                using var sqlConnection = new SqlConnection(connectionString);

                await sqlConnection.OpenAsync();

                string query = @"INSERT INTO Invoices(TechnicianId, CustomerId, JobId) 
                                    VALUES (@TechnicianId, @CustomerId, @JobId)";

                using var command = new SqlCommand(query, sqlConnection);

                command.Parameters.AddWithValue("@TechnicianId", newInvoice.TechnicianId);
                command.Parameters.AddWithValue("@CustomerId", newInvoice.CustomerId);
                command.Parameters.AddWithValue("@JobId", newInvoice.JobId);


                await command.ExecuteNonQueryAsync();

                await sqlConnection.CloseAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<InvoiceDb>> GetInvoicesByCustomer(long customerId, DateTime serviceDate)
        {
            try
            {
                return await GetInvoicesByGivenId(customerId, serviceDate, IdLookupInvoice.CustomerId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<InvoiceDb>> GetInvoicesByTechnician(long id, DateTime serviceDate)
        {
            try
            {
                return await GetInvoicesByGivenId(id, serviceDate, IdLookupInvoice.TechnicianId);

            }
            catch (Exception ex)
            {
                throw;
            }
        }


        #region Private Methods

        private async Task<List<InvoiceDb>> GetInvoicesByGivenId(long id, DateTime serviceDate, IdLookupInvoice typeOfId)
        {
            List<InvoiceDb> dbListToReturn = [];

            var connectionString = ConfigHelper.GetDefaultConnection();

            using var sqlConnection = new SqlConnection(connectionString);

            await sqlConnection.OpenAsync();

            string query = @$"SELECT [Id]
                                  ,[TechnicianId]
                                  ,[CustomerId]
                                  ,[IsPaid]
                                  ,[DatePaid]
                                  ,[DateDue]
                                  ,[JobId]
                              FROM [OnyxDb].[dbo].[Invoices]
                              WHERE {typeOfId} = @GivenId AND ServiceDate = @ServiceDate;        
";

            using var command = new SqlCommand(query, sqlConnection);

            command.Parameters.AddWithValue("@GivenId", id);
            command.Parameters.AddWithValue("@ServiceDate", serviceDate);


            await using var reader = await command.ExecuteReaderAsync();


            if (reader.HasRows)
            {
                while (await reader.ReadAsync())
                {
                    long idDb = reader.Get<long, InvoiceColumns>(InvoiceColumns.Id);
                    long jobId = reader.Get<long, InvoiceColumns>(InvoiceColumns.JobId);
                    long technicianId = reader.Get<long, InvoiceColumns>(InvoiceColumns.TechnicianId);
                    long customerIdDb = reader.Get<long, InvoiceColumns>(InvoiceColumns.CustomerId);
                    DateTime? datePaid = reader.Get<DateTime?, InvoiceColumns>(InvoiceColumns.DatePaid);
                    DateTime dateDue = reader.Get<DateTime, InvoiceColumns>(InvoiceColumns.DateDue);

                    var invoiceDb = new InvoiceDb()
                    {
                        Id = idDb,
                        JobId = jobId,
                        TechnicianId = technicianId,
                        CustomerId = customerIdDb,
                        DatePaid = datePaid,
                        DateDue = dateDue
                    };

                    dbListToReturn.Add(invoiceDb);
                }
            }
            await sqlConnection.CloseAsync();

            return dbListToReturn;
        }

        #endregion
    }
}
