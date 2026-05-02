using System;
using System.Collections.Generic;
using System.Text;

namespace Onyx.Service.Contracts.Dtos.Jobs
{
    public class NewJobResponse
    {
        public bool IsSuccess { get; set; }
        public string? ErrorMessage { get; set; }

        public NewJobResponse(string? errorMessage = "")
        {
            // If theres no error message, request was successful
            IsSuccess = string.IsNullOrEmpty(errorMessage);
            ErrorMessage = errorMessage;
        }
    }
}
