using Sport.Domain.Entities;
using Sport.Domain.Entities.MMRelation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sport.Service.Abstract
{
   public interface INutritionListService
    {
        Task<IEnumerable<NutritionList>> GetAllNutritionListAsync();
        Task<int> AddNutritionListAsync(NutritionList nutritionList);

        Task<int> EditNutritionListAsync(NutritionList nutritionList);

        Task<int> DeleteNutritionListAsync(NutritionList nutritionList);

        Task<NutritionList> NutritionListById(int Id);

        Task<int> AddThatFoods(string[] stringFoodIdList, int thatId);

        Task<IEnumerable<MealFoods>> NutritionListDetailsView(int nutritionListId);
    }
}
