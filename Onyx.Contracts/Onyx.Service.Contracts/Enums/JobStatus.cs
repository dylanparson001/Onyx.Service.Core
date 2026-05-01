using System.ComponentModel;

namespace Onyx.Service.Contracts.Enums
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
