using Sport.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sport.Service.Abstract
{
     public interface IThatDayService
    {

        Task<IEnumerable<ThatDay>> GetAllThatDayAsync();
        Task<int> AddThatDayAsync(ThatDay thatDay);

        Task<int> EditThatDayAsync(ThatDay thatDay);

        Task<int> DeleteThatDayAsync(ThatDay thatDay);

        Task<ThatDay> ThatDayById(int Id);

        Task<List<ThatDay>> GetThatDaysByNutritionDayId(int nutritionDayId);
      
    }
}
