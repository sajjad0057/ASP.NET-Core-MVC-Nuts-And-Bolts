using FirstDemo.Infrastructure.Services;
using FirstDemo.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FirstDemo.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private static ICourseModel _courseModel;
        

        private readonly IDataUtility _dataUtility;
        private readonly ITimeService _timeService;


        //// **** Should not be used Model in Dependency Injection , although here we used Model Instance for create a Dependency Injection Examples **** 
        public HomeController(ILogger<HomeController> logger,ICourseModel courseModel,
            IDataUtility dataUtility, ITimeService timeService)
        {
            _logger = logger;

            if (_courseModel != null)
            {
                if (courseModel == _courseModel)
                {
                    int x = 354;
                }
            }
            else
                _courseModel = courseModel;

            _dataUtility = dataUtility;
            _timeService = timeService;

        }

        public async Task<IActionResult> Index(int id)
        {
            _logger.LogInformation("I am in index page .");     //// here .LogInformation , .LogWarning and Like others are called log levels . 
            ViewData["id"] = id;


            ////string sql = $"insert into Courses (Id,Title,Fees,ClassStartDate) values('{Guid.NewGuid()}','ADO.NET',2000,'{_timeService.Now.AddDays(30).ToString()}')";
            string sql = "delete from courses where Title = 'ADO.NET' ";
            //// Test for Ado.Net - 
            await _dataUtility.InsertDataAsync(sql);

            return View();
        }

        public IActionResult Privacy()
        {
            _logger.LogInformation("I am in privacy page .");   //// here .LogInformation , .LogWarning and Like others are called log levels . 
            return View();
        }

        public IActionResult Test()
        {
            var model = new TestModel();

            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}