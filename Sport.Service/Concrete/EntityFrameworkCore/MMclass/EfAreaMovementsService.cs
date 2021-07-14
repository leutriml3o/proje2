using Sport.Domain.Entities.MMRelation;
using Sport.Repository.Abstract.MMinterfaces;
using Sport.Service.Abstract.MMRelation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sport.Service.Concrete.EntityFrameworkCore.MMclass
{
    public class EfAreaMovementsService:IAreaMovementsService
    {

        private readonly IAreaMovementsRepository _areaMovementsRepo;
        public EfAreaMovementsService(IAreaMovementsRepository areaMovementsRepo)
        {
            _areaMovementsRepo = areaMovementsRepo;
        }

        public async Task<int> AddAreaMovementsAsync(AreaMovements areaMovements)
        {
            return await _areaMovementsRepo.Add(areaMovements);
        }


        public async Task<int> DeleteAreaMovementsAsync(AreaMovements areaMovements)
        {
            return await _areaMovementsRepo.Delete(areaMovements);
        }

        public async Task<int> EditAreaMovementsAsync(AreaMovements areaMovements)
        {
            return await _areaMovementsRepo.Edit(areaMovements);
        }

        public async Task<AreaMovements> AreaMovementsById(int Id)
        {
            AreaMovements getAreaMovements = await _areaMovementsRepo.Get(p => p.Id == Id);
            return getAreaMovements;
        }

        public async Task<IEnumerable<AreaMovements>> GetAllAreaMovementsAsync()
        {
            return await _areaMovementsRepo.GetAll();
        }
    }
}
