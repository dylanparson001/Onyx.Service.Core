using System.ComponentModel;

namespace Onyx.Service.Domain.Jobs
{
    public enum JobStatus
    {
        [Description("Open")]
        Open,
        [Description("Pending")]
        Pending,
        [Description("Started")]
        Started,
        [Description("Completed")]
        Complete,
        [Description("Cancelled")]
        Cancelled
    }
}
