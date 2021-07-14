using Sport.Domain.Entities.MMRelation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sport.Service.Abstract.MMRelation
{
    public interface IUserNutritionListsService
    {

        Task<IEnumerable<UserNutritionLists>> GetAllUserNutritionListsAsync();
        Task<int> AddUserNutritionListsAsync(UserNutritionLists userNutritionLists);

        Task<int> EditUserNutritionListsAsync(UserNutritionLists userNutritionLists);

        Task<int> DeleteUserNutritionListsAsync(UserNutritionLists userNutritionLists);

        Task<UserNutritionLists> UserNutritionListsById(int Id);

        Task<List<UserNutritionLists>> UserNutritionLists(string userId);

        Task<UserNutritionLists> UserNutritionList(string userId);

        Task<int> AddUserNutritionListsAsync(string userId, int nutritionListId);
        Task<bool> UserNutritionListIsThere(string userId, int nutritionListId);
    }
}
