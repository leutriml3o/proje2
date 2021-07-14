using Microsoft.EntityFrameworkCore;
using Sport.Domain;
using Sport.Domain.Entities;
using Sport.Domain.Entities.MMRelation;
using Sport.Repository.Abstract;
using Sport.Repository.Abstract.MMinterfaces;
using Sport.Service.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sport.Service.Concrete.EntityFrameworkCore
{
    public class EfSportListService : ISportListService
    {
        private readonly ISportListRepository _sportListRepo;
        private readonly ISportDayRepository _sportDayRepository;
        private readonly IAreaRepository _areaRepository;
        private readonly IAreaMovementsRepository _areaMovementsRepository;
        private readonly SportDatabaseContext _context;
        public EfSportListService(ISportListRepository sportListRepo,
            ISportDayRepository sportDayRepository,
            IAreaRepository areaRepository,
            IAreaMovementsRepository areaMovementsRepository,
            SportDatabaseContext context)
        {
            _sportListRepo = sportListRepo;
            _sportDayRepository = sportDayRepository;
            _areaRepository = areaRepository;
            _areaMovementsRepository = areaMovementsRepository;
            _context = context;
        }

        public async Task<int> AddSportListAsync(SportList sportList)
        {
            var savedNutritionList = await _sportListRepo.AddEntityAndGetId(sportList);

            SportDay sDay = null;
            Area aDay = null;
            int success = 0;

            for (int i = 1; i <= 7; i++)
            {
                sDay = new SportDay();
                sDay.Name = i + ".Gün";
                sDay.FKSportListId = savedNutritionList.Id;
                sDay = await _sportDayRepository.AddEntityAndGetId(sDay);

                for (int j = 1; j <= 8; j++)
                {
                    aDay = new Area();
                    if (j == 1)
                    {
                        aDay.Name = "Göğüs";
                    }
                    else if (j == 2)
                    {
                        aDay.Name = "Sırt";
                    }
                    else if (j == 3)
                    {
                        aDay.Name = "Omuz";
                    }
                    else if (j == 4)
                    {
                        aDay.Name = "Ön Kol";
                    }
                    else if (j == 5)
                    {
                        aDay.Name = "Arka Kol";
                    }
                    else if (j == 6)
                    {
                        aDay.Name = "Bacak";
                    }
                    else if (j == 7)
                    {
                        aDay.Name = "Kardiyo";
                    }
                    else
                    {
                        aDay.Name = "Karın";
                    }

                    aDay.FKDayId = sDay.Id;
                    success = await _areaRepository.Add(aDay);
                }
            }
            if (success > 0)
            {
                return 1;
            }
            return 0;
        }

        public async Task<int> DeleteSportListAsync(SportList sportList)
        {
            return await _sportListRepo.Delete(sportList);
        }

        public async Task<int> EditSportListAsync(SportList sportList)
        {
            return await _sportListRepo.Edit(sportList);
        }

        public async Task<SportList> SportListById(int Id)
        {
            SportList getSportList = await _sportListRepo.Get(p => p.Id == Id);
            return getSportList;
        }

        public async Task<IEnumerable<SportList>> GetAllSportListAsync()
        {
            return await _sportListRepo.GetAll();
        }

        public async Task<int> AddAreaMovements(string[] stringMovementIdList, int areaId)
        {
            AreaMovements newAreaMovement = null;
            int success = 0;

            foreach (var thatMovement in stringMovementIdList)
            {
                newAreaMovement = new AreaMovements();
                newAreaMovement.FKMovementId = Convert.ToInt32(thatMovement);
                newAreaMovement.FKAreaId = areaId;

                await _areaMovementsRepository.Add(newAreaMovement);
                success = await _areaMovementsRepository.Save();
            }

            if (success > 0)
            {
                return 1;
            }
            return 0;
        }

        public async Task<IEnumerable<AreaMovements>> SportListDetailsView(int sportListId)
        {
            List<AreaMovements> areaMovements = _context.AreaMovements.Include(x => x.Area).Include(x => x.Movement).Include(x=>x.Area.SportDay).Where(x => x.Area.SportDay.SportList.Id == sportListId).ToList();  

            return areaMovements;
        }

    }
}