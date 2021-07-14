using System;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sport.Domain;
using Sport.Domain.Entities;
using Sport.Repository.Abstract;
using Sport.Repository.Abstract.MMinterfaces;
using Sport.Repository.Concrete.EntityFrameworkCore;
using Sport.Repository.Concrete.EntityFrameworkCore.MMclass;
using Sport.Service.Abstract;
using Sport.Service.Abstract.MMRelation;
using Sport.Service.Concrete.EntityFrameworkCore;
using Sport.Service.Concrete.EntityFrameworkCore.MMclass;
using Sport.WebUI.Models;
using Sport.WebUI.TwoFactorService;

namespace Sport.WebUI
{
    public class Startup
    {

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = Configuration = new ConfigurationBuilder()
                        .SetBasePath(env.ContentRootPath)
                        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                        .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                        .AddEnvironmentVariables()
                        .Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddDbContext<SportDatabaseContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddDbContext<AppIdentityDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IFoodRepository, EfFoodRepository>();
            services.AddTransient<IFoodService, EfFoodService>();
            services.AddScoped<INutritionListRepository, EfNutritionListRepository>();
            services.AddTransient<INutritionListService, EfNutritionListService>();
            services.AddScoped<IAreaRepository, EfAreaRepository>();
            services.AddTransient<IAreaService, EfAreaService>();
            services.AddScoped<IMovementRepository, EfMovementRepository>();
            services.AddTransient<IMovementService, EfMovementService>();
            services.AddScoped<INutritionDayRepository, EfNutritionDayRepository>();
            services.AddTransient<INutritionDayService, EfNutritionDayService>();
            services.AddScoped<ISportDayRepository, EfSportDayRepository>();
            services.AddScoped<ISportDayService, EfSportDayService>();
            services.AddScoped<ISportListRepository, EfSportListRepository>();
            services.AddTransient<ISportListService, EfSportListService>();
            services.AddScoped<IThatDayRepository, EfThatDayRepository>();
            services.AddTransient<IThatDayService, EfThatDayService>();
            services.AddScoped<IUserRepository, EfUserRepository>();
            services.AddTransient<IUserService, EfUserService>();
            services.AddScoped<IAreaMovementsRepository, EfAreaMovementsRepository>();
            services.AddTransient<IAreaMovementsService, EfAreaMovementsService>();
            services.AddScoped<IMealFoodsRepository, EfMealFoodsRepository>();
            services.AddTransient<IMealFoodsService, EfMealFoodsService>();
            services.AddScoped<IUserNutritionListsRepository, EfUserNutritionListsRepository>();
            services.AddTransient<IUserNutritionListsService, EfUserNutritionListsService>();
            services.AddScoped<IUserSportListsRepository, EfUserSportListsRepository>();
            services.AddTransient<IUserSportListsService, EfUserSportListsService>();
            services.AddScoped<SessionHelper>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            
            services.Configure<TwoFactorOptions>(Configuration.GetSection("TwoFactorOptions"));

            //services.Configure<EmailSettings>(Configuration.GetSection("EmailSettings"));
            services.AddIdentity<AppUser, AppRole>()
                .AddEntityFrameworkStores<AppIdentityDbContext>()
                .AddDefaultTokenProviders();


   

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false; //sayý olsun mu
                options.Password.RequireLowercase = false; //
                options.Password.RequiredLength = 6; // minimum uzunluk 6 olsun mu
                options.Password.RequireNonAlphanumeric = false; //alfa numarik karakter olsun mu
                options.Password.RequireUppercase = false; // büyük harf olsun mu

                options.Lockout.MaxFailedAccessAttempts = 5; // kullanýcý 5 kez þifreyi yanlýþ girdiðinde hata versin
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);// 5 kez yanlýþ girince 5 dk sonra tekrar girebilsin
                options.Lockout.AllowedForNewUsers = true;

                //options.User.RequireUniqueEmail = true;
                //options.SignIn.RequireConfirmedEmail = true;
                //options.SignIn.RequireConfirmedPhoneNumber = false;
                //options.User.RequireUniqueEmail = true;
            });

            services.ConfigureApplicationCookie(option =>
            {
                option.LoginPath = "/Security/Login";
                option.LogoutPath = "/Security/Logout";
                option.AccessDeniedPath = "/Security/AccessDenied";
                option.SlidingExpiration = true;

            });

            JwtSecurityTokenHandler.DefaultMapInboundClaims = false;

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = "Cookies";
                options.DefaultChallengeScheme = "oidc";
            })
                .AddCookie("Cookies", options =>
                {
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                    options.Cookie.Name = "sport";
                    //options.EventsType = typeof(CookieEventHandler);
                }
                );
            services.AddControllersWithViews();
            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Calculator}/{id?}");
            });
        }
    }
}
