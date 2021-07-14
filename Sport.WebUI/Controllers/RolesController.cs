using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sport.Domain.Entities;
using Sport.Service.Abstract;
using Sport.WebUI.Models;

namespace Sport.WebUI.Controllers
{
    public class RolesController : Controller
    {
        private readonly IUserService _userService;
        private UserManager<AppUser> _userManager;



        public RolesController(IUserService userService,
            UserManager<AppUser> userManager)
        {
            _userService = userService;
            _userManager = userManager;
        }

        public IActionResult AddRoles()
        {
            return View();
        }

        public async Task<JsonResult> AddRolesPost(string userName, string rolName)
        {
            AppUser appUser = await _userService.UserByName(userName);

            var result = await _userManager.IsInRoleAsync(appUser, rolName);//Rol kontrolu yapıyor
            if (result == false)
            {
                await _userManager.AddToRoleAsync(appUser, rolName);
                return Json(new { status = 1, message = "Rol Kaydı Başarılı...!", redirect = "/Home/Index" });
            }
            else
            {
                return Json(new { status = 0, message = "Aynı Rol Daha Önce Eklenmiş...!", redirect = "/Home/Index" });
            }
        }

        [HttpGet]
        public async Task<JsonResult> GetUserNameList()
        {
            IEnumerable<AppUser> users = await _userService.GetAllUserAsync();
            var userList = new SelectList(users, "Id", "UserName");
            return Json(userList);
        }

        public async Task<IActionResult> GetUserRoleList()
        {
            IEnumerable<IdentityUserRole<string>> identityUserRoles = await _userService.GetAllUserRole();
            List<string> userList = new List<string>();
            UserRolesViewModel userRolesViewModel = new UserRolesViewModel();
            foreach (var item in identityUserRoles)
            {
                //userList.Add(await _userService.GetUserByName(item.UserId));
                //userList.Add(await _userService.GetUserByRolName(item.RoleId));

                var userName = await _userService.GetUserByName(item.UserId);
                var rolName = await _userService.GetUserByRolName(item.RoleId);

                userRolesViewModel.userName.Add(userName);
                userRolesViewModel.userRolName.Add(rolName);
            }
            return View(userRolesViewModel);
        }



        //////////////////////////////////////////////////////////////////////777
        //public ViewResult Index() => View(_roleManager.Roles);

        //public IActionResult Create() => View();

        //[HttpPost]
        //public async Task<IActionResult> Create([Required]string name)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        IdentityResult result = await _roleManager.CreateAsync(new IdentityRole(name));
        //        if (result.Succeeded)
        //            return RedirectToAction("Index");
        //        else
        //            Errors(result);
        //    }
        //    return View(name);
        //}

        //[HttpPost]
        //public async Task<IActionResult> Delete(string id)
        //{
        //    IdentityRole role = await _roleManager.FindByIdAsync(id);
        //    if (role != null)
        //    {
        //        IdentityResult result = await _roleManager.DeleteAsync(role);
        //        if (result.Succeeded)
        //            return RedirectToAction("Index");
        //        else
        //            Errors(result);
        //    }
        //    else
        //        ModelState.AddModelError("", "No role found");
        //    return View("Index", _roleManager.Roles);
        //}

        //private void Errors(IdentityResult result)
        //{
        //    foreach (IdentityError error in result.Errors)
        //        ModelState.AddModelError("", error.Description);
        //}



        //public async Task<IActionResult> Update(string id)
        //{
        //    IdentityRole role = await _roleManager.FindByIdAsync(id);
        //    List<AppUser> members = new List<AppUser>();
        //    List<AppUser> nonMembers = new List<AppUser>();
        //    foreach (AppUser user in _userManager.Users)
        //    {
        //        var list = await _userManager.IsInRoleAsync(user, role.Name) ? members : nonMembers;
        //        list.Add(user);
        //    }
        //    return View(new RoleEdit
        //    {
        //        Role = role,
        //        Members = members,
        //        NonMembers = nonMembers
        //    });
        //}

        //[HttpPost]
        //public async Task<IActionResult> Update(RoleModification model)
        //{
        //    IdentityResult result;
        //    if (ModelState.IsValid)
        //    {
        //        foreach (string userId in model.AddIds ?? new string[] { })
        //        {
        //            AppUser user = await _userManager.FindByIdAsync(userId);
        //            if (user != null)
        //            {
        //                result = await _userManager.AddToRoleAsync(user, model.RoleName);
        //                if (!result.Succeeded)
        //                    Errors(result);
        //            }
        //        }
        //        foreach (string userId in model.DeleteIds ?? new string[] { })
        //        {
        //            AppUser user = await _userManager.FindByIdAsync(userId);
        //            if (user != null)
        //            {
        //                result = await _userManager.RemoveFromRoleAsync(user, model.RoleName);
        //                if (!result.Succeeded)
        //                    Errors(result);
        //            }
        //        }
        //    }

        //    if (ModelState.IsValid)
        //        return RedirectToAction(nameof(Index));
        //    else
        //        return await Update(model.RoleId);
        //}
    }


}
