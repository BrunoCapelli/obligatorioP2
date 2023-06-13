using LogicaDeNegocio;
using Microsoft.AspNetCore.Mvc;

namespace AppWebMVC.Controllers
{
    public class UsuarioController : Controller
    {
        public IActionResult ListarAgendas()
        {
            
            return View();
        }

        public IActionResult DatosPersonales()
        {
            ViewBag.Usuario = AdminHostel.GetInstancia.BuscarPorEmail(HttpContext.Session.GetString("email"));
            return View();
        }
    }
}
