using System.ComponentModel;

namespace Onyx.Service.Domain.Models
{
    public enum UserRoles
    {
        [Description("Admin")]
        Admin,
        [Description("Technician")]
        Technician,
        [Description("Manager")]
        Manager,
    }
}
