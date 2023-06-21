using LogicaDeNegocio;
using Microsoft.AspNetCore.Mvc;

namespace AppWebMVC.Controllers
{
    public class UsuarioController : Controller
    {
        

        public IActionResult DatosPersonales()
        {
            string email = HttpContext.Session.GetString("email");
            if (email != null) {
                Usuario user = AdminHostel.GetInstancia.BuscarPorEmail(email);
                ViewBag.Usuario = user;
                ViewBag.userRol = user.VerificarRol();
                return View();
            } else {
                return RedirectToAction("index","Login");
                // si no esta logeado lo mando a la pantalla de login
            }
        }

       
    }
}
