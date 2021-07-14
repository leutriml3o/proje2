using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Sport.Domain;
using Sport.Domain.Entities;
using Sport.Service.Abstract;
using Sport.WebUI.Models;

namespace Sport.WebUI.Controllers
{
    public class ProfileController : Controller
    {
        private readonly SessionHelper _sessionHelper;
        private readonly IUserService _userService;
        private UserManager<AppUser> _userManager;
        public ProfileController(SessionHelper sessionHelper,
            IUserService userService,
            UserManager<AppUser> userManager)
        {
            _sessionHelper = sessionHelper;
            _userService = userService;
            _userManager = userManager;
        }
        public async Task<IActionResult> Profile()
        {
            string username = _sessionHelper.GetSessionUsername();
            AppUser appUser = await _userService.UserByName(username);
            ProfileInfo profileInfo = new ProfileInfo
            {
                Calorie = appUser.Calorie,
                Weight = appUser.Weight,
                Height = appUser.Height,
                FatRate = appUser.FatRate,
                Age = appUser.Age,
                Email=appUser.Email
            };

            return View(profileInfo);
        }
        public IActionResult ProfileSetting()
        {
            return View();
        }
        [HttpPost]
        public async Task<JsonResult> ProfileSettingAdd(ProfileInfo jsondata)
        {
            string username = _sessionHelper.GetSessionUsername();
            AppUser appUser = await _userService.UserByName(username);
            appUser.Calorie = jsondata.Calorie;
            appUser.Weight = jsondata.Weight;
            appUser.Height = jsondata.Height;
            appUser.FatRate = jsondata.FatRate;
            try
            {
                var result = await _userManager.UpdateAsync(appUser);
                if (result.Succeeded)
                {
                    return Json(new { status = 1, message = "Profil kaydınız başarılı...!", redirect = "/Profile/Profile" });
                }
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }
            return Json(new { status = 0, message = "Profil kaydı başarsızı...!", redirect = "/Profile/Profile" });
        }
    }
}