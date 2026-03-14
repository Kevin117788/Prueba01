using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Prueba01.Models;

namespace Prueba01.Controllers
{
    public class CarritoController : Controller
    {
        
        public static List<Producto> listaCarrito = new List<Producto>();

        [HttpGet]
        public ActionResult ListaCarrito()
        {
            ViewBag.Total = listaCarrito.Sum(p => decimal.TryParse(p.Precio, out decimal precio) ? precio : 0);
            return View(listaCarrito);
        }

        [HttpPost]
        public ActionResult Agregar(string id)
        {
            
            var producto = ProductosController.listaProductos.FirstOrDefault(p => p.ID == id);
            if (producto != null)
            {
                listaCarrito.Add(producto);
            }
            return RedirectToAction("ListaCarrito");
        }

        [HttpPost]
        public ActionResult Eliminar(string id)
        {
            var producto = listaCarrito.FirstOrDefault(p => p.ID == id);
            if (producto != null)
            {
                listaCarrito.Remove(producto);
            }
            return RedirectToAction("ListaCarrito");
        }
    }
}