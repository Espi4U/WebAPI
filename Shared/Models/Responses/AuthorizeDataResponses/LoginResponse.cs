using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Models.Responses.AuthorizeDataResponses
{
    public class LoginResponse
    {
        public int PersonId { get; set; }
        public int? FamilyId { get; set; }
    }
}
