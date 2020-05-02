using System;
using System.Collections.Generic;
using System.Text;
using WebAPI.Models.APIModels.Requests;

namespace Shared.Models.Requests
{
    public class InviteKeyRequest : BaseRequest
    {
        public string Key { get; set; }
    }
}
