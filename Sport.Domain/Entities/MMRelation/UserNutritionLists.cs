using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Sport.Domain.Entities.MMRelation
{
    public class UserNutritionLists
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string UserSecret { get; set; }
        public int FKNutritionListId { get; set; }

        //[ForeignKey("FKUserId")]
        //public virtual AppUser User { get; set; }
        [ForeignKey("FKNutritionListId")]
        public virtual NutritionList NutritionList { get; set; }
    }
}
