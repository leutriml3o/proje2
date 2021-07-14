using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Sport.Domain.Entities.MMRelation
{
    public class MealFoods
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int FKFoodId { get; set; }
        public int FKThatDayId { get; set; }

        [ForeignKey("FKFoodId")]
        public virtual Food Food { get; set; }
        [ForeignKey("FKThatDayId")]
        public virtual ThatDay ThatDay { get; set; }
    }
}
