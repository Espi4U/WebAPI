using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Models.Requests
{
    public class RegistrationRequest
    {
        public string FamilyName { get; set; }
        public string PersonName { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Key { get; set; } = null;
    }
}