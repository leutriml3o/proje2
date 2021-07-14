using Sport.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sport.Service.Abstract
{
    public interface IAreaService
    {
        Task<IEnumerable<Area>> GetAllAreaAsync();
        Task<int> AddAreaAsync(Area area);

        Task<int> EditAreaAsync(Area area);

        Task<int> DeleteAreaAsync(Area area);

        Task<Area> AreaById(int Id);

        Task<List<Area>> GetAreasBySportDayId(int sportDayId);
    }
}
