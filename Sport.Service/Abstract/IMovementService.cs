using Sport.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sport.Service.Abstract
{
    public interface IMovementService
    {
        Task<List<Movement>> GetAllMovementAsync();
        Task<int> AddMovementAsync(Movement movement);

        Task<int> EditMovementAsync(Movement movement);

        Task<int> DeleteMovementAsync(Movement movement);

        Task<Movement> MovementById(int Id);

    }
}
