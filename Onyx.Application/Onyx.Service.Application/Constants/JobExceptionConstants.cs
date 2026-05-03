using System;
using System.Collections.Generic;
using System.Text;

namespace Onyx.Service.Application.Constants
{
    public static class JobExceptionConstants
    {
        public static string NullJobError = "No job was sent";
        public static string StartTimeGreaterError = "Start time cannot be greater than end time";
        public static string JobDescriptionEmptyError = "Job Description cannot be empty";
        public static string JobStatusShouldBeScheduledOrPending = "New job status should be either scheduled or pending";
    }
}
