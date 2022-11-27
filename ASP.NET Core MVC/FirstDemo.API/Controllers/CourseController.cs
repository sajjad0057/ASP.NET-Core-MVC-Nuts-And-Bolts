using Autofac;
using Microsoft.AspNetCore.Mvc;

namespace FirstDemo.API.Controllers
{
    [ApiController]
    [Route("api/v3/[controller]")]
    public class CourseController : Controller
    {
        private readonly ILifetimeScope _scope;

        private readonly ILogger<CourseController> _logger;

        public CourseController(ILogger<CourseController> logger,ILifetimeScope scope)
        {
            _logger = logger;
            _scope = scope;
        }


        public IActionResult Index()
        {
            return View();
        }
    }
}
