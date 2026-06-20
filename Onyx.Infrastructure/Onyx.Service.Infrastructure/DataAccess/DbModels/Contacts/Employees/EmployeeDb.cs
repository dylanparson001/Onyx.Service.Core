using Onyx.Service.Infrastructure.DataAccess.DbModels.Contacts.BaseTypes;
using Onyx.Shared.Contracts.Users;
using Onyx.Shared.Enums;

namespace Onyx.Service.Infrastructure.DataAccess.DbModels.Contacts.Employees
{
    public class EmployeeDb : ContactDb
    {
        public string Username { get; set; } = "";
        public DateTime HireDate { get; set; }
        public DateTime? TerminationDate { get; set; }
        public AccessLevel Access { get; set; }

        public string DaysAvailable { get; set; } = "";

        public EmployeeDto ToDto()
        {
            return new EmployeeDto
            {
                Id = Id,
                FirstName = FirstName,
                LastName = LastName,
                Address = Address,
                PhoneNumber = PhoneNumber,
                City = City,
                State = State,
                ZipCode = ZipCode,
                Email = Email,
                Username = Username,
                HireDate = HireDate,
                TerminationDate = TerminationDate,
                Access = Access
            };
        }
    }
}
