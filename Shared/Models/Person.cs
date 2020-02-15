using System.Collections.Generic;
namespace WebAPI.Models.APIModels
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsHead { get; set; }
        public int PersonalPINCode { get; set; }
        public List<ChangeMoney> ChangesInMoney { get; set; }
        public List<Purpose> Purposes { get; set; }
        public List<Purse> Purses { get; set; }
        public List<Report> Reports { get; set; }
        public int FamilyId { get; set; }
        public Family Family { get; set; }
    }
}
