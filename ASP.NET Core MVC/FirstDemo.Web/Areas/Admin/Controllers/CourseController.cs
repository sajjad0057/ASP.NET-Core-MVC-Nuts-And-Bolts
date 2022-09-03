using FirstDemo.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;
using Autofac;

namespace FirstDemo.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CourseController : Controller
    {
        
        private readonly  ILifetimeScope _scope;        //// Autofac Container . 

        private readonly ILogger<CourseController> _logger;

        public CourseController(ILogger<CourseController> logger, ILifetimeScope scope )
        {
            _scope = scope;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            CourseCreateModel model = _scope.Resolve<CourseCreateModel>();   //// here create automatically CourseCreateModel object, with his parameter using autofac container (_scope);
            return View(model);
        }


        //// For receiving POST data -- 
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CourseCreateModel model)   //// Here created CourseCreateModel instance from modelBinder .
        {
            if (ModelState.IsValid)     //// Here , ModelState.IsValid property comming from Controller parent class
            {
                model.ResolveDependency(_scope);
                await model.CreateCourse();
            }
            return View();
        }
    }
}
