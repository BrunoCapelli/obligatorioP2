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
                    HttpContext.Session.SetString("rol", userEncontrado.VerificarRol());
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

        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignUp(string email, string password, string nombre, string apellido, DateTime fechaNac, string documento, TipoDocumento tipoDoc)
        {
            if(!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password) && !string.IsNullOrEmpty(nombre) && !string.IsNullOrEmpty(apellido) && !string.IsNullOrEmpty(documento))
            {
                AdminHostel.GetInstancia.AltaHuesped(email, password, nombre, apellido, tipoDoc, documento, fechaNac, "NULL", 1);
                ViewBag.msgExito = "Se registró correctamente";
                return View();

            }else 
            {
                ViewBag.msgError = "Los campos no pueden estas vacios";
                return View(); 
            }
        }
    }
}
