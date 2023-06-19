using LogicaDeNegocio;
using Microsoft.AspNetCore.Mvc;

namespace AppWebMVC.Controllers
{
    public class UsuarioController : Controller
    {
        

        public IActionResult DatosPersonales()
        {
            ViewBag.Usuario = AdminHostel.GetInstancia.BuscarPorEmail(HttpContext.Session.GetString("email"));
            return View();
        }

       
    }
}
