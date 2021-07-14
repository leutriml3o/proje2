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
    public class EfThatDayService : IThatDayService
    {

        private readonly IThatDayRepository _thatDayRepo;
        public EfThatDayService(IThatDayRepository thatDayRepo)
        {
            _thatDayRepo = thatDayRepo;
        }

        public async Task<int> AddThatDayAsync(ThatDay thatDay)
        {
            return await _thatDayRepo.Add(thatDay);
        }


        public async Task<int> DeleteThatDayAsync(ThatDay thatDay)
        {
            return await _thatDayRepo.Delete(thatDay);
        }

        public async Task<int> EditThatDayAsync(ThatDay thatDay)
        {
            return await _thatDayRepo.Edit(thatDay);
        }

        public async Task<ThatDay> ThatDayById(int Id)
        {
            ThatDay getThatDay = await _thatDayRepo.Get(p => p.Id == Id);
            return getThatDay;
        }

        public async Task<IEnumerable<ThatDay>> GetAllThatDayAsync()
        {
            return await _thatDayRepo.GetAll();
        }
        
        public async Task<List<ThatDay>> GetThatDaysByNutritionDayId(int nutritionDayId)
        {
            IEnumerable<ThatDay> thatsForNutritionDay = await _thatDayRepo.GetAll();

            List<ThatDay> returnThats = thatsForNutritionDay.Where(x => x.FKNutritionDayId  == nutritionDayId).ToList();
            return returnThats;

        }

       
    }
}
