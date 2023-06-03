using Microsoft.AspNetCore.Mvc;

namespace AppWebMVC.Controllers
{
    public class UsuarioController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
