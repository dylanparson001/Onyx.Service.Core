using System.ComponentModel;

namespace onyx_services_core.Common.Enums
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
