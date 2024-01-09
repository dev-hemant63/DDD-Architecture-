using Microsoft.AspNetCore.Mvc;

namespace WEBAPI.Controllers
{
    public class HomeController : ControllerBase
    {
        public IActionResult Index()
        {
            return Redirect("api-docs");
        }
    }
}
