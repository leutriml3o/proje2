using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sport.Domain.Entities;
using Sport.Service.Abstract;
using Sport.WebUI.Models;
using System;
using System.Threading.Tasks;

namespace Sport.WebUI.Controllers
{
    [Authorize()]
    public class FoodsController : Controller
    {
        private readonly IFoodService _foodService;
        private readonly INutritionListService _nutritionListService;

        public FoodsController(IFoodService foodService,
            INutritionListService nutritionListService)
        {
            _foodService = foodService;
            _nutritionListService = nutritionListService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _foodService.GetAllFoodAsync());
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View(new Food());
        }
        [HttpPost]
        public async Task<IActionResult> Create(Food food)
        {
            int success = await _foodService.AddFoodAsync(food);

            if(success<0)
            {
                return NotFound();
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(Food food)
        {
            int success = await _foodService.DeleteFoodAsync(food);
            if(success < 0)
            {
                return NotFound();
            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Edit(int Id)
        {
            Food food = await _foodService.FoodById(Id);
            return View(food);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Food food)
        {
            int success= await _foodService.EditFoodAsync(food);
            if (success < 0)
            {
                return NotFound();
            }
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> Details(int Id)
        {
            Food food = await _foodService.FoodById(Id);
            return View(food);
        }
        public async Task<IActionResult> Index2()
        {
            return View(await _nutritionListService.GetAllNutritionListAsync());
        }

        public IActionResult Create2()
        {
            return View(new NutritionList());
        }
        [HttpPost]
        public async Task<IActionResult> Create2(NutritionList nutritionList)
        {
            int success = await _nutritionListService.AddNutritionListAsync(nutritionList);

            if (success < 0)
            {
                return NotFound();
            }

            return RedirectToAction("Index2");
        }



        #region AllClosed
        /*
        // GET: Foods/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var food = await _context.Foods
                .FirstOrDefaultAsync(m => m.Id == id);
            if (food == null)
            {
                return NotFound();
            }

            return View(food);
        }

        // GET: Foods/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Foods/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,ProteinValue,CarbohydrateValue,OilValue,FiberValue,FoodPhoto,EnumFoodType")] Food food)
        {
            if (ModelState.IsValid)
            {
                _context.Add(food);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(food);
        }

        // GET: Foods/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var food = await _context.Foods.FindAsync(id);
            if (food == null)
            {
                return NotFound();
            }
            return View(food);
        }

        // POST: Foods/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,ProteinValue,CarbohydrateValue,OilValue,FiberValue,FoodPhoto,EnumFoodType")] Food food)
        {
            if (id != food.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(food);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FoodExists(food.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(food);
        }

        // GET: Foods/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var food = await _context.Foods
                .FirstOrDefaultAsync(m => m.Id == id);
            if (food == null)
            {
                return NotFound();
            }

            return View(food);
        }

        // POST: Foods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var food = await _context.Foods.FindAsync(id);
            _context.Foods.Remove(food);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FoodExists(int id)
        {
            return _context.Foods.Any(e => e.Id == id);
        }
        */
        #endregion
    }
}