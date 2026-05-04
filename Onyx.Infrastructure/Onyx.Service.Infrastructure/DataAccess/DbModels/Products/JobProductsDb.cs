using System;
using System.Collections.Generic;
using System.Text;

namespace Onyx.Service.Infrastructure.DataAccess.DbModels.Products
{
    public class JobProductsDb
    {
        public int Id { get; set; }
        public long InvoiceId { get; set; }
        public long ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
