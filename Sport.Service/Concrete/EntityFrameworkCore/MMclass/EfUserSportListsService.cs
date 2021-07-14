using Microsoft.EntityFrameworkCore;
using Sport.Domain;
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
    public class EfUserSportListsService : IUserSportListsService
    {

        private readonly IUserSportListsRepository _userSportListsRepository;
        private readonly SportDatabaseContext _sportDatabaseContext;

        public EfUserSportListsService(IUserSportListsRepository userSportListsRepository,
            SportDatabaseContext sportDatabaseContext)
        {
            _userSportListsRepository = userSportListsRepository;
            _sportDatabaseContext = sportDatabaseContext;
        }

        public async Task<int> AddUserSportListsAsync(UserSportLists userSportLists)
        {
            return await _userSportListsRepository.Add(userSportLists);
        }


        public async Task<int> DeleteUserSportListsAsync(UserSportLists userSportLists)
        {
            return await _userSportListsRepository.Delete(userSportLists);
        }

        public async Task<int> EditUserSportListsAsync(UserSportLists userSportLists)
        {
            return await _userSportListsRepository.Edit(userSportLists);
        }

        public async Task<UserSportLists> UserSportListsById(int Id)
        {
            UserSportLists getUserSportLists = await _userSportListsRepository.Get(p => p.Id == Id);
            return getUserSportLists;
        }

        public async Task<IEnumerable<UserSportLists>> GetAllUserSportListsAsync()
        {
            return await _userSportListsRepository.GetAll();
        }

        public async Task<bool> UserSportListIsThere(string userId, int sportListId)
        {
            int sayisi_spor = await _sportDatabaseContext.UserSportLists.Where(x => x.UserSecret == userId && x.FKSportListId == sportListId).CountAsync();
            if (sayisi_spor > 0)
            {
                return true;
            }
            return false;
        }
        public async Task<int> AddUserSportListsAsync(string userId, int sportListId)
        {
            UserSportLists userList = new UserSportLists();
            userList.UserSecret = userId;
            userList.FKSportListId = sportListId;
            int succes = 0;
            try
            {
                await _sportDatabaseContext.UserSportLists.AddAsync(userList);
                succes = _sportDatabaseContext.SaveChanges();
                return succes;
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }
            return succes;
        }
        public async Task<List<UserSportLists>> UserSportLists(string userId)
        {
            List<UserSportLists> userSportLists = await _sportDatabaseContext.UserSportLists.Include(x => x.SportList).Where(x => x.UserSecret == userId).ToListAsync();
            return userSportLists;
        }
        public async Task<UserSportLists> UserSportList(string userId)
        {
            UserSportLists userSportList = await _sportDatabaseContext.UserSportLists.Include(x => x.SportList).Where(x => x.UserSecret == userId).FirstOrDefaultAsync();
            return userSportList;
        }
    }
}
