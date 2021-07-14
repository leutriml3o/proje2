using Sport.Domain.Entities;
using Sport.Domain.Entities.MMRelation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sport.WebUI.ViewModels
{
    public class UserNutritionListDetails
    {
     
        public IEnumerable<MealFoods> nutritionLists { get; set; }
        public AppUser appUser { get; set; }

    }
}
