using Autofac;
using FirstDemo.Infrastructure.Exceptions;
using FirstDemo.Web.Areas.Admin.Models;
using FirstDemo.Web.Codes;
using FirstDemo.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FirstDemo.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class CourseController : Controller
    {

        private readonly ILifetimeScope _scope;        //// Autofac Container . 

        private readonly ILogger<CourseController> _logger;

        public CourseController(ILogger<CourseController> logger, ILifetimeScope scope)
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

                try
                {
                    await model.CreateCourse();

                    TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
                    {
                        Message = "Successfully added a new course .",
                        Type = ResponseTypes.Success
                    });

                    return RedirectToAction("Index");
                }
                catch (DuplicateException ioe)
                {
                    _logger.LogError(ioe, ioe.Message);

                    ////for showing duplicateException Message in Client Side Validation Message Showing Section.
                    ModelState.AddModelError("", ioe.Message);


                    TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
                    {
                        Message = ioe.Message,
                        Type = ResponseTypes.Warning
                    });
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, ex.Message);
                    TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
                    {
                        Message = "There Was a Problem in Creating Course .",
                        Type = ResponseTypes.Danger
                    });
                }

            }
            else
            {

                ////// For showing validation message from server side for invalid ModelState , and Messages -   
                string messageText = string.Empty;
                foreach (var message in ModelState.Values)
                {
                    for (int i = 0; i < message.Errors.Count(); i++)
                    {
                        messageText += $"{message.Errors[i].ErrorMessage}";


                    }

                }
                TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
                {
                    Message = messageText,
                    Type = ResponseTypes.Warning
                });
            }

            return View(model);
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

                    TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
                    {
                        Message = "Successfully Updated course ! ",
                        Type = ResponseTypes.Success
                    });

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, ex.Message);
                    TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
                    {
                        Message = "There Was a Problem in Updating Course .",
                        Type = ResponseTypes.Danger
                    });

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

                TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
                {
                    Message = "Successfully Deleted this course ! ",
                    Type = ResponseTypes.Success
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
                {
                    Message = "There Was a Problem in Deleting Course .",
                    Type = ResponseTypes.Danger
                });
            }

            return RedirectToAction("Index");
        }



    }
}
