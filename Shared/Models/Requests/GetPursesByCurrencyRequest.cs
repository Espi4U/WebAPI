using System;
using System.Collections.Generic;
using System.Text;
using WebAPI.Models.APIModels.Requests;

namespace Shared.Models.Requests
{
    public class GetPursesByCurrencyRequest : BaseRequest
    {
        public int CurrencyId { get; set; }
    }
}
