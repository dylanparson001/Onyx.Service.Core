using Onyx.Service.Domain.Auth;
using Onyx.Service.Infrastructure.DataAccess.DbModels.Contacts.BaseTypes;

namespace Onyx.Service.Infrastructure.DataAccess.DbModels.Contacts.Employees
{
    public class EmployeeDb : ContactDb
    {
        public DateTime HireDate { get; set; }
        public DateTime? TerminationDate { get; set; }
        public AccessLevel Access { get; set; }
    }   
}
