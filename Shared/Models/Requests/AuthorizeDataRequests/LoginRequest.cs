using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Models.Requests.AuthorizeDataRequests
{
    public class LoginRequest
    {
        public string Login { get; set; }
        public string PasswordHash { get; set; }
        public int PersonalPINCode { get; set; }
    }
}
