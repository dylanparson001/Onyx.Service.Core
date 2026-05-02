using System.ComponentModel;

namespace Onyx.Service.Domain.Enums
{
    public enum JobStatus
    {
        [Description("Scheduled")]
        Scheduled,
        [Description("Pending")]
        Pending,
        [Description("Started")]
        Started,
        [Description("Completed")]
        Completed,
        [Description("Cancelled")]
        Cancelled
    }
}
