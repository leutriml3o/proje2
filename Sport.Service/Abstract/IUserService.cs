using Microsoft.AspNetCore.Identity;
using Sport.Domain;
using Sport.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sport.Service.Abstract
{
    public interface IUserService
    {

        Task<IEnumerable<AppUser>> GetAllUserAsync();
        Task<int> AddUserAsync(AppUser user);

        Task<int> EditUserAsync(AppUser user);

        Task<int> DeleteUserAsync(AppUser user);

        Task<AppUser> UserByName(string username);
        Task<string> GetUserByName(string id);

        Task<string> GetUserByRolName(string id);

        Task<IEnumerable<IdentityUserRole<string>>> GetAllUserRole();

    }
}
