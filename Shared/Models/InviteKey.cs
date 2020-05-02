using System;
using System.Collections.Generic;
using System.Text;
using WebAPI.Models.APIModels;

namespace Shared.Models
{
    public class InviteKey
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public int FamilyId { get; set; }
        public Family Family { get; set; }
    }
}
