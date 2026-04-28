using onyx_services_core.Common.Enums;
using onyx_services_core.DataAccess.DbModels.Contacts.BaseTypes;

namespace onyx_services_core.DataAccess.DbModels.Contacts.Employees
{
    public class EmployeeDb : ContactDb
    {
        public DateTime HireDate { get; set; }
        public DateTime? TerminationDate { get; set; }
        public AccessLevel Access { get; set; }
    }   
}
