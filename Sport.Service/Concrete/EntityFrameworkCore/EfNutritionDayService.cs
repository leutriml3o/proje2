using Sport.Domain.Entities;
using Sport.Repository.Abstract;
using Sport.Service.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sport.Service.Concrete.EntityFrameworkCore
{
    public class EfNutritionDayService : INutritionDayService
    {
        private readonly INutritionDayRepository _nutritionDayRepo;
        public EfNutritionDayService(INutritionDayRepository nutritionDayRepo)
        {
            _nutritionDayRepo = nutritionDayRepo;
        }

        public async Task<int> AddNutritionDayAsync(NutritionDay nutritionDay)
        {
            return await _nutritionDayRepo.Add(nutritionDay);
        }


        public async Task<int> DeleteNutritionDayAsync(NutritionDay nutritionDay)
        {
            return await _nutritionDayRepo.Delete(nutritionDay);
        }

        public async Task<int> EditNutritionDayAsync(NutritionDay nutritionDay)
        {
            return await _nutritionDayRepo.Edit(nutritionDay);
        }

        public async Task<NutritionDay> NutritionDayById(int Id)
        {
            NutritionDay getNutritionDay = await _nutritionDayRepo.Get(p => p.Id == Id);
            return getNutritionDay;
        }

        public async Task<IEnumerable<NutritionDay>> GetAllNutritionDayAsync()
        {
            return await _nutritionDayRepo.GetAll();
        }

        public async Task<List<NutritionDay>> GetNutritionDaysByNutritionListId(int nutritionListId)
        {
            IEnumerable<NutritionDay> daysForNutritionList = await _nutritionDayRepo.GetAll();

            List<NutritionDay> returnDays = daysForNutritionList.Where(x => x.FKNutritionListId == nutritionListId).ToList();

            return returnDays;
        }

   
    }
}
