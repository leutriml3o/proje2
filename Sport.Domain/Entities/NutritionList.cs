using Sport.Domain.Entities.MMRelation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using static Sport.Domain.Enums.AllEnums;

namespace Sport.Domain.Entities
{
    public class NutritionList
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public double TotalCaloriValue { get; set; }

        public EnumNutritionType EnumNutritionType { get; set; }
        public EnumNutritionKind EnumNutritionKind { get; set; }

        public virtual ICollection<NutritionDay> NutritionDays { get; set; }
        public virtual ICollection<UserNutritionLists> UserNutritionLists { get; set; }
    }
}
