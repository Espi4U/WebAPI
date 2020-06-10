using System;
using System.Collections.Generic;
using System.Text;
using WebAPI.Models.APIModels.Requests;

namespace Shared.Models.Requests
{
    public class UpdatePurposeRequest : BaseRequest
    {
        public int PurposeId { get; set; }
        public int PurseId { get; set; }
        public int NewSize { get; set; }
    }
}
