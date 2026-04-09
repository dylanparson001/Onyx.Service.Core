using onyx_services_core.Common.Enums;
using onyx_services_core.DataAccess.DbModels.Jobs.Items;

namespace onyx_services_core.DataAccess.DbModels.Jobs
{
    public class Invoice : BaseJob
    {

        public List<Product> InvoiceItems { get; set; }
        public DateTime DatePaid { get; set; }
        public DateTime DateDue { get; set; }
    }
}
