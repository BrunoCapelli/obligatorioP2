﻿using LogicaDeNegocio;
using Microsoft.AspNetCore.Mvc;

namespace AppWebMVC.Controllers
{
    public class ActividadController : Controller
    {
        public IActionResult Index(DateTime fecha) /* esto va a ser ver todas las actividades*/
        {
            ViewBag.Fecha = fecha;
            if (!(fecha > DateTime.MinValue)) {
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
                            ViewBag.Email = uh.Email;
                            return View("ListarAgendas");
                            //redirecciono a mostrar todas las agendas
                        } catch (Exception e) {
                            ViewBag.mensaje = e.Message;
                            ViewBag.Fecha = act.Fecha;
                            return View("Index");
                        }
                    } else {
                        ViewBag.mensaje = "El usuario no es de tipo huesped "; // no deberia tirar este mensaje nunca pero la profe dijo que valide
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
        public IActionResult BuscarAgendaPorHuesped()
        {
            if (HttpContext.Session.GetString("rol") == "Operador")
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public IActionResult BuscarAgendaPorHuesped(string docHuesped, TipoDocumento tipoDocumento)
        {
            if(docHuesped != null)
            {
                UsuarioHuesped userH = AdminHostel.GetInstancia.BuscarHuesped(docHuesped, tipoDocumento);
                ViewBag.Email = userH.Email;
                return View("ListarAgendas");

            }
            else
            {
                ViewBag.Error = "Debe ingresar un nro de documento";
                return View("ListarAgendas");
            }
        }

        public IActionResult ListarAgendas()
        {
            string email = HttpContext.Session.GetString("email");
            if (email != null) {

                if (HttpContext.Session.GetString("rol") == "Huesped")
                {
                    ViewBag.Email = email;

                }
                return View();
            } else {
                return RedirectToAction("Index", "Login");
            }
        }
        public IActionResult ListarAgendasPendientes()
        {
           
            if (HttpContext.Session.GetString("rol") == "Operador") {
                return View();
            }
            else {
                return RedirectToAction("Index", "Home");
            }
        }
        public IActionResult ConfirmarAgenda(string nomAc, DateTime fecAc)
        {          
            if(nomAc!= null && fecAc > DateTime.MinValue) {

               Agenda ag = AdminHostel.GetInstancia.BuscarAgenda(nomAc, fecAc);
               ag.EstadoAgenda = EstadoAgenda.CONFIRMADA;
               return RedirectToAction("ListarAgendasPendientes");
            } else {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
