using FirstDemo.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;
using Autofac;
using FirstDemo.Web.Models;
using FirstDemo.Web.Codes;

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

        public JsonResult GetCourseData()
        {
            var dataTableModel = new DataTablesAjaxRequestModel(Request);  //// Here Request - object is Controller class property.
            var model = _scope.Resolve<CourseListModel>();
            return Json(model.GetPagedCourses(dataTableModel));
        }



        public IActionResult Edit(Guid id)
        {
            CourseEditModel model = _scope.Resolve<CourseEditModel>();
            model.LoadData(id);
            return View(model);
        }


        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Edit(CourseEditModel model)
        {
            if (ModelState.IsValid)
            {
                model.ResolveDependency(_scope);
                try
                {
                    model.EditCourse();
                    TempData["ResponseMessage"] = "Successfuly updated course.";
                    TempData["ResponseType"] = ResponseTypes.Success;

                    return RedirectToAction("Index");
                }
                catch(Exception ex)
                {
                    _logger.LogError(ex, ex.Message);
                    TempData["ResponseMessage"] = "There was a problem in updating course.";
                    TempData["ResponseType"] = ResponseTypes.Danger;

                }
            }

            return View(model);
        }



        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Delete(Guid id)
        {
            try
            {
                var model = _scope.Resolve<CourseListModel>();
                model.DeleteCourse(id);

                TempData["ResponseMessage"] = "Successfuly deleted course .";  //// TempData is a temporary dataStructure in Razor pages
                TempData["ResponseType"] = ResponseTypes.Success; 
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                TempData["ResponseMessage"] = "There Was a problem in deleting courses !";
                TempData["ResponseType"] = ResponseTypes.Danger;
            }

            return RedirectToAction("Index");
        }



    }
}
