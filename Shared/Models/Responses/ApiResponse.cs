using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Models.Responses
{
    public class ApiResponse
    {
        public bool IsSuccess { get; set; } = true;
        public string ApiMessage { get; set; } = "";
    }
}
