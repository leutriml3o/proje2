using Sport.Domain.Entities.MMRelation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using static Sport.Domain.Enums.AllEnums;

namespace Sport.Domain.Entities
{
    public class SportList
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public EnumSportType EnumSportType { get; set; }

        public virtual ICollection<SportDay> SportDays { get; set; }
        public virtual ICollection<UserSportLists> UserSportLists { get; set; }
    }
}
