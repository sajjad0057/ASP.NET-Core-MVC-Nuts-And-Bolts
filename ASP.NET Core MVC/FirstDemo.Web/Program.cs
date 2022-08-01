using Autofac;
using Autofac.Extensions.DependencyInjection;
using FirstDemo.Web;
using FirstDemo.Web.Data;
using FirstDemo.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

#region Autofac Configuration

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());  //// by this here, added autofac as dependency injection framework with asp.net core app
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder => {
    containerBuilder.RegisterModule(new WebModule());             //// here , can load one / more module that need for binding . 
});

#endregion

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();


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




var app = builder.Build();

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
