using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using WebAPI.Models.APIModels;
using WebAPI.Models.APIModels.Requests;

namespace Shared.Models.Requests.ChangeMoneyRequests
{
    public class ChangeMoneyRequest : BaseRequest
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public int Size { get; set; }
        public DateTime Date { get; set; }
        public Category Category { get; set; }
        public Currency Currency { get; set; }
        public Purse Purse { get; set; }
    }
}
