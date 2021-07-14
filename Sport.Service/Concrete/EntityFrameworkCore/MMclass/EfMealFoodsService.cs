using Sport.Domain.Entities.MMRelation;
using Sport.Repository.Abstract.MMinterfaces;
using Sport.Service.Abstract.MMRelation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sport.Service.Concrete.EntityFrameworkCore.MMclass
{
    public class EfMealFoodsService:IMealFoodsService
    {

        private readonly IMealFoodsRepository _mealFoodsRepo;
        public EfMealFoodsService(IMealFoodsRepository mealFoodsRepo)
        {
            _mealFoodsRepo = mealFoodsRepo;
        }

        public async Task<int> AddMealFoodsAsync(MealFoods mealFoods)
        {
            return await _mealFoodsRepo.Add(mealFoods);
        }


        public async Task<int> DeleteMealFoodsAsync(MealFoods mealFoods)
        {
            return await _mealFoodsRepo.Delete(mealFoods);
        }

        public async Task<int> EditMealFoodsAsync(MealFoods mealFoods)
        {
            return await _mealFoodsRepo.Edit(mealFoods);
        }

        public async Task<MealFoods> MealFoodsById(int Id)
        {
            MealFoods getMealFoods = await _mealFoodsRepo.Get(p => p.Id == Id);
            return getMealFoods;
        }

        public async Task<IEnumerable<MealFoods>> GetAllMealFoodsAsync()
        {
            return await _mealFoodsRepo.GetAll();
        }
    }
}
