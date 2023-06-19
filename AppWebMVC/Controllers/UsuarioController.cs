using LogicaDeNegocio;
using Microsoft.AspNetCore.Mvc;

namespace AppWebMVC.Controllers
{
    public class UsuarioController : Controller
    {
        

        public IActionResult DatosPersonales()
        {
            Usuario user = AdminHostel.GetInstancia.BuscarPorEmail(HttpContext.Session.GetString("email"));
            ViewBag.Usuario = user;
            ViewBag.userRol = user.VerificarRol();
            return View();
        }

       
    }
}
