using System;
using System.Collections.Generic;
using System.Text;

namespace Onyx.Service.Application.Constants
{
    public static class JobExceptionMessageConstants
    {
        public static string NullJobError = "No job was sent";
        public static string StartTimeGreaterError = "Start time cannot be greater than end time";
        public static string JobDescriptionEmptyError = "Job Description cannot be empty";
        public static string JobStatusShouldBeScheduledOrPending = "New job status should be either scheduled or pending";
        public static string ServiceDateWasEmpty = "Service date was empty";
        public static string ServiceDateWasInvalid = "Service date was invalid";
        public static string IdInvalid = "$Id is not valid";
    }
}
