using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FirstDemo.Web.Controllers
{
    [Route("api/v3/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet, Authorize(Policy = "ApiRequirementPolicy")]
        public IEnumerable<string> Get()
        {
            try
            {
                return new string[] { "dhaka", "sylhet", "khulna" };
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
