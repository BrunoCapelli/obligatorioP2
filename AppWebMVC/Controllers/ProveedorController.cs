using Microsoft.AspNetCore.Mvc;

namespace AppWebMVC.Controllers
{
    public class ProveedorController : Controller
    {
        public IActionResult ListarProveedores()
        {
            return View();
        }
    }
}
