using Sport.Domain.Entities.MMRelation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sport.Service.Abstract.MMRelation
{
    public interface IUserSportListsService
    {

        Task<IEnumerable<UserSportLists>> GetAllUserSportListsAsync();
        Task<int> AddUserSportListsAsync(UserSportLists userSportLists);

        Task<int> EditUserSportListsAsync(UserSportLists userSportLists);

        Task<int> DeleteUserSportListsAsync(UserSportLists userSportLists);

        Task<UserSportLists> UserSportListsById(int Id);
        Task<UserSportLists> UserSportList(string userId);
        Task<List<UserSportLists>> UserSportLists(string userId);
        Task<int> AddUserSportListsAsync(string userId, int sportListId);
        Task<bool> UserSportListIsThere(string userId, int sportListId);

    }
}
