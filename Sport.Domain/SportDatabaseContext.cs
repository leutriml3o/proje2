using Microsoft.EntityFrameworkCore;
using Sport.Domain.Entities;
using Sport.Domain.Entities.MMRelation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sport.Domain
{
    public class SportDatabaseContext:DbContext
    {
        public SportDatabaseContext(DbContextOptions<SportDatabaseContext> options) : base(options)
        {
        }

        public virtual DbSet<Area> Areas { get; set; }
        public virtual DbSet<Food> Foods { get; set; }
        public virtual DbSet<Movement> Movements { get; set; }
        public virtual DbSet<NutritionDay> NutritionDays { get; set; }
        public virtual DbSet<NutritionList> NutritionLists { get; set; }
        public virtual DbSet<SportDay> SportDays { get; set; }
        public virtual DbSet<SportList> SportLists { get; set; }
        public virtual DbSet<ThatDay> ThatDays { get; set; }
        public virtual DbSet<AreaMovements> AreaMovements { get; set; }
        public virtual DbSet<MealFoods> MealFoods { get; set; }
        public virtual DbSet<UserSportLists> UserSportLists { get; set; }
        public virtual DbSet<UserNutritionLists> UserNutritionLists { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                modelBuilder.Entity(entity.Name).ToTable(entity.Name);
            }
            modelBuilder.Entity<AreaMovements>().HasKey(sc => sc.Id);

            modelBuilder.Entity<AreaMovements>()
                .HasOne(pc => pc.Area)
                .WithMany(p => p.AreaMovements)
                .HasForeignKey(pc => pc.FKAreaId)
                .IsRequired().OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AreaMovements>()
               .HasOne(pc => pc.Movement)
               .WithMany(p => p.AreaMovements)
               .HasForeignKey(pc => pc.FKMovementId)
               .IsRequired().OnDelete(DeleteBehavior.Restrict);

            //====================================================================================

            modelBuilder.Entity<UserNutritionLists>().HasKey(sc => sc.Id);

            //modelBuilder.Entity<UserNutritionLists>()
            //    .HasOne(y => y.User)
            //    .WithMany(x => x.NutritionLists)
            //    .HasForeignKey(pc => pc.FKUserId)
            //    .IsRequired().OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserNutritionLists>()
             .HasOne(y => y.NutritionList)
             .WithMany(x => x.UserNutritionLists)
             .HasForeignKey(pc => pc.FKNutritionListId)
             .IsRequired().OnDelete(DeleteBehavior.Restrict);

            //====================================================================================

            modelBuilder.Entity<UserSportLists>().HasKey(sc => sc.Id);

            //modelBuilder.Entity<UserSportLists>()
            //    .HasOne(y => y.User)
            //    .WithMany(x => x.UserSportLists)
            //    .HasForeignKey(pc => pc.FKUserId)
            //    .IsRequired().OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserSportLists>()
             .HasOne(y => y.SportList)
             .WithMany(x => x.UserSportLists)
             .HasForeignKey(pc => pc.FKSportListId)
             .IsRequired().OnDelete(DeleteBehavior.Restrict);

            //====================================================================================
            modelBuilder.Entity<MealFoods>().HasKey(sc => sc.Id);

            modelBuilder.Entity<MealFoods>()
                .HasOne(y => y.Food)
                .WithMany(x => x.MealsIncluded)
                .HasForeignKey(pc => pc.FKFoodId)
                .IsRequired().OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MealFoods>()
             .HasOne(y => y.ThatDay)
             .WithMany(x => x.NutrientsInMeals)
             .HasForeignKey(pc => pc.FKThatDayId)
             .IsRequired().OnDelete(DeleteBehavior.Restrict); 
        }
    }
}
