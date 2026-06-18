using Onyx.Service.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Onyx.Service.Infrastructure.DataAccess.Enums.ColumnEnums
{
    internal enum JobsColumnEnum
    {
        Id,
        JobGuid,
        TechnicianId,
        CustomerId,
        ScheduledStartTime,
        ScheduledEndTime,
        ActualStartTime,
        ActualEndTime,
        IsCompleted,
        JobDescription,
        Status,
        RemovedAt,
        RemovedReason,
        ServiceDate
    }
}
