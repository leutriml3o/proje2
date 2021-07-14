using Microsoft.AspNetCore.Identity;
using Sport.Domain;
using Sport.Domain.Entities;
using Sport.Repository.Abstract;
using Sport.Service.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sport.Service.Concrete.EntityFrameworkCore
{
    public class EfUserService : IUserService
    {

        private readonly IUserRepository _userRepo;
        private readonly AppIdentityDbContext _appIdentityDbContext;
        public EfUserService(IUserRepository userRepo,
            AppIdentityDbContext appIdentityDbContext)
        {
            _userRepo = userRepo;
            _appIdentityDbContext = appIdentityDbContext;
        }

        public async Task<int> AddUserAsync(AppUser user)
        {
            return await _userRepo.Add(user);
        }


        public async Task<int> DeleteUserAsync(AppUser user)
        {
            return await _userRepo.Delete(user);
        }

        public async Task<int> EditUserAsync(AppUser user)
        {
            return await _userRepo.Edit(user);
        }


        public async Task<AppUser> UserByName(string username)
        {
            AppUser getUser = _appIdentityDbContext.Users.Where(x => x.UserName.ToLower() == username.ToLower()).FirstOrDefault();


            return getUser;
        }

        public async Task<string> GetUserByName(string id)
        {
            string getUserName = _appIdentityDbContext.Users.Where(x => x.Id == id).Select(x => x.UserName).FirstOrDefault();

            return getUserName;
        }

        public async Task<string> GetUserByRolName(string id)
        {
            string getUserRoleName = _appIdentityDbContext.Roles.Where(x => x.Id == id).Select(x => x.Name).FirstOrDefault();

            return getUserRoleName;
        }

        public async Task<IEnumerable<AppUser>> GetAllUserAsync()
        {
            return _appIdentityDbContext.Users.ToList();

        }


        public async Task<IEnumerable<IdentityUserRole<string>>> GetAllUserRole()
        {
            List<IdentityUserRole<string>> ıdentityUserRoles =  _appIdentityDbContext.UserRoles.ToList();
            return ıdentityUserRoles;
        }

    }
}



