using LogicaDeNegocio;
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
                Usuario userEncontrado = AdminHostel.GetInstancia.BuscarPorEmail(user);
                if(userEncontrado != null && userEncontrado.Email == user && userEncontrado.Password == password) 
                {
                    HttpContext.Session.SetString("email", userEncontrado.Email);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.DatosErroneos = "Los datos son incorrectos";
                    return View();
                }


            }
            else
            {
                ViewBag.DatosErroneos = "Debe completar los campos";
                return View();
            }
        }
        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Login");
        }
    }
}
