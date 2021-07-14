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
    public class EfSportDayService:ISportDayService
    {
        private readonly ISportDayRepository _sportDayRepo;
        public EfSportDayService(ISportDayRepository sportDayRepo)
        {
            _sportDayRepo = sportDayRepo;
        }

        public async Task<int> AddSportDayAsync(SportDay sportDay)
        {
            return await _sportDayRepo.Add(sportDay);
        }


        public async Task<int> DeleteSportDayAsync(SportDay sportDay)
        {
            return await _sportDayRepo.Delete(sportDay);
        }

        public async Task<int> EditSportDayAsync(SportDay sportDay)
        {
            return await _sportDayRepo.Edit(sportDay);
        }

        public async Task<SportDay> SportDayById(int Id)
        {
            SportDay getSportDay = await _sportDayRepo.Get(p => p.Id == Id);
            return getSportDay;
        }

        public async Task<IEnumerable<SportDay>> GetAllSportDayAsync()
        {
            return await _sportDayRepo.GetAll();
        }

        public async Task<List<SportDay>> GetSportDaysBySportListId(int sportListId)
        {
            IEnumerable<SportDay> daysForSportList = await _sportDayRepo.GetAll();

            List<SportDay> returnDays = daysForSportList.Where(x => x.FKSportListId == sportListId).ToList();

            return returnDays;
        }
    }
}
