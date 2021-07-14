using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sport.Domain.Entities;
using Sport.Domain.Entities.MMRelation;

namespace Sport.WebUI.ViewModels
{
    public class UserSportListDetails
    {
        public IEnumerable<AreaMovements> sportLists { get; set; }
        public AppUser appUser { get; set; }
    }
}