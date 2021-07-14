using Sport.Domain.Entities.MMRelation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Sport.Domain.Entities
{
    public class Area
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [DisplayName("Etki Ettiği Bölge")]
        public string Name { get; set; }
        public int FKDayId { get; set; }


        [ForeignKey("FKDayId")]
        public virtual SportDay SportDay { get; set; }
        public virtual ICollection<AreaMovements> AreaMovements { get; set; }
    }
}
