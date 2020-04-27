using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Models.Requests.BaseRequests
{
    public class LoginRequest
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public int PINCode { get; set; }
    }
}
