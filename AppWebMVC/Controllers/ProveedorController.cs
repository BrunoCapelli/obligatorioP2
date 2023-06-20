using LogicaDeNegocio;
using Microsoft.AspNetCore.Mvc;

namespace AppWebMVC.Controllers
{
    public class ProveedorController : Controller
    {
        public IActionResult ListarProveedores()
        {
            if(HttpContext.Session.GetString("rol") == "Operador") {
                return View();
            }
            else {
                return RedirectToAction("Index","Home");
            }
        }
        public IActionResult EstablecerPromocion(string nombreProveedor)
        {
            if (HttpContext.Session.GetString("rol") == "Operador") {
                ViewBag.nombreProv = nombreProveedor;
                return View();
            }
            else {
                return RedirectToAction("Index", "Home");
            }
            
        }
        [HttpPost]
        public IActionResult EstablecerPromocion(string nombreProveedor,int valorPromocion)
        {
            if(nombreProveedor == null)
            {
                AdminHostel.GetInstancia.BuscarProveedor(nombreProveedor).DescuentoFijo = valorPromocion;

            }
            else
            {
                ViewBag.Error = "No se encontro el proveedor";
            }
            return RedirectToAction("ListarProveedores");
        }
    }
}
