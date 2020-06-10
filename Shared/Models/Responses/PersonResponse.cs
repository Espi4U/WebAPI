using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using WebAPI.Models.APIModels;

namespace Shared.Models.Responses.PersonsResponses
{
    public class PersonResponse : BaseResponse
    {
        public Person Person { get; set; }
    }
}
