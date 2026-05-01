namespace Onyx.Service.Infrastructure.DataAccess.DbModels.Products
{
    public class ProductDb
    {
        public long Id { get; set; }
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public decimal Price { get; set; } = 0;

    }
}
