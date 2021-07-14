using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Sport.Domain.Entities
{
    public class NutritionDay
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [DisplayName("Gün")]
        public string Name { get; set; }
        public int FKNutritionListId { get; set; }

        [ForeignKey("FKNutritionListId")]
        public virtual NutritionList NutritionList { get; set; }
        public virtual ICollection<ThatDay> ThatDays { get; set; }
    }
}
