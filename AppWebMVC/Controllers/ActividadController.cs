using Microsoft.AspNetCore.Mvc;

namespace AppWebMVC.Controllers
{
    public class ActividadController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
