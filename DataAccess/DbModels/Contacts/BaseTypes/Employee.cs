using onyx_services_core.Common.Enums;

namespace onyx_services_core.DataAccess.DbModels.Contacts.BaseTypes
{
    public abstract class Employee : Contact
    {
        public DateTime HireDate { get; set; }
        public DateTime? TerminationDate { get; set; }
        public AccessLevel Access { get; set; }
    }
}
