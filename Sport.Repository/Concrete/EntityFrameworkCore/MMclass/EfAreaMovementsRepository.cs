using Sport.Domain;
using Sport.Domain.Entities.MMRelation;
using Sport.Repository.Abstract.MMinterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sport.Repository.Concrete.EntityFrameworkCore.MMclass
{
    public class EfAreaMovementsRepository:EfGenericRepository<AreaMovements>,IAreaMovementsRepository
    {
        public EfAreaMovementsRepository(SportDatabaseContext context) : base(context)
        {
        }
        public SportDatabaseContext DatabaseContext
        {
            get { return _context as SportDatabaseContext; }
        }
    }
}

