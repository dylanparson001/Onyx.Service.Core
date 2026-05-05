using Microsoft.Data.SqlClient;
using Onyx.Service.Contracts.Dtos.Invoices;
using Onyx.Service.Domain.Models;
using Onyx.Service.Infrastructure.DataAccess.DbModels.Jobs;
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
                                    VALUES @TechnicianId, @CustomerId, @JobId)";

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
    }
}
