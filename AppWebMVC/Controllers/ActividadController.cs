using Microsoft.AspNetCore.Mvc;

namespace AppWebMVC.Controllers
{
    public class ActividadController : Controller
    {
        public IActionResult Index(DateTime fecha) /* esto va a ser ver todas las actividades*/
        {
            ViewBag.Fecha = fecha;
            if (!(fecha > DateTime.MinValue)) {
                ViewBag.Mensaje = "Error de fecha";
                ViewBag.Fecha = DateTime.Now;
            }
            

            return View();
        }

        public IActionResult Agendar() {
            return View();
        }
    }
}
