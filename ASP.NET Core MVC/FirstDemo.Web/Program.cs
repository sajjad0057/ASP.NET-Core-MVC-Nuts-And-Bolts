using Autofac;
using Autofac.Extensions.DependencyInjection;
using FirstDemo.Infrastructure;
using FirstDemo.Infrastructure.DbContexts;
using FirstDemo.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Sinks.MSSqlServer;
using Serilog.Events;
using System.Reflection;





try
{

    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
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


    #region Serilog (Logger) Configuration 

    builder.Host.UseSerilog((ctx, lc) => lc
        .MinimumLevel.Debug()
        .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
        .Enrich.FromLogContext()
        .ReadFrom.Configuration(builder.Configuration)
        );

    #endregion



    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(connectionString, m => m.MigrationsAssembly(assemblyName)));
    builder.Services.AddDatabaseDeveloperPageExceptionFilter();

    builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
        .AddEntityFrameworkStores<ApplicationDbContext>();
    builder.Services.AddControllersWithViews();

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


    app.MapControllerRoute(
          name: "areas",
          pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");


    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");


    app.MapRazorPages();

    app.Run();

}
catch(Exception ex)
{
    Log.Fatal(ex, "Application start-up failed .");
}
finally
{
    Log.CloseAndFlush();
}
