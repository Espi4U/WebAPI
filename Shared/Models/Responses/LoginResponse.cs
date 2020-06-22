using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Models.Responses
{
    public class LoginResponse : BaseResponse
    { 
        public int? FamilyId { get; set; }
        public int? PersonId { get; set; }
        public string PersonName { get; set; }
        public string FamilyName { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
    }
}
