using Microsoft.AspNetCore.Mvc.Rendering;
using Sport.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sport.WebUI.ViewModels
{
    public class SelectFoodsAndThatViewModel
    {
        public List<Food> allFoods { get; set; }
        public int thatId { get; set; }
        public string[] selectedFoodIdArray { get; set; }
    }
}
