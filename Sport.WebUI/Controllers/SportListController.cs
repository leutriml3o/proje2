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
    public class SportListController : Controller
    {
        private readonly ISportListService _sportListService;
        private readonly ISportDayService _sportDayService;
        private readonly IAreaService _areaService;
        private readonly IMovementService _movementService;
        private readonly SessionHelper _sessionHelper;
        private readonly IUserService _userService;
        private readonly IUserSportListsService _userSportListsService;



        public SportListController(ISportListService sportListService,
            ISportDayService sportDayService,
            IAreaService areaService,
            IMovementService movementService,
            SessionHelper sessionHelper,
            IUserService userService,
            IUserSportListsService userSportListsService)
        {
            _sportListService = sportListService;
            _sportDayService = sportDayService;
            _areaService = areaService;
            _movementService = movementService;
            _sessionHelper = sessionHelper;
            _userService = userService;
            _userSportListsService = userSportListsService;
        }
        public async Task<IActionResult> GetAllSportList()
        {
            IEnumerable<SportList> alLSportList = await _sportListService.GetAllSportListAsync();
            return View(alLSportList);
        }
        [Authorize(Roles = "Admin,User")]
        public IActionResult CreateSportList()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateSportList(SportList newList)
        {
            try
            {
                int success = await _sportListService.AddSportListAsync(newList);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return RedirectToAction("GetAllSportList");
        }
        public async Task<IActionResult> EditSportList(int Id)
        {
            SportList sportList = await _sportListService.SportListById(Id);
            return View(sportList);
        }

        [HttpPost]
        public async Task<IActionResult> EditSportList(SportList sportList)
        {
            int success = await _sportListService.EditSportListAsync(sportList);
            if (success < 0)
            {
                return NotFound();
            }
            return RedirectToAction("GetAllSportList");
        }

        public async Task<IActionResult> DetailsSportList(int Id)
        {
            SportList sportList = await _sportListService.SportListById(Id);
            return View(sportList);
        }
        public async Task<IActionResult> DeleteSportList(SportList newList)
        {
            int success = await _sportListService.DeleteSportListAsync(newList);
            if (success < 0)
            {
                return NotFound();
            }
            return RedirectToAction("GetAllSportList");
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public async Task<IActionResult> AddMovements(int Id)
        {
            SportList nList = await _sportListService.SportListById(Id);
            return View(nList);
        }
        [HttpPost]
        public async Task<IActionResult> AddMovementForArea(int bolgeGun, int sportListId)
        {
            int sayi = bolgeGun;
            int hangiOgun = (bolgeGun / 10);
            int hangiOgun1 = hangiOgun;
            hangiOgun--;
            sayi = sayi - (hangiOgun1 * 10);
            int hangiGun = sayi;
            hangiGun--;


            List<SportDay> getSDays = await _sportDayService.GetSportDaysBySportListId(sportListId);
            SportDay selectedDay = getSDays[hangiGun];

            List<Area> getDayAreas = await _areaService.GetAreasBySportDayId(selectedDay.Id);

            Area selectedArea = getDayAreas[hangiOgun];

            //return RedirectToAction("Movements", "SportList", new
            //{
            //    id = selectedArea.Id,
            //    hangiOgun = hangiOgun
            //});

            return RedirectToAction("Movements", "SportList", new { @id = selectedArea.Id });


        }

        public async Task<IActionResult> Movements(int id)
        {

            SelectMovementAndAreaViewModel vm = new SelectMovementAndAreaViewModel();
            //IEnumerable<Movement> areaForMovementList = await _movementService.GetAllMovementAsync();
            vm.allMovements = await _movementService.GetAllMovementAsync();
            vm.areaId = id;
            return View(vm);
        }

        [HttpPost]
        public async Task<JsonResult> MovementsPost(SelectMovementAndAreaViewModel jsonData)
        {
            await _sportListService.AddAreaMovements(jsonData.selectedMovementIdArray, jsonData.areaId);
            return Json(new { status = 1, redirect = "/SportList/GetAllSportList" });
        }

        public async Task<IActionResult> SportListView(int id)
        {
            IEnumerable<AreaMovements> alLSportList = await _sportListService.SportListDetailsView(id);
            return View(alLSportList);
        }

        public IActionResult QuestionsSport()
        {
            return View();
        }
        [HttpPost]
        public async Task<JsonResult> QuestionsSportResult(QuestionsSportViewModel jsonData)
        {
            string username = _sessionHelper.GetSessionUsername();
            AppUser user = await _userService.UserByName(username);
            if (jsonData.Bolge == 1)
            {
                int success = await _userSportListsService.AddUserSportListsAsync(user.Id, 1);
                if (success > 0)
                {
                    return Json(new { status = 1, message = "Listeniz Başarılı Bir Şekilde Oluşturulmuştur...!", redirect = "/UserSportList/UserSportListDetails" });
                }
                return Json(new { status = 0, message = "Liste Oluşturma Başarısız..!", redirect = "/SportList/QuestionsSport" });
            }
            else if (jsonData.Bolge == 2)
            {
                int success = await _userSportListsService.AddUserSportListsAsync(user.Id, 1);
                if (success > 0)
                {
                    return Json(new { status = 1, message = "Listeniz Başarılı Bir Şekilde Oluşturulmuştur...!", redirect = "/UserSportList/UserSportListDetails" });
                }
                return Json(new { status = 0, message = "Liste Oluşturma Başarısız..!", redirect = "/SportList/QuestionsSport" });
            }
            else if (jsonData.Bolge == 3)
            {
                int success = await _userSportListsService.AddUserSportListsAsync(user.Id, 2);
                if (success > 0)
                {
                    return Json(new { status = 1, message = "Listeniz Başarılı Bir Şekilde Oluşturulmuştur...!", redirect = "/UserSportList/UserSportListDetails" });
                }
                return Json(new { status = 0, message = "Liste Oluşturma Başarısız..!", redirect = "/SportList/QuestionsSport" });
            }
            else if (jsonData.Bolge == 4)
            {
                int success = await _userSportListsService.AddUserSportListsAsync(user.Id, 3);
                if (success > 0)
                {
                    return Json(new { status = 1, message = "Listeniz Başarılı Bir Şekilde Oluşturulmuştur...!", redirect = "/UserSportList/UserSportListDetails" });
                }
                return Json(new { status = 0, message = "Liste Oluşturma Başarısız..!", redirect = "/SportList/QuestionsSport" });
            }
            return Json(new { status = 2, message = "Bir Hata Oluştu..!", redirect = "/Home/Index" });
        }
    }
}
