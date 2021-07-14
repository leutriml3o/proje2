using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sport.Domain.Entities;
using Sport.Repository.Abstract;
using Sport.Service.Abstract;
using Sport.WebUI.Entities;
using Sport.WebUI.Models;

namespace Sport.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IFoodRepository _foodService;

        public HomeController(ILogger<HomeController> logger, IFoodRepository foodService)
        {
            _logger = logger;
            _foodService = foodService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public ActionResult Calculator()
        {
            var calorieCalculatorViewModel = new CalorieCalculatorViewModel
            {
                calorieCalculator = new CalorieCalculator(),
            };

            return View(calorieCalculatorViewModel);
        }

        [HttpPost]
        public ActionResult Calculator(CalorieCalculator calorieCalculator, ViewInfo viewInfo)
        {
            if (ModelState.IsValid)
            {
                CalorieCalculation hsb = new CalorieCalculation();
                ViewInfo results = hsb.Calculator(calorieCalculator, viewInfo);

                return RedirectToAction("ViewInfo", results);
            }

            return View();
        }

        public ActionResult ViewInfo(ViewInfo viewInfo)
        {
            var x = viewInfo;
            return View(x);
        }

        public IActionResult FatRate()
        {
            return View();
        }

        [HttpPost]
        public JsonResult FatRatePost(CalculateFatRate jsonData)
        {
            FatRate fatRate = new FatRate();
            double result = fatRate.FateRate(jsonData);
            return Json(result);
        }
    }
}
