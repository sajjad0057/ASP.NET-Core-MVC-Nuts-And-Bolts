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
            /* here create automatically CourseCreateModel object, with his parameter using autofac container (_scope);
            We Resolved Manually CourseCreateModel using ILifetimeScope  Coz , CourseCreateModel class have a Parameterized Constructor ,
            this parameter constructor - parameter does not resolved autofac automatically using Binding Module , Coz we dose not set 
            CourseCreateModel class as dependency in any Controller class Constructor parameters. autofac auto resolved just Controller
            Constructor dependency. If we set a Class as dependency in at least one controller constructor then autofac resolved this 
            automatic , else we can resolved manually class dependency using auto container (ILifetimeScope) . Example given below :-
            here CourseCreateModel does not as dependency in any Controller Constructor , so we resolved it Parameterized Constructor
            manually .  
             */
            CourseCreateModel model = _scope.Resolve<CourseCreateModel>();
            return View(model);
        }



        /* Here created CourseCreateModel instance from modelBinder . ModelBinder creating CourseCreateModel instance using 
           Empty constructor of CourseCreateModel class . but here need CourseCreateModel having ICourseService dependency -
           For this we resolved this issue using Autofac container ILifetimeScope .  
        */
        //// For receiving POST data --
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CourseCreateModel model)
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
