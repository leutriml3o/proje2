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
    public class EfMovementService:IMovementService
    {
        private readonly IMovementRepository _movementRepo;
        public EfMovementService(IMovementRepository movementRepo)
        {
            _movementRepo = movementRepo;
        }

        public async Task<int> AddMovementAsync(Movement movement)
        {
            return await _movementRepo.Add(movement);
        }


        public async Task<int> DeleteMovementAsync(Movement movement)
        {
            return await _movementRepo.Delete(movement);
        }

        public async Task<int> EditMovementAsync(Movement movement)
        {
            return await _movementRepo.Edit(movement);
        }

        public async Task<Movement> MovementById(int Id)
        {
            Movement getMovement = await _movementRepo.Get(p => p.Id == Id);
            return getMovement;
        }

        public async Task<List<Movement>> GetAllMovementAsync()
        {
            IEnumerable<Movement> list = await _movementRepo.GetAll();
            List<Movement> movements = list.ToList();
            return movements;
        }
    }
}
