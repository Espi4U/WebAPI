using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Models.Responses
{
    public class LoginResponse : BaseResponse
    {
        public int PersonId { get; set; }
        public string Token { get; set; }
    }
}
