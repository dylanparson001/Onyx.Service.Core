using Onyx.Service.Domain.Auth;
using System;
using System.Collections.Generic;
using System.Text;

namespace Onyx.Service.Contracts.Dtos.Users
{
    public class EmployeeDto
    {
        public long Id { get; set; }
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string Address { get; set; } = "";
        public string PhoneNumber { get; set; } = "";
        public string City { get; set; } = "";
        public string State { get; set; } = "";
        public string ZipCode { get; set; } = "";
        public string Email { get; set; } = "";
        public string Username { get; set; } = "";
        public DateTime HireDate { get; set; }
        public DateTime? TerminationDate { get; set; }
        public AccessLevel Access { get; set; }
    }
}
