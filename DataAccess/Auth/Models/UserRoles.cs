using System.ComponentModel;

namespace onyx_services_core.DataAccess.Auth.Models
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
