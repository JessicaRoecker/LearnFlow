using Microsoft.AspNetCore.Mvc;

namespace LearFlow.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
