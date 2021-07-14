using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Sport.Domain.Entities
{
    public class SportDay
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [DisplayName("Gün")]
        public string Name { get; set; }

        public int FKSportListId { get; set; }
        [ForeignKey("FKSportListId")]
        public virtual SportList SportList { get; set; }

        public virtual ICollection<Area> Areas { get; set; }
    }
}
