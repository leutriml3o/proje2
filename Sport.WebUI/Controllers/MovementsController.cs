using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Sport.Domain.Entities;
using Sport.Service.Abstract;

namespace Sport.WebUI.Controllers
{
    [Authorize()]
    public class MovementsController : Controller
    {
        private readonly IMovementService _movementService;
        private readonly IConfiguration _configration;


        public MovementsController(IMovementService movementService,IConfiguration configuration)
        {
            _movementService = movementService;
            _configration = configuration;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _movementService.GetAllMovementAsync());
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View(new Movement());
        }
        [HttpPost]
        public async Task<IActionResult> Create(Movement movement)
        {
            int success = await _movementService.AddMovementAsync(movement);

            if (success < 0)
            {
                return NotFound();
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(Movement movement)
        {
            int success = await _movementService.DeleteMovementAsync(movement);
            if (success < 0)
            {
                return NotFound();
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int Id)
        {
            Movement movement = await _movementService.MovementById(Id);
            return View(movement);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Movement movement)
        {
            int success = await _movementService.EditMovementAsync(movement);
            if (success < 0)
            {
                return NotFound();
            }
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> Details(int Id)
        {
            Movement movement = await _movementService.MovementById(Id);
            return View(movement);
        }
    }
}