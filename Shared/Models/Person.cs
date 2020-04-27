﻿using Newtonsoft.Json;
using System.Collections.Generic;
namespace WebAPI.Models.APIModels
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public List<ChangeMoney> ChangeMoneys { get; set; }
        public List<Purpose> Purposes { get; set; }
        public List<Purse> Purses { get; set; }
        public List<Report> Reports { get; set; }
        public int FamilyId { get; set; }
        public Family Family { get; set; }


        // дані для авторизації/реєстрації
        public string Login { get; set; }
        public string PasswordHash { get; set; }
        public int PINCode { get; set; }
    }
}
