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
    public class ThatDay
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public int FKNutritionDayId{ get; set; }
        [DisplayName("Hangi Öğün")]
        public EnumMealType EnumMealType { get; set; }

        [ForeignKey("FKNutritionDayId")]
        public virtual NutritionDay NutritionDays { get; set; }
        public virtual ICollection<MealFoods> NutrientsInMeals { get; set; }
    }
}
