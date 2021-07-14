using Sport.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sport.Service.Abstract
{
   public interface ISportDayService
    {

        Task<IEnumerable<SportDay>> GetAllSportDayAsync();
        Task<int> AddSportDayAsync(SportDay sportDay);

        Task<int> EditSportDayAsync(SportDay sportDay);

        Task<int> DeleteSportDayAsync(SportDay sportDay);

        Task<SportDay> SportDayById(int Id);

        Task<List<SportDay>> GetSportDaysBySportListId(int sportListId);
    }
}
