using Sport.Domain;
using Sport.Domain.Entities;
using Sport.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sport.Repository.Concrete.EntityFrameworkCore
{
    public class EfNutritionDayRepository:EfGenericRepository<NutritionDay>,INutritionDayRepository
    {
        public EfNutritionDayRepository(SportDatabaseContext context) : base(context)
        {
        }
        public SportDatabaseContext DatabaseContext
        {
            get { return _context as SportDatabaseContext; }
        }
    }
}

