using FirstDemo.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FirstDemo.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private static ICourseModel _courseModel;


        //// **** Should not be used Model in Dependency Injection , although here we used Model Instance for create a Dependency Injection Examples **** 
        public HomeController(ILogger<HomeController> logger,ICourseModel courseModel)
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

        }

        public IActionResult Index(int id)
        {
            _logger.LogInformation("I am in index page .");     //// here .LogInformation , .LogWarning and Like others are called log levels . 
            ViewData["id"] = id;
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