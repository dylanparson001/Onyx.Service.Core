using System;
using System.Collections.Generic;
using System.Text;

namespace Onyx.Service.Infrastructure.DataAccess.ColumnEnums
{
    internal enum InvoiceColumns
    {
        Id,
        TechnicianId,
        CustomerId,
        IsPaid,
        DatePaid,
        DateDue,
        JobId
    }
}
