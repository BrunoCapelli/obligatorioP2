using LogicaDeNegocio;
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

        [HttpGet]
        public IActionResult BuscarAgendaPorHuesped()
        {
            return  View();
        }

        [HttpPost]
        public IActionResult BuscarAgendaPorHuesped(string docHuesped, TipoDocumento tipoDocumento)
        {
            UsuarioHuesped userH = AdminHostel.GetInstancia.BuscarHuesped(docHuesped, tipoDocumento);
            @ViewBag.Email = userH.Email;
            return View("ListarAgendas");
        }

        public IActionResult ListarAgendas()
        {
            return View();
        }
        public IActionResult ListarAgendasPendientes()
        {
            return View();
        }
        public IActionResult ConfirmarAgenda(string nomAc, DateTime fecAc)
        {          
            
               Agenda ag = AdminHostel.GetInstancia.BuscarAgenda(nomAc, fecAc);
               ag.EstadoAgenda = EstadoAgenda.CONFIRMADA;
            
            return RedirectToAction("ListarAgendasPendientes");
        }
    }
}
