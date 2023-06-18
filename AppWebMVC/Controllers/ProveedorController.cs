using LogicaDeNegocio;
using Microsoft.AspNetCore.Mvc;

namespace AppWebMVC.Controllers
{
    public class ProveedorController : Controller
    {
        public IActionResult ListarProveedores()
        {
            return View();
        }
        public IActionResult EstablecerPromocion(string nombreProveedor)
        {
            ViewBag.nombreProv= nombreProveedor;
            return View();
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
