﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using WebAPI.Models.APIModels;

namespace Shared.Models.Responses.ChangeMoneysResponses
{
    public class ChangeMoneyResponse : BaseResponse
    {
        [JsonProperty("changemoney")]
        public ChangeMoney ChangeMoney { get; set; }
    }
}
