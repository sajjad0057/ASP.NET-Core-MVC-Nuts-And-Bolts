using FirstDemo.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;

namespace FirstDemo.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CourseController : Controller
    {
        
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            CourseCreateModel model = new CourseCreateModel();
            return View(model);
        }


        //// For receiving POST data -- 
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CourseCreateModel model)   ////Here created CourseCreateModel instance from model binding,that's done through Dependency Injection Config.
        {
            if (ModelState.IsValid)     //// Here , ModelState.IsValid property comming from Controller parent class
            {
                await model.CreateCourse();
            }
            return View();
        }
    }
}
