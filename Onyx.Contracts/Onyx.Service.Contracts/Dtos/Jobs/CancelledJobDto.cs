using Onyx.Service.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Onyx.Service.Contracts.Dtos.Jobs
{
    public class CancelledJobDto : JobDto
    {
        public DateTime RemovedAt { get; set; }
        public CancellationReason RemovedReason { get; set; }
    }
}
