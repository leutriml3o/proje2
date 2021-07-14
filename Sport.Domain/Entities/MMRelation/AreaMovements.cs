using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Sport.Domain.Entities.MMRelation
{
    public class AreaMovements
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int FKAreaId { get; set; }
        public int FKMovementId { get; set; }

        [ForeignKey("FKAreaId")]
        public virtual Area Area { get; set; }
        [ForeignKey("FKMovementId")]
        public virtual Movement Movement { get; set; }
    }
}
