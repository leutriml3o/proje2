using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Sport.Domain.Entities.MMRelation
{
    public class UserSportLists
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string UserSecret { get; set; }
        public int FKSportListId { get; set; }

        ////[ForeignKey("FKUserId")]
        //public virtual AppUser User { get; set; }
        [ForeignKey("FKSportListId")]
        public virtual SportList SportList { get; set; }
    }
}
