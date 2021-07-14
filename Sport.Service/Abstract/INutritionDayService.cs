using Sport.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sport.Service.Abstract
{
    public interface INutritionDayService
    {
        Task<IEnumerable<NutritionDay>> GetAllNutritionDayAsync();
        Task<int> AddNutritionDayAsync(NutritionDay nutritionDay);

        Task<int> EditNutritionDayAsync(NutritionDay nutritionDay);

        Task<int> DeleteNutritionDayAsync(NutritionDay nutritionDay);

        Task<NutritionDay> NutritionDayById(int Id);

        Task<List<NutritionDay>> GetNutritionDaysByNutritionListId(int nutritionListId);
    }
}
