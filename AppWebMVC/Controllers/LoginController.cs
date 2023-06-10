using Microsoft.AspNetCore.Mvc;

namespace AppWebMVC.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string user, string password)
        {

            if(!String.IsNullOrEmpty(user) && !String.IsNullOrEmpty(password))
            {
                
                return RedirectToAction("Index", "Home");

            }
            else
            {
                return View();
            }
        }
    }
}
