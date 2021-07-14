using Sport.Domain.Entities.MMRelation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sport.Service.Abstract.MMRelation
{
    public interface IAreaMovementsService
    {
        Task<IEnumerable<AreaMovements>> GetAllAreaMovementsAsync();
        Task<int> AddAreaMovementsAsync(AreaMovements areaMovements);

        Task<int> EditAreaMovementsAsync(AreaMovements areaMovements);

        Task<int> DeleteAreaMovementsAsync(AreaMovements areaMovements);

        Task<AreaMovements> AreaMovementsById(int Id);

    }
}
