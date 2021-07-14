using Sport.Domain.Entities;
using Sport.Repository.Abstract;
using Sport.Repository.Concrete;
using Sport.Service.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Sport.Service.Concrete.EntityFrameworkCore
{
    public class EfFoodService : IFoodService
    {
        private readonly IFoodRepository _foodRepo;
        public EfFoodService(IFoodRepository foodRepo)
        {
            _foodRepo = foodRepo;
        }

        public async Task<int> AddFoodAsync(Food food)
        {
            return await _foodRepo.Add(food);
        }


        public async Task<int> DeleteFoodAsync(Food food)
        {
            return await _foodRepo.Delete(food);
        }

        public async Task<int> EditFoodAsync(Food food)
        {
            return await _foodRepo.Edit(food);
        }

        public async Task<Food> FoodById(int Id)
        {
            Food getFood = await _foodRepo.Get(p => p.Id == Id);
            return getFood;
        }

        public async Task<List<Food>> GetAllFoodAsync()
        {
            IEnumerable<Food> list = await _foodRepo.GetAll();
            List<Food> foods = list.ToList();
            return foods;
        }

    }
}
