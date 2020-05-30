using System;
using System.Collections.Generic;
using System.Text;
using WebAPI.Models.APIModels;

namespace Shared.Models.Requests
{
    public class WithdrawFromPurseRequest
    {
        public int Size { get; set; }
        public Purse Purse { get; set; }
    }
}
