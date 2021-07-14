using Microsoft.EntityFrameworkCore;
using Sport.Domain;
using Sport.Domain.Entities;
using Sport.Domain.Entities.MMRelation;
using Sport.Repository.Abstract.MMinterfaces;
using Sport.Service.Abstract.MMRelation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sport.Service.Concrete.EntityFrameworkCore.MMclass
{
    public class EfUserNutritionListsService : IUserNutritionListsService
    {

        private readonly IUserNutritionListsRepository _userNutritionListsRepo;
        private readonly SportDatabaseContext _sportDatabaseContext;

        public EfUserNutritionListsService(IUserNutritionListsRepository userNutritionListsRepo,
            SportDatabaseContext sportDatabaseContext
            )
        {
            _userNutritionListsRepo = userNutritionListsRepo;
            _sportDatabaseContext = sportDatabaseContext;
        }

        public async Task<int> AddUserNutritionListsAsync(UserNutritionLists userNutritionLists)
        {
            return await _userNutritionListsRepo.Add(userNutritionLists);
        }

        public async Task<int> DeleteUserNutritionListsAsync(UserNutritionLists userNutritionLists)
        {
            return await _userNutritionListsRepo.Delete(userNutritionLists);
        }

        public async Task<int> EditUserNutritionListsAsync(UserNutritionLists userNutritionLists)
        {
            return await _userNutritionListsRepo.Edit(userNutritionLists);
        }

        public async Task<UserNutritionLists> UserNutritionListsById(int Id)
        {
            UserNutritionLists getUserNutritionLists = await _userNutritionListsRepo.Get(p => p.Id == Id);
            return getUserNutritionLists;
        }

        public async Task<IEnumerable<UserNutritionLists>> GetAllUserNutritionListsAsync()
        {
            return await _userNutritionListsRepo.GetAll();
        }

        public async Task<bool> UserNutritionListIsThere(string userId, int nutritionListId)
        {
            int sayisi = await _sportDatabaseContext.UserNutritionLists.Where(x => x.UserSecret == userId && x.FKNutritionListId == nutritionListId).CountAsync();
            if (sayisi > 0)
            {
                return true;
            }
            return false;
        }


        public async Task<int> AddUserNutritionListsAsync(string userId, int nutritionListId)
        {
            UserNutritionLists userList = new UserNutritionLists();
            userList.UserSecret =userId;
            userList.FKNutritionListId = nutritionListId;
            int succes = 0;
            try
            {
                await _sportDatabaseContext.UserNutritionLists.AddAsync(userList);
                succes = await _sportDatabaseContext.SaveChangesAsync();
                return succes;
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }

            return succes;
        }

        public async Task<List<UserNutritionLists>> UserNutritionLists(string userId)
        {
            List<UserNutritionLists> userNutritionLists = await _sportDatabaseContext.UserNutritionLists.Include(x => x.NutritionList).Where(x => x.UserSecret == userId).ToListAsync();
            return userNutritionLists;
        }

        public async Task<UserNutritionLists> UserNutritionList(string userId)
        {
            UserNutritionLists userNutritionLists = await _sportDatabaseContext.UserNutritionLists.Include(x => x.NutritionList).Where(x => x.UserSecret == userId).FirstOrDefaultAsync();
            return userNutritionLists;
        }
    }
}
