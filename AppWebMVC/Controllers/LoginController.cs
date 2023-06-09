﻿using LogicaDeNegocio;
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
            if(HttpContext.Session.GetString("email")== null) {
                return View();

            }else {
                return RedirectToAction("Index", "Home");
                //esto es para que cuando esten logueados si quieren entrar a la pantalla de registrarse no los deje
            }
        }

        [HttpPost]
        public IActionResult SignUp(string email, string password, string nombre, string apellido, DateTime fechaNac, string documento, TipoDocumento tipoDoc)
        {
            try
            {
                if(!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password) && !string.IsNullOrEmpty(nombre) && !string.IsNullOrEmpty(apellido) && !string.IsNullOrEmpty(documento))
                {
                    if(AdminHostel.GetInstancia.BuscarHuesped(documento, tipoDoc) == null)
                    {
                        AdminHostel.GetInstancia.AltaHuesped(email, password, nombre, apellido, tipoDoc, documento, fechaNac, "NULL", 1);
                        ViewBag.msgExito = "Se registró correctamente";

                    }
                    else
                    {
                        ViewBag.msgError = "El documento ingresado ya existe";
                    }
                    return View();

                }else 
                {
                    ViewBag.msgError = "Los campos no pueden estas vacios";
                    return View(); 
                }

            }catch(Exception ex)
            {
                ViewBag.MsgError = ex.Message;
                return View();
            }
        }
    }
}
