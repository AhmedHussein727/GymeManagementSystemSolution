using GymeManagementBLL;
using GymeManagementBLL.Services.Classes;
using GymeManagementBLL.Services.Interfaces;
using GymeManagementDAL.Data.Contexts;
using GymeManagementDAL.Data.DataSeed;
using GymeManagementDAL.Repositories.Classes;
using GymeManagementDAL.Repositories.InterFaces;
using Microsoft.EntityFrameworkCore;

using GymeManagementBLL.Services.AttachmentService;
using Microsoft.AspNetCore.Identity;
using GymeManagementDAL.Entities;
namespace GymeManagementPL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<GymeDbContext>(options => {

                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));

            });
            //builder.Services.AddScoped(typeof(IGenericRepository<>),typeof(GenericRepository<>));
            //builder.Services.AddScoped<IPlanRepository, PlanRepository>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<ISessionRepository, SessionRepository>();
            builder.Services.AddAutoMapper(x => x.AddProfile(new MappingProfiles()));
            builder.Services.AddScoped<IAnalytictsService, AnalyticService>();
            builder.Services.AddScoped<IMemberService, MemberService>();
            builder.Services.AddScoped<ITrainerService,TrainerService>();
            builder.Services.AddScoped<IPlanService, PlanService>();
            builder.Services.AddScoped<ISessionService, SessionService>();
            builder.Services.AddScoped<IAttachmentService, AttachmentService>();
            builder.Services.AddScoped<IAccountService, AccountService>();
            builder.Services.AddScoped<IMembershipsService,MembershipService>();
            builder.Services.AddScoped<IMembershipRepository, MembershipRepo>();
            builder.Services.AddScoped<IMemberSessionSevice,MemberSessionSevice>();
            builder.Services.AddScoped<IMemberRepository,MemberRepository>();
            builder.Services.AddScoped<IMemberSessionsRepository,MemberSessionsRepository>();
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(config =>
            {
                config.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<GymeDbContext>();

            builder.Services.ConfigureApplicationCookie(opt =>
            {
                opt.LoginPath = "/Account/Login";
                opt.AccessDeniedPath = "/Account/AccessDenied";
            });

            var app = builder.Build();

            #region Migrate Database - Seeding Data

            using var scope = app.Services.CreateScope();
            var dbContext=scope.ServiceProvider.GetRequiredService<GymeDbContext>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager=scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var PendingMigration=dbContext.Database.GetPendingMigrations();
            if(PendingMigration?.Any() ?? false )
                dbContext.Database.Migrate();
            GymDbContextSeeding.SeedData(dbContext);
            IdentityDbContextSeeding.SeedData(roleManager, userManager);

            #endregion

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Account}/{action=Login}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
