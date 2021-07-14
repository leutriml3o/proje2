using Microsoft.AspNetCore.Identity;
using Sport.Domain.Entities.MMRelation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Sport.Domain.Entities
{
   public class AppUser:IdentityUser
    {
        public int Age { get; set; }
        public int Calorie { get; set; }
        public int Weight { get; set; }
        public double Height { get; set; }
        public double FatRate { get; set; }

        public virtual ICollection<UserNutritionLists> NutritionLists { get; set; }
        public virtual ICollection<UserSportLists> UserSportLists { get; set; }
    }
}
