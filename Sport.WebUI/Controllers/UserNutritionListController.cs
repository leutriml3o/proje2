using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sport.Domain.Entities;
using Sport.Domain.Entities.MMRelation;
using Sport.Service.Abstract;
using Sport.Service.Abstract.MMRelation;
using Sport.WebUI.Models;
using Sport.WebUI.ViewModels;

namespace Sport.WebUI.Controllers
{
    [Authorize()]
    public class UserNutritionListController : Controller
    {
        private readonly IUserNutritionListsService _userNutritionListsService;
        private readonly SessionHelper _sessionHelper;
        private INutritionListService _nutritionListService;
        private IUserService _userService;
        public UserNutritionListController(IUserNutritionListsService userNutritionListsService,
            SessionHelper sessionHelper,
            INutritionListService nutritionListService,
            IUserService userService)
        {
            _userNutritionListsService = userNutritionListsService;
            _sessionHelper = sessionHelper;
            _nutritionListService = nutritionListService;
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View();

        }

        public async Task<IActionResult> UserNutritionListDetails()
        {
            string username = _sessionHelper.GetSessionUsername();
            AppUser appUser = await _userService.UserByName(username);
            List<UserNutritionLists> userNutritionList = await _userNutritionListsService.UserNutritionLists(appUser.Id);
            var count = userNutritionList.LastOrDefault();
            IEnumerable<MealFoods> mealFoods = await _nutritionListService.NutritionListDetailsView(count.FKNutritionListId);
            UserNutritionListDetails userNutritionListDetails = new UserNutritionListDetails();
            userNutritionListDetails.nutritionLists = mealFoods;
            userNutritionListDetails.appUser = appUser;

            return View(userNutritionListDetails);

        }
    }
}