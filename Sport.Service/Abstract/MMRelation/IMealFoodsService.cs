using Sport.Domain.Entities.MMRelation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sport.Service.Abstract.MMRelation
{
    public interface IMealFoodsService
    {
        Task<IEnumerable<MealFoods>> GetAllMealFoodsAsync();
        Task<int> AddMealFoodsAsync(MealFoods mealFoods);

        Task<int> EditMealFoodsAsync(MealFoods mealFoods);

        Task<int> DeleteMealFoodsAsync(MealFoods mealFoods);

        Task<MealFoods> MealFoodsById(int Id);
    }
}
