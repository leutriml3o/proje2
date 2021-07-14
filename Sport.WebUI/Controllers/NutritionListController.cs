using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sport.Domain;
using Sport.Domain.Entities;
using Sport.Domain.Entities.MMRelation;
using Sport.Service.Abstract;
using Sport.Service.Abstract.MMRelation;
using Sport.WebUI.Models;
using Sport.WebUI.ViewModels;

namespace Sport.WebUI.Controllers
{
    [Authorize()]
    public class NutritionListController : Controller
    {
        private readonly IThatDayService _thatDayService;
        private readonly INutritionListService _nutritionListService;
        private readonly INutritionDayService _nutritionDayService;
        private readonly IFoodService _foodService;
        private readonly SessionHelper _sessionHelper;
        private readonly IUserService _userService;
        private readonly IUserNutritionListsService _userNutritionListService;


        public NutritionListController(IThatDayService thatDayService,
            INutritionListService nutritionListService,
            INutritionDayService nutritionDayService,
            IFoodService foodService,
            SessionHelper sessionHelper,
            IUserService userService,
            IUserNutritionListsService userNutritionListsService)
        {
            _nutritionListService = nutritionListService;
            _nutritionDayService = nutritionDayService;
            _thatDayService = thatDayService;
            _foodService = foodService;
            _sessionHelper = sessionHelper;
            _userService = userService;
            _userNutritionListService = userNutritionListsService;
        }
        public async Task<IActionResult> GetAllNutritionList()
        {
            IEnumerable<NutritionList> allNutritionList = await _nutritionListService.GetAllNutritionListAsync();
            return View(allNutritionList);
        }
        [Authorize(Roles = "Admin,User")]
        public IActionResult CreateNutritionList()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateNutritionList(NutritionList newList)
        {
            try
            {
                int success = await _nutritionListService.AddNutritionListAsync(newList);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return RedirectToAction("GetAllNutritionList");
        }
        public async Task<IActionResult> EditNutritionList(int Id)
        {
            NutritionList nutritionList = await _nutritionListService.NutritionListById(Id);
            return View(nutritionList);
        }

        [HttpPost]
        public async Task<IActionResult> EditNutritionList(NutritionList nutritionList)
        {
            int success = await _nutritionListService.EditNutritionListAsync(nutritionList);
            if (success < 0)
            {
                return NotFound();
            }
            return RedirectToAction("GetAllNutritionList");
        }

        public async Task<IActionResult> DetailsNutritionList(int Id)
        {
            NutritionList newList = await _nutritionListService.NutritionListById(Id);
            return View(newList);
        }
        public async Task<IActionResult> DeleteNutritionList(NutritionList newList)
        {
            int success = await _nutritionListService.DeleteNutritionListAsync(newList);
            if (success < 0)
            {
                return NotFound();
            }
            return RedirectToAction("GetAllNutritionList");
        }


        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public async Task<IActionResult> AddFoods(int Id)
        {
            NutritionList nList = await _nutritionListService.NutritionListById(Id);
            return View(nList);
        }
        [HttpPost]
        public async Task<IActionResult> AddFoodForThat(int ogunGun, int nutritionListId)
        {
            int sayi = ogunGun;
            int hangiOgun = (ogunGun / 10);
            int hangiOgun1 = hangiOgun;
            hangiOgun--;
            sayi = sayi - (hangiOgun1 * 10);
            int hangiGun = sayi;
            hangiGun--;


            List<NutritionDay> getNDays = await _nutritionDayService.GetNutritionDaysByNutritionListId(nutritionListId);
            NutritionDay selectedDay = getNDays[hangiGun];

            List<ThatDay> getDayThats = await _thatDayService.GetThatDaysByNutritionDayId(selectedDay.Id);

            ThatDay selectedThat = getDayThats[hangiOgun];

            return RedirectToAction("Foods", "NutritionList", new { @id = selectedThat.Id });

        }

        public async Task<IActionResult> Foods(int id)
        {
            SelectFoodsAndThatViewModel vm = new SelectFoodsAndThatViewModel();
            vm.allFoods = await _foodService.GetAllFoodAsync();
            vm.thatId = id;
            return View(vm);
        }

        [HttpPost]
        public async Task<JsonResult> FoodsPost(SelectFoodsAndThatViewModel jsonData)
        {
            await _nutritionListService.AddThatFoods(jsonData.selectedFoodIdArray, jsonData.thatId);   
            return Json(new { status = 1, redirect = "/NutritionList/GetAllNutritionList" });
        }


        public async Task<IActionResult> NutritionListView(int id)
        {
            IEnumerable<MealFoods> alLSportList = await _nutritionListService.NutritionListDetailsView(id);
            return View(alLSportList);
        }

        public IActionResult Questions()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> QuestionsResult(QuestionsViewModel jsonData)
        {
            string username = _sessionHelper.GetSessionUsername();
            //AppUser appUser = await _userManager.get(username);
            AppUser user = await _userService.UserByName(username);
            double calori = user.Calorie;


            if (calori == 0)
            {
                return Json(new { status = 3, message = "Lütfen Kalorinizi Giriniz..!", redirect = "/Profile/ProfileSetting" });
            }

            if (calori <= 1500 )
            {
                return Json(new { status = 3, message = "1500 altında kolori miktari olamaz..!", redirect = "/Profile/ProfileSetting" });
            }

            if (jsonData.Vejeteryan == 1)
            {
                if (jsonData.BesinDegeri == 1)
                {
                    if (calori >= 1500 && calori <= 1800)
                    {
                        int success = await _userNutritionListService.AddUserNutritionListsAsync(user.Id, 27);
                        //bool thereIs = await _userNutritionListService.UserNutritionListIsThere(user.Id, 18);
                        //if (thereIs == true)
                        //{
                        //    return Json(new { status = 4, message = "Beslenme Listeniz zaten güncel !", redirect = "/NutritionList/Questions" });
                        //}

                        if (success > 0)
                        {
                            return Json(new { status = 1, message = "Listeniz Başarılı Bir Şekilde Oluşturulmuştur...!", redirect = "/UserNutritionList/UserNutritionListDetails" });
                        }
                        return Json(new { status = 0, message = "Liste Oluşturma Başarısız..!", redirect = "/NutritionList/Questions" });
                    }
                    else if (calori > 1800 && calori <= 1900)
                    {
                        int success = await _userNutritionListService.AddUserNutritionListsAsync(user.Id, 28);
                        //bool thereIs = await _userNutritionListService.UserNutritionListIsThere(user.Id, 21);
                        //if (thereIs == true)
                        //{
                        //    return Json(new { status = 4, message = "Beslenme Listeniz zaten güncel !", redirect = "/NutritionList/Questions" });
                        //}

                        if (success > 0)
                        {
                            return Json(new { status = 1, message = "Listeniz Başarılı Bir Şekilde Oluşturulmuştur...!", redirect = "/UserNutritionList/UserNutritionListDetails" });
                        }
                        return Json(new { status = 0, message = "Liste Oluşturma Başarısız..!", redirect = "/NutritionList/Questions" });
                    }
                    else if (calori > 1900 && calori <= 2000)
                    {
                        int success = await _userNutritionListService.AddUserNutritionListsAsync(user.Id, 29);
                        //bool thereIs = await _userNutritionListService.UserNutritionListIsThere(user.Id, 24);
                        //if (thereIs == true)
                        //{
                        //    return Json(new { status = 4, message = "Beslenme Listeniz zaten güncel !", redirect = "/NutritionList/Questions" });
                        //}

                        if (success > 0)
                        {
                            return Json(new { status = 1, message = "Listeniz Başarılı Bir Şekilde Oluşturulmuştur...!", redirect = "/UserNutritionList/UserNutritionListDetails" });
                        }
                        return Json(new { status = 0, message = "Liste Oluşturma Başarısız..!", redirect = "/NutritionList/Questions" });
                    }
                    else if (calori > 2000 && calori <= 2100)
                    {
                        int success = await _userNutritionListService.AddUserNutritionListsAsync(user.Id, 36);
                        //bool thereIs = await _userNutritionListService.UserNutritionListIsThere(user.Id, 9);
                        //if (thereIs == true)
                        //{
                        //    return Json(new { status = 4, message = "Beslenme Listeniz zaten güncel !", redirect = "/NutritionList/Questions" });
                        //}

                        if (success > 0)
                        {
                            return Json(new { status = 1, message = "Listeniz Başarılı Bir Şekilde Oluşturulmuştur...!", redirect = "/UserNutritionList/UserNutritionListDetails" });
                        }
                        return Json(new { status = 0, message = "Liste Oluşturma Başarısız..!", redirect = "/NutritionList/Questions" });
                    }
                    else if (calori > 2100 && calori <= 2200)
                    {
                        int success = await _userNutritionListService.AddUserNutritionListsAsync(user.Id, 39);
                        //bool thereIs = await _userNutritionListService.UserNutritionListIsThere(user.Id, 12);
                        //if (thereIs == true)
                        //{
                        //    return Json(new { status = 4, message = "Beslenme Listeniz zaten güncel !", redirect = "/NutritionList/Questions" });
                        //}

                        if (success > 0)
                        {
                            return Json(new { status = 1, message = "Listeniz Başarılı Bir Şekilde Oluşturulmuştur...!", redirect = "/UserNutritionList/UserNutritionListDetails" });
                        }
                        return Json(new { status = 0, message = "Liste Oluşturma Başarısız..!", redirect = "/NutritionList/Questions" });
                    }
                    else if (calori > 2200)
                    {
                        int success = await _userNutritionListService.AddUserNutritionListsAsync(user.Id, 42);
                        //bool thereIs = await _userNutritionListService.UserNutritionListIsThere(user.Id, 15);
                        //if (thereIs == true)
                        //{
                        //    return Json(new { status = 4, message = "Beslenme Listeniz zaten güncel !", redirect = "/NutritionList/Questions" });
                        //}

                        if (success > 0)
                        {
                            return Json(new { status = 1, message = "Listeniz Başarılı Bir Şekilde Oluşturulmuştur...!", redirect = "/UserNutritionList/UserNutritionListDetails" });
                        }
                        return Json(new { status = 0, message = "Liste Oluşturma Başarısız..!", redirect = "/NutritionList/Questions" });
                    }
                }
                else if (jsonData.BesinDegeri == 2)
                {
                    if (calori >= 1700 && calori < 1800)
                    {
                        int success = await _userNutritionListService.AddUserNutritionListsAsync(user.Id, 30);
                        //bool thereIs = await _userNutritionListService.UserNutritionListIsThere(user.Id, 19);
                        //if (thereIs == true)
                        //{
                        //    return Json(new { status = 4, message = "Beslenme Listeniz zaten güncel !", redirect = "/NutritionList/Questions" });
                        //}

                        if (success > 0)
                        {
                            return Json(new { status = 1, message = "Listeniz Başarılı Bir Şekilde Oluşturulmuştur...!", redirect = "/UserNutritionList/UserNutritionListDetails" });
                        }
                        return Json(new { status = 0, message = "Liste Oluşturma Başarısız..!", redirect = "/NutritionList/Questions" });
                    }
                    else if (calori > 1800 && calori <= 1900) 
                    {
                        int success = await _userNutritionListService.AddUserNutritionListsAsync(user.Id, 32);
                        //bool thereIs = await _userNutritionListService.UserNutritionListIsThere(user.Id, 22);
                        //if (thereIs == true)
                        //{
                        //    return Json(new { status = 4, message = "Beslenme Listeniz zaten güncel !", redirect = "/NutritionList/Questions" });
                        //}

                        if (success > 0)
                        {
                            return Json(new { status = 1, message = "Listeniz Başarılı Bir Şekilde Oluşturulmuştur...!", redirect = "/UserNutritionList/UserNutritionListDetails" });
                        }
                        return Json(new { status = 0, message = "Liste Oluşturma Başarısız..!", redirect = "/NutritionList/Questions" });
                    }
                    else if (calori > 1900 && calori <= 2000)
                    {
                        int success = await _userNutritionListService.AddUserNutritionListsAsync(user.Id, 34);
                        //bool thereIs = await _userNutritionListService.UserNutritionListIsThere(user.Id, 25);
                        //if (thereIs == true)
                        //{
                        //    return Json(new { status = 4, message = "Beslenme Listeniz zaten güncel !", redirect = "/NutritionList/Questions" });
                        //}

                        if (success > 0)
                        {
                            return Json(new { status = 1, message = "Listeniz Başarılı Bir Şekilde Oluşturulmuştur...!", redirect = "/UserNutritionList/UserNutritionListDetails" });
                        }
                        return Json(new { status = 0, message = "Liste Oluşturma Başarısız..!", redirect = "/NutritionList/Questions" });
                    }
                    else if (calori > 2000 && calori <= 2100)
                    {
                        int success = await _userNutritionListService.AddUserNutritionListsAsync(user.Id, 37);
                        //bool thereIs = await _userNutritionListService.UserNutritionListIsThere(user.Id, 10);
                        //if (thereIs == true)
                        //{
                        //    return Json(new { status = 4, message = "Beslenme Listeniz zaten güncel !", redirect = "/NutritionList/Questions" });
                        //}

                        if (success > 0)
                        {
                            return Json(new { status = 1, message = "Listeniz Başarılı Bir Şekilde Oluşturulmuştur...!", redirect = "/UserNutritionList/UserNutritionListDetails" });
                        }
                        return Json(new { status = 0, message = "Liste Oluşturma Başarısız..!", redirect = "/NutritionList/Questions" });
                    }
                    else if (calori > 2100 && calori <= 2200)
                    {
                        int success = await _userNutritionListService.AddUserNutritionListsAsync(user.Id, 40);
                        //bool thereIs = await _userNutritionListService.UserNutritionListIsThere(user.Id, 13);
                        //if (thereIs == true)
                        //{
                        //    return Json(new { status = 4, message = "Beslenme Listeniz zaten güncel !", redirect = "/NutritionList/Questions" });
                        //}

                        if (success > 0)
                        {
                            return Json(new { status = 1, message = "Listeniz Başarılı Bir Şekilde Oluşturulmuştur...!", redirect = "/UserNutritionList/UserNutritionListDetails" });
                        }
                        return Json(new { status = 0, message = "Liste Oluşturma Başarısız..!", redirect = "/NutritionList/Questions" });
                    }
                    else if (calori > 2200)
                    {
                        int success = await _userNutritionListService.AddUserNutritionListsAsync(user.Id, 43);
                        //bool thereIs = await _userNutritionListService.UserNutritionListIsThere(user.Id, 16);
                        //if (thereIs == true)
                        //{
                        //    return Json(new { status = 4, message = "Beslenme Listeniz zaten güncel !", redirect = "/NutritionList/Questions" });
                        //}

                        if (success > 0)
                        {
                            return Json(new { status = 1, message = "Listeniz Başarılı Bir Şekilde Oluşturulmuştur...!", redirect = "/UserNutritionList/UserNutritionListDetails" });
                        }
                        return Json(new { status = 0, message = "Liste Oluşturma Başarısız..!", redirect = "/NutritionList/Questions" });
                    }
                }
                else if (jsonData.BesinDegeri == 3)
                {
                    if (calori >= 1700 && calori < 1800)
                    {
                        int success = await _userNutritionListService.AddUserNutritionListsAsync(user.Id, 31);
                        //bool thereIs = await _userNutritionListService.UserNutritionListIsThere(user.Id, 18);
                        //if (thereIs == true)
                        //{
                        //    return Json(new { status = 4, message = "Beslenme Listeniz zaten güncel !", redirect = "/NutritionList/Questions" });
                        //}

                        if (success > 0)
                        {
                            return Json(new { status = 1, message = "Listeniz Başarılı Bir Şekilde Oluşturulmuştur...!", redirect = "/UserNutritionList/UserNutritionListDetails" });
                        }
                        return Json(new { status = 0, message = "Liste Oluşturma Başarısız..!", redirect = "/NutritionList/Questions" });
                    }
                    else if (calori > 1800 && calori <= 1900)
                    {
                        int success = await _userNutritionListService.AddUserNutritionListsAsync(user.Id, 33);
                        //bool thereIs = await _userNutritionListService.UserNutritionListIsThere(user.Id, 23);
                        //if (thereIs == true)
                        //{
                        //    return Json(new { status = 4, message = "Beslenme Listeniz zaten güncel !", redirect = "/NutritionList/Questions" });
                        //}

                        if (success > 0)
                        {
                            return Json(new { status = 1, message = "Listeniz Başarılı Bir Şekilde Oluşturulmuştur...!", redirect = "/UserNutritionList/UserNutritionListDetails" });
                        }
                        return Json(new { status = 0, message = "Liste Oluşturma Başarısız..!", redirect = "/NutritionList/Questions" });
                    }
                    else if (calori > 1900 && calori <= 2000)
                    {
                        int success = await _userNutritionListService.AddUserNutritionListsAsync(user.Id, 35);
                        //bool thereIs = await _userNutritionListService.UserNutritionListIsThere(user.Id, 26);
                        //if (thereIs == true)
                        //{
                        //    return Json(new { status = 4, message = "Beslenme Listeniz zaten güncel !", redirect = "/NutritionList/Questions" });
                        //}

                        if (success > 0)
                        {
                            return Json(new { status = 1, message = "Listeniz Başarılı Bir Şekilde Oluşturulmuştur...!", redirect = "/UserNutritionList/UserNutritionListDetails" });
                        }
                        return Json(new { status = 0, message = "Liste Oluşturma Başarısız..!", redirect = "/NutritionList/Questions" });
                    }
                    else if (calori > 2000 && calori <= 2100)
                    {
                        int success = await _userNutritionListService.AddUserNutritionListsAsync(user.Id, 38);
                        //bool thereIs = await _userNutritionListService.UserNutritionListIsThere(user.Id, 11);
                        //if (thereIs == true)
                        //{
                        //    return Json(new { status = 4, message = "Beslenme Listeniz zaten güncel !", redirect = "/NutritionList/Questions" });
                        //}

                        if (success > 0)
                        {
                            return Json(new { status = 1, message = "Listeniz Başarılı Bir Şekilde Oluşturulmuştur...!", redirect = "/UserNutritionList/UserNutritionListDetails" });
                        }
                        return Json(new { status = 0, message = "Liste Oluşturma Başarısız..!", redirect = "/NutritionList/Questions" });
                    }
                    else if (calori > 2100 && calori <= 2200)
                    {
                        int success = await _userNutritionListService.AddUserNutritionListsAsync(user.Id, 41);
                        //bool thereIs = await _userNutritionListService.UserNutritionListIsThere(user.Id, 14);
                        //if (thereIs == true)
                        //{
                        //    return Json(new { status = 4, message = "Beslenme Listeniz zaten güncel !", redirect = "/NutritionList/Questions" });
                        //}

                        if (success > 0)
                        {
                            return Json(new { status = 1, message = "Listeniz Başarılı Bir Şekilde Oluşturulmuştur...!", redirect = "/UserNutritionList/UserNutritionListDetails" });
                        }
                        return Json(new { status = 0, message = "Liste Oluşturma Başarısız..!", redirect = "/NutritionList/Questions" });
                    }
                    else if (calori > 2200)
                    {
                        int success = await _userNutritionListService.AddUserNutritionListsAsync(user.Id, 44);
                        //bool thereIs = await _userNutritionListService.UserNutritionListIsThere(user.Id, 17);
                        //if (thereIs == true)
                        //{
                        //    return Json(new { status = 4, message = "Beslenme Listeniz zaten güncel !", redirect = "/NutritionList/Questions" });
                        //}

                        if (success > 0)
                        {
                            return Json(new { status = 1, message = "Listeniz Başarılı Bir Şekilde Oluşturulmuştur...!", redirect = "/UserNutritionList/UserNutritionListDetails" });
                        }
                        return Json(new { status = 0, message = "Liste Oluşturma Başarısız..!", redirect = "/NutritionList/Questions" });
                    }
                }
            }
            else if (jsonData.Vejeteryan == 2)
            {
                if (jsonData.BesinDegeri == 1)
                {
                    if (calori >= 1700 && calori < 1800)
                    {
                        int success = await _userNutritionListService.AddUserNutritionListsAsync(user.Id, 18);
                        //bool thereIs = await _userNutritionListService.UserNutritionListIsThere(user.Id, 18);
                        //if (thereIs == true)
                        //{
                        //    return Json(new { status = 4, message = "Beslenme Listeniz zaten güncel !", redirect = "/NutritionList/Questions" });
                        //}

                        if (success > 0)
                        {
                            return Json(new { status = 1, message = "Listeniz Başarılı Bir Şekilde Oluşturulmuştur...!", redirect = "/UserNutritionList/UserNutritionListDetails" });
                        }
                        return Json(new { status = 0, message = "Liste Oluşturma Başarısız..!", redirect = "/NutritionList/Questions" });
                    }
                    else if (calori > 1800 && calori <= 1900)
                    {
                        int success = await _userNutritionListService.AddUserNutritionListsAsync(user.Id, 21);
                        //bool thereIs = await _userNutritionListService.UserNutritionListIsThere(user.Id, 21);
                        //if (thereIs == true)
                        //{
                        //    return Json(new { status = 4, message = "Beslenme Listeniz zaten güncel !", redirect = "/NutritionList/Questions" });
                        //}

                        if (success > 0)
                        {
                            return Json(new { status = 1, message = "Listeniz Başarılı Bir Şekilde Oluşturulmuştur...!", redirect = "/UserNutritionList/UserNutritionListDetails" });
                        }
                        return Json(new { status = 0, message = "Liste Oluşturma Başarısız..!", redirect = "/NutritionList/Questions" });
                    }
                    else if (calori > 1900 && calori <= 2000)
                    {
                        int success = await _userNutritionListService.AddUserNutritionListsAsync(user.Id, 24);
                        //bool thereIs = await _userNutritionListService.UserNutritionListIsThere(user.Id, 24);
                        //if (thereIs == true)
                        //{
                        //    return Json(new { status = 4, message = "Beslenme Listeniz zaten güncel !", redirect = "/NutritionList/Questions" });
                        //}

                        if (success > 0)
                        {
                            return Json(new { status = 1, message = "Listeniz Başarılı Bir Şekilde Oluşturulmuştur...!", redirect = "/UserNutritionList/UserNutritionListDetails" });
                        }
                        return Json(new { status = 0, message = "Liste Oluşturma Başarısız..!", redirect = "/NutritionList/Questions" });
                    }
                    else if (calori > 2000 && calori <= 2100)
                    {
                        int success = await _userNutritionListService.AddUserNutritionListsAsync(user.Id, 9);
                        //bool thereIs = await _userNutritionListService.UserNutritionListIsThere(user.Id, 9);
                        //if (thereIs == true)
                        //{
                        //    return Json(new { status = 4, message = "Beslenme Listeniz zaten güncel !", redirect = "/NutritionList/Questions" });
                        //}

                        if (success > 0)
                        {
                            return Json(new { status = 1, message = "Listeniz Başarılı Bir Şekilde Oluşturulmuştur...!", redirect = "/UserNutritionList/UserNutritionListDetails" });
                        }
                        return Json(new { status = 0, message = "Liste Oluşturma Başarısız..!", redirect = "/NutritionList/Questions" });
                    }
                    else if (calori > 2100 && calori <= 2200)
                    {
                        int success = await _userNutritionListService.AddUserNutritionListsAsync(user.Id, 12);
                        //bool thereIs = await _userNutritionListService.UserNutritionListIsThere(user.Id, 12);
                        //if (thereIs == true)
                        //{
                        //    return Json(new { status = 4, message = "Beslenme Listeniz zaten güncel !", redirect = "/NutritionList/Questions" });
                        //}

                        if (success > 0)
                        {
                            return Json(new { status = 1, message = "Listeniz Başarılı Bir Şekilde Oluşturulmuştur...!", redirect = "/UserNutritionList/UserNutritionListDetails" });
                        }
                        return Json(new { status = 0, message = "Liste Oluşturma Başarısız..!", redirect = "/NutritionList/Questions" });
                    }
                    else if (calori > 2200)
                    {
                        int success = await _userNutritionListService.AddUserNutritionListsAsync(user.Id, 15);
                        //bool thereIs = await _userNutritionListService.UserNutritionListIsThere(user.Id, 15);
                        //if (thereIs == true)
                        //{
                        //    return Json(new { status = 4, message = "Beslenme Listeniz zaten güncel !", redirect = "/NutritionList/Questions" });
                        //}

                        if (success > 0)
                        {
                            return Json(new { status = 1, message = "Listeniz Başarılı Bir Şekilde Oluşturulmuştur...!", redirect = "/UserNutritionList/UserNutritionListDetails" });
                        }
                        return Json(new { status = 0, message = "Liste Oluşturma Başarısız..!", redirect = "/NutritionList/Questions" });
                    }
                }
                else if (jsonData.BesinDegeri == 2)
                {
                    if (calori >= 1700 && calori < 1800)
                    {
                        int success = await _userNutritionListService.AddUserNutritionListsAsync(user.Id, 19);
                        //bool thereIs = await _userNutritionListService.UserNutritionListIsThere(user.Id, 19);
                        //if (thereIs == true)
                        //{
                        //    return Json(new { status = 4, message = "Beslenme Listeniz zaten güncel !", redirect = "/NutritionList/Questions" });
                        //}

                        if (success > 0)
                        {
                            return Json(new { status = 1, message = "Listeniz Başarılı Bir Şekilde Oluşturulmuştur...!", redirect = "/UserNutritionList/UserNutritionListDetails" });
                        }
                        return Json(new { status = 0, message = "Liste Oluşturma Başarısız..!", redirect = "/NutritionList/Questions" });
                    }
                    else if (calori > 1800 && calori <= 1900)
                    {
                        int success = await _userNutritionListService.AddUserNutritionListsAsync(user.Id, 22);
                        //bool thereIs = await _userNutritionListService.UserNutritionListIsThere(user.Id, 22);
                        //if (thereIs == true)
                        //{
                        //    return Json(new { status = 4, message = "Beslenme Listeniz zaten güncel !", redirect = "/NutritionList/Questions" });
                        //}

                        if (success > 0)
                        {
                            return Json(new { status = 1, message = "Listeniz Başarılı Bir Şekilde Oluşturulmuştur...!", redirect = "/UserNutritionList/UserNutritionListDetails" });
                        }
                        return Json(new { status = 0, message = "Liste Oluşturma Başarısız..!", redirect = "/NutritionList/Questions" });
                    }
                    else if (calori > 1900 && calori <= 2000)
                    {
                        int success = await _userNutritionListService.AddUserNutritionListsAsync(user.Id, 25);
                        //bool thereIs = await _userNutritionListService.UserNutritionListIsThere(user.Id, 25);
                        //if (thereIs == true)
                        //{
                        //    return Json(new { status = 4, message = "Beslenme Listeniz zaten güncel !", redirect = "/NutritionList/Questions" });
                        //}

                        if (success > 0)
                        {
                            return Json(new { status = 1, message = "Listeniz Başarılı Bir Şekilde Oluşturulmuştur...!", redirect = "/UserNutritionList/UserNutritionListDetails" });
                        }
                        return Json(new { status = 0, message = "Liste Oluşturma Başarısız..!", redirect = "/NutritionList/Questions" });
                    }
                    else if (calori > 2000 && calori <= 2100)
                    {
                        int success = await _userNutritionListService.AddUserNutritionListsAsync(user.Id, 10);
                        //bool thereIs = await _userNutritionListService.UserNutritionListIsThere(user.Id, 10);
                        //if (thereIs == true)
                        //{
                        //    return Json(new { status = 4, message = "Beslenme Listeniz zaten güncel !", redirect = "/NutritionList/Questions" });
                        //}

                        if (success > 0)
                        {
                            return Json(new { status = 1, message = "Listeniz Başarılı Bir Şekilde Oluşturulmuştur...!", redirect = "/UserNutritionList/UserNutritionListDetails" });
                        }
                        return Json(new { status = 0, message = "Liste Oluşturma Başarısız..!", redirect = "/NutritionList/Questions" });
                    }
                    else if (calori > 2100 && calori <= 2200)
                    {
                        int success = await _userNutritionListService.AddUserNutritionListsAsync(user.Id, 13);
                        //bool thereIs = await _userNutritionListService.UserNutritionListIsThere(user.Id, 13);
                        //if (thereIs == true)
                        //{
                        //    return Json(new { status = 4, message = "Beslenme Listeniz zaten güncel !", redirect = "/NutritionList/Questions" });
                        //}

                        if (success > 0)
                        {
                            return Json(new { status = 1, message = "Listeniz Başarılı Bir Şekilde Oluşturulmuştur...!", redirect = "/UserNutritionList/UserNutritionListDetails" });
                        }
                        return Json(new { status = 0, message = "Liste Oluşturma Başarısız..!", redirect = "/NutritionList/Questions" });
                    }
                    else if (calori > 2200)
                    {
                        int success = await _userNutritionListService.AddUserNutritionListsAsync(user.Id, 16);
                        //bool thereIs = await _userNutritionListService.UserNutritionListIsThere(user.Id, 16);
                        //if (thereIs == true)
                        //{
                        //    return Json(new { status = 4, message = "Beslenme Listeniz zaten güncel !", redirect = "/NutritionList/Questions" });
                        //}

                        if (success > 0)
                        {
                            return Json(new { status = 1, message = "Listeniz Başarılı Bir Şekilde Oluşturulmuştur...!", redirect = "/UserNutritionList/UserNutritionListDetails" });
                        }
                        return Json(new { status = 0, message = "Liste Oluşturma Başarısız..!", redirect = "/NutritionList/Questions" });
                    }
                }
                else if (jsonData.BesinDegeri == 3)
                {
                    if (calori >= 1700 && calori < 1800)
                    {
                        int success = await _userNutritionListService.AddUserNutritionListsAsync(user.Id, 20);
                        //bool thereIs = await _userNutritionListService.UserNutritionListIsThere(user.Id, 18);
                        //if (thereIs == true)
                        //{
                        //    return Json(new { status = 4, message = "Beslenme Listeniz zaten güncel !", redirect = "/NutritionList/Questions" });
                        //}

                        if (success > 0)
                        {
                            return Json(new { status = 1, message = "Listeniz Başarılı Bir Şekilde Oluşturulmuştur...!", redirect = "/UserNutritionList/UserNutritionListDetails" });
                        }
                        return Json(new { status = 0, message = "Liste Oluşturma Başarısız..!", redirect = "/NutritionList/Questions" });
                    }
                    else if (calori > 1800 && calori <= 1900)
                    {
                        int success = await _userNutritionListService.AddUserNutritionListsAsync(user.Id, 23);
                        //bool thereIs = await _userNutritionListService.UserNutritionListIsThere(user.Id, 23);
                        //if (thereIs == true)
                        //{
                        //    return Json(new { status = 4, message = "Beslenme Listeniz zaten güncel !", redirect = "/NutritionList/Questions" });
                        //}

                        if (success > 0)
                        {
                            return Json(new { status = 1, message = "Listeniz Başarılı Bir Şekilde Oluşturulmuştur...!", redirect = "/UserNutritionList/UserNutritionListDetails" });
                        }
                        return Json(new { status = 0, message = "Liste Oluşturma Başarısız..!", redirect = "/NutritionList/Questions" });
                    }
                    else if (calori > 1900 && calori <= 2000)
                    {
                        int success = await _userNutritionListService.AddUserNutritionListsAsync(user.Id, 26);
                        //bool thereIs = await _userNutritionListService.UserNutritionListIsThere(user.Id, 26);
                        //if (thereIs == true)
                        //{
                        //    return Json(new { status = 4, message = "Beslenme Listeniz zaten güncel !", redirect = "/NutritionList/Questions" });
                        //}

                        if (success > 0)
                        {
                            return Json(new { status = 1, message = "Listeniz Başarılı Bir Şekilde Oluşturulmuştur...!", redirect = "/UserNutritionList/UserNutritionListDetails" });
                        }
                        return Json(new { status = 0, message = "Liste Oluşturma Başarısız..!", redirect = "/NutritionList/Questions" });
                    }
                    else if (calori > 2000 && calori <= 2100)
                    {
                        int success = await _userNutritionListService.AddUserNutritionListsAsync(user.Id, 11);
                        //bool thereIs = await _userNutritionListService.UserNutritionListIsThere(user.Id, 11);
                        //if (thereIs == true)
                        //{
                        //    return Json(new { status = 4, message = "Beslenme Listeniz zaten güncel !", redirect = "/NutritionList/Questions" });
                        //}

                        if (success > 0)
                        {
                            return Json(new { status = 1, message = "Listeniz Başarılı Bir Şekilde Oluşturulmuştur...!", redirect = "/UserNutritionList/UserNutritionListDetails" });
                        }
                        return Json(new { status = 0, message = "Liste Oluşturma Başarısız..!", redirect = "/NutritionList/Questions" });
                    }
                    else if (calori > 2100 && calori <= 2200)
                    {
                        int success = await _userNutritionListService.AddUserNutritionListsAsync(user.Id, 14);
                        //bool thereIs = await _userNutritionListService.UserNutritionListIsThere(user.Id, 14);
                        //if (thereIs == true)
                        //{
                        //    return Json(new { status = 4, message = "Beslenme Listeniz zaten güncel !", redirect = "/NutritionList/Questions" });
                        //}

                        if (success > 0)
                        {
                            return Json(new { status = 1, message = "Listeniz Başarılı Bir Şekilde Oluşturulmuştur...!", redirect = "/UserNutritionList/UserNutritionListDetails" });
                        }
                        return Json(new { status = 0, message = "Liste Oluşturma Başarısız..!", redirect = "/NutritionList/Questions" });
                    }
                    else if (calori > 2200)
                    {
                        int success = await _userNutritionListService.AddUserNutritionListsAsync(user.Id, 17);
                        //bool thereIs = await _userNutritionListService.UserNutritionListIsThere(user.Id, 17);
                        //if (thereIs == true)
                        //{
                        //    return Json(new { status = 4, message = "Beslenme Listeniz zaten güncel !", redirect = "/NutritionList/Questions" });
                        //}

                        if (success > 0)
                        {
                            return Json(new { status = 1, message = "Listeniz Başarılı Bir Şekilde Oluşturulmuştur...!", redirect = "/UserNutritionList/UserNutritionListDetails" });
                        }
                        return Json(new { status = 0, message = "Liste Oluşturma Başarısız..!", redirect = "/NutritionList/Questions" });
                    }
                }
            }

            return Json(new { status = 2, message = "Bir Hata Oluştu..!", redirect = "/NutritionList/Questions" });
        }

    }
}
