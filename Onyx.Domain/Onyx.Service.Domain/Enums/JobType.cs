using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Onyx.Service.Domain.Enums
{
    public enum JobType
    {
        [Description("Service")]
        Service,
        [Description("Installation")]
        Installation,
        [Description("Call Back")]
        CallBack
    }
}
