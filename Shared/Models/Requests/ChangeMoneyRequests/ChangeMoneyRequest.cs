﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using WebAPI.Models.APIModels;

namespace Shared.Models.Requests.ChangeMoneyRequests
{
    public class ChangeMoneyRequest
    {
        [JsonProperty("changemoney")]
        public ChangeMoney ChangeMoney { get; set; }
    }
}