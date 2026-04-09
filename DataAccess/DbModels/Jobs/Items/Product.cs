namespace onyx_services_core.DataAccess.DbModels.Jobs.Items
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public decimal Price { get; set; } = 0;

    }
}
