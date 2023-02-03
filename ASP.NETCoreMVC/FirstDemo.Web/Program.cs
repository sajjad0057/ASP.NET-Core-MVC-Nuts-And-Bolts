using Autofac;
using Autofac.Extensions.DependencyInjection;
using FirstDemo.Infrastructure;
using FirstDemo.Infrastructure.BusinessObjects;
using FirstDemo.Infrastructure.DbContexts;
using FirstDemo.Infrastructure.Entities;
using FirstDemo.Infrastructure.Securities;
using FirstDemo.Infrastructure.Services;
using FirstDemo.Web;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Serilog.Events;
using System.Reflection;
using System.Text;



var builder = WebApplication.CreateBuilder(args);

#region Serilog (Logger) Configuration 

builder.Host.UseSerilog((ctx, lc) => lc
    .MinimumLevel.Debug()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .ReadFrom.Configuration(builder.Configuration)
    );

#endregion


try
{

    //// Add services to the container.
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    var assemblyName = Assembly.GetExecutingAssembly().FullName;

    #region Autofac Configuration

    builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());  //// by this here, added autofac as dependency injection framework with asp.net core app
    builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
    {
        //// here , can load one / more module that need for binding .
        containerBuilder.RegisterModule(new WebModule());  //// it's for Web project dependency binding

        containerBuilder.RegisterModule(new InfrastructureModule(connectionString, assemblyName)); //// it's for Infrastructure project dependency binding
    });

    #endregion


    #region Dependency Injection Config with default ServiceCollections 

    //// For dependency Injection Configuration 
    ////builder.Services.[ServiceCollectionServiceExtensionsMethodName]<ModelName>(); //// Create Instance just using Class .

    //// using AddTransient<>  create a new instance every time , when need to inject a dependency 
    //builder.Services.AddTransient<ICourseModel, CourseModel>();  //// Create Instance just using Interface . 


    ////using AddSingleton<> always passed a same intance , when need to inject a dependency  
    //builder.Services.AddSingleton<ICourseModel, CourseModel>();


    ////here, can define which instance are used where
    //builder.Services.AddScoped<ICourseModel, CourseModel>();

    #endregion


    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(connectionString, m => m.MigrationsAssembly(assemblyName)));
    builder.Services.AddDatabaseDeveloperPageExceptionFilter();

    #region Config AutoMapper
    builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    #endregion



    #region ForDefaultIdentityManagement
    ////builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    ////    .AddEntityFrameworkStores<ApplicationDbContext>();
    #endregion




    #region ForCustomizeIdentityManagement

    builder.Services
        .AddIdentity<ApplicationUser, ApplicationRole>()
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddUserManager<ApplicationUserManager>()
        .AddRoleManager<ApplicationRoleManager>()
        .AddSignInManager<ApplicationSignInManager>()
        .AddDefaultTokenProviders();



    builder.Services
        .AddAuthentication()
        .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
        {
            options.LoginPath = new PathString("/Account/Login");
            options.AccessDeniedPath = new PathString("/Account/Login");
            options.LogoutPath = new PathString("/Account/Logout");
            options.Cookie.Name = "FirstDemoPortal.Identity";
            options.SlidingExpiration = true;
            options.ExpireTimeSpan = TimeSpan.FromHours(1);
        })
        //// For Configuring JWT Token - 
        .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, x =>
        {
            x.RequireHttpsMetadata = false;
            x.SaveToken = true;
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"])),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidIssuer = builder.Configuration["Jwt:Issuer"],
                ValidAudience = builder.Configuration["Jwt:Audience"],
            };
        }); 


    builder.Services
        .Configure<IdentityOptions>(options =>
        {
            // Password settings.
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequiredLength = 6;
            options.Password.RequiredUniqueChars = 0;

            // Lockout settings.
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.AllowedForNewUsers = true;

            // User settings.
            options.User.AllowedUserNameCharacters =
            "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
            options.User.RequireUniqueEmail = true;
        });


    #endregion


    #region Policy based and Claim based Authorization


    builder.Services.AddAuthorization(options =>
    {
        options.AddPolicy("CourseManagementPolicy", policy =>
        {
            policy.RequireAuthenticatedUser();
            policy.RequireRole("Admin");
            policy.RequireRole("Teacher");
        });

        options.AddPolicy("CourseViewPolicy", policy =>
        {
            policy.RequireAuthenticatedUser();
            policy.RequireClaim("ViewCourse", "true");
        });

        options.AddPolicy("CourseCreatePolicy", policy =>
        {
            policy.RequireAuthenticatedUser();
            policy.RequireClaim("CreateCourse", "true");
        });

        options.AddPolicy("CourseViewRequirementPolicy", policy =>
        {
            policy.RequireAuthenticatedUser();
            policy.Requirements.Add(new CourseViewRequirement());
        });

        //// For authorized by JWT Token when make request to API Controller in Web Project - 
        options.AddPolicy("ApiRequirementPolicy", policy =>
        {
            policy.AuthenticationSchemes.Clear();
            policy.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);   //// By this declare this policy authenticated by JWT not by default.
            policy.RequireAuthenticatedUser();
            policy.Requirements.Add(new ApiRequirement());
        });


    });

    //// For Configuring Cookie bassed session 
    builder.Services.AddSession(options =>
    {
        options.IdleTimeout = TimeSpan.FromMinutes(30);
        options.Cookie.HttpOnly = true;
        options.Cookie.IsEssential = true;
    });


    builder.Services.AddSingleton<IAuthorizationHandler, CourseViewRequirementHandler>();
    builder.Services.AddSingleton<IAuthorizationHandler, ApiRequirementHandler>();

    #endregion


    ////Load data from appsettings into a objects -
    builder.Services.Configure<Smtp>(builder.Configuration.GetSection("Smtp"));

    builder.Services.AddControllersWithViews();

    #region ForRunningDockerImages

    //builder.WebHost.UseUrls("http://*:80");

    #endregion

    var app = builder.Build();


    // Here Log static class being accessible after initially app Build successfully .

    Log.Information("Application Starting .");

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseMigrationsEndPoint();
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

    //// for configuring Session -
    app.UseSession();


    app.MapControllerRoute(
          name: "areas",
          pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");


    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");



    app.Run();

}
catch (Exception ex)
{
    Log.Fatal(ex, "Application start-up failed .");
}
finally
{
    Log.CloseAndFlush();
}