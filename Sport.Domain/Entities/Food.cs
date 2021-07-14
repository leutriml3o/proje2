using Sport.Domain.Entities.MMRelation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using static Sport.Domain.Enums.AllEnums;

namespace Sport.Domain.Entities
{
    public class Food
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [DisplayName("Besin İsmi")]
        public string Name { get; set; }
        public double CaloriValue { get; set; }
        public double ProteinValue { get; set; }
        public double CarbohydrateValue { get; set; }
        public double OilValue { get; set; }
        public double FiberValue { get; set; }
        public string FoodPhoto { get; set; }
        public EnumFoodType EnumFoodType { get; set; }


        public virtual ICollection<MealFoods> MealsIncluded { get; set; }

    }
}
