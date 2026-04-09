namespace onyx_services_core.DataAccess.DbModels.Jobs
{
    public abstract class BaseJob
    {
        public Guid Id { get; set; }
        public Guid TechnicianId { get; set; }
        public Guid CustomerId { get; set; }
        public Guid InvoiceId { get; set; }
        public DateTime ScheduledStartTime { get; set; }
        public DateTime ScheduledEndTime { get; set; }
        public DateTime ActualStartTime { get; set; }
        public DateTime ActualEndTime { get; set; }
    }
}
