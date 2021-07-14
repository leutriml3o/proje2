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
    public class Movement
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [DisplayName("Hareket İsmi")]
        public string MovementName { get; set; }
        public string MovementPhoto { get; set; }
        [DisplayName("Hareket Açıklaması")]
        public string MovementDescription { get; set; }
        public EnumMovementType EnumMovementType { get; set; }

        public virtual ICollection<AreaMovements> AreaMovements { get; set; }
    }
}
