namespace Onyx.Service.Infrastructure.DataAccess.DbModels.Products
{
    public class InvoiceProductsDb
    {
        public long InvoiceId { get; set; }
        public long ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
