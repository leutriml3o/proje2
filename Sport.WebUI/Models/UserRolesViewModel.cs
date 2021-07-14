using Sport.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sport.WebUI.Models
{
    public class UserRolesViewModel
    {

        public UserRolesViewModel()
        {

        }

        public List<string> userName { get; set; }
        public List<string> userRolName { get; set; }

        public string[] userName1 { get; set; }
        public string[] userRolName1 { get; set; }
    }
}
