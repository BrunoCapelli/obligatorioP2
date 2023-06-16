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

        [HttpPost]
        public IActionResult Agendar(int id) {
            if (HttpContext.Session.Get("email") != null) {
                string email = HttpContext.Session.GetString("email");
                AdminHostel adm = AdminHostel.GetInstancia;
                Actividad act = adm.BuscarActividad(id);
                if (act != null) {
                    UsuarioHuesped uh = adm.BuscarHuespedPorEmail(email);
                    if (uh != null) {
                        try {
                            adm.AltaAgenda(uh.NroDocumento, uh.TipoDoc, act.Nombre, act.Fecha);
                            RedirectToAction("ListarAgendas","Usuario");
                            //redirecciono a mostrar todas las agendas
                        } catch (Exception e) {
                            ViewBag.mensaje = e.Message;
                            //aca veo a donde voy
                        }
                    } else {
                        ViewBag.mensaje = "El usuario no existe "; // no deberia tirar este mensaje nunca pero la profe dijo que valide
                    }
                } else {
                    ViewBag.mensaje = "La actividad no existe "; // no deberia tirar este mensaje nunca pero la profe dijo que valide
                }

                ViewBag.Fecha = act.Fecha;
                return View("Index");
            }
            else { 
                return RedirectToAction("Index","Login"); 
            }
            
        }

        [HttpGet]

        public IActionResult AdmnistrarAgendas() { 
        //aca el usuario huesped va a confirmar las agendas no confirmadas para cada actividad
            return View(); 
        }


    }
}
