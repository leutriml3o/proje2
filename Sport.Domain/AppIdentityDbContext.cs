using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Sport.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sport.Domain
{
    public class AppIdentityDbContext  : IdentityDbContext<AppUser , AppRole,string>
    {
        public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options) : base(options)
        { 
        }
    }
}
