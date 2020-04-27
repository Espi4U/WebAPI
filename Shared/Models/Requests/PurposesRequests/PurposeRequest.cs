using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using WebAPI.Models.APIModels;

namespace Shared.Models.Requests.PurposesRequests
{
    public class PurposeRequest
    {
        public Purpose Purpose { get; set; }
    }
}
