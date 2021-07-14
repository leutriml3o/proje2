using Sport.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sport.WebUI.ViewModels
{
    public class SelectMovementAndAreaViewModel
    {
        public List<Movement> allMovements { get; set; }
        public int areaId { get; set; }
        public string[] selectedMovementIdArray { get; set; }
    }
}
