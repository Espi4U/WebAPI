﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models.APIModels
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int FamilyId { get; set; }
        public Family Family { get; set; }
        public List<ChangeMoney> ChangeMoneys { get; set; }
    }
}