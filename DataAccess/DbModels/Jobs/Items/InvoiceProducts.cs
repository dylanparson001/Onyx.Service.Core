namespace onyx_services_core.DataAccess.DbModels.Jobs.Items
{
    public class InvoiceProducts
    {
        public Guid InvoiceId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
