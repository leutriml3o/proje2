using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sport.Domain.Entities;
using Sport.Domain.Entities.MMRelation;
using Sport.Service.Abstract;
using Sport.Service.Abstract.MMRelation;
using Sport.WebUI.Models;
using Sport.WebUI.ViewModels;

namespace Sport.WebUI.Controllers
{
    public class UserSportListController : Controller
    {
        private readonly IUserSportListsService _userSportListsService;
        private readonly SessionHelper _sessionHelper;
        private ISportListService _sportListService;
        private IUserService _userService;
        public UserSportListController(IUserSportListsService userSportListsService,
            SessionHelper sessionHelper,
            ISportListService sportListService,
            IUserService userService)
        {
            _userSportListsService = userSportListsService;
            _sessionHelper = sessionHelper;
            _sportListService = sportListService;
            _userService = userService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> UserSportListDetails()
        {
            string username = _sessionHelper.GetSessionUsername();
            AppUser appUser = await _userService.UserByName(username);
            List<UserSportLists> userSportLists = await _userSportListsService.UserSportLists(appUser.Id);
            var count = userSportLists.LastOrDefault();
            IEnumerable<AreaMovements> sportLists = await _sportListService.SportListDetailsView(count.FKSportListId);
            UserSportListDetails userSportListDetails = new UserSportListDetails();
            userSportListDetails.sportLists = sportLists;
            userSportListDetails.appUser = appUser;
            return View(userSportListDetails);

            //UserSportLists userSportLists = await _userSportListsService.UserSportList(appUser.Id);
            //IEnumerable<AreaMovements> sportLists = await _sportListService.SportListDetailsView(userSportLists.FKSportListId);

            //UserSportListDetails userSportListDetails = new UserSportListDetails();
            //userSportListDetails.sportLists = sportLists;
            //userSportListDetails.appUser = appUser;
            //return View(userSportListDetails);
        }
    }
}