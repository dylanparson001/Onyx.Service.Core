using System.ComponentModel;

namespace Onyx.Service.Contracts.Models
{
    public enum UserRoles
    {
        [Description("ADMIN")]
        Admin = 0,
        [Description("TECHNICIAN")]
        Technician = 1,
        [Description("MANAGER")]
        Manager = 2,
    }
}
