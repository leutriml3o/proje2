using Sport.Domain.Entities;
using Sport.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Sport.Service.Abstract
{
    public interface IFoodService
    {
        Task<List<Food>> GetAllFoodAsync();
        Task<int> AddFoodAsync(Food food);

        Task<int> EditFoodAsync(Food food);

        Task<int> DeleteFoodAsync(Food food);

        Task<Food> FoodById(int Id);


    }
}
