using Sport.Domain.Entities;
using Sport.Domain.Entities.MMRelation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sport.Service.Abstract
{
    public interface ISportListService
    {
        Task<IEnumerable<SportList>> GetAllSportListAsync();
        Task<int> AddSportListAsync(SportList sportList);

        Task<int> EditSportListAsync(SportList sportList);

        Task<int> DeleteSportListAsync(SportList sportList);

        Task<SportList> SportListById(int Id);

        Task<int> AddAreaMovements(string[] stringMovementIdList, int areaId);

        Task<IEnumerable<AreaMovements>> SportListDetailsView(int sportListId);
    }
}
