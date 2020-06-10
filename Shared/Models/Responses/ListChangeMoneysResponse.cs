using System;
using System.Collections.Generic;
using System.Text;
using WebAPI.Models.APIModels;

namespace Shared.Models.Responses.ChangeMoneysResponses
{
    public class ListChangeMoneysResponse : BaseResponse
    {
        public List<ChangeMoney> ChangeMoneys { get; set; }
    }
}
