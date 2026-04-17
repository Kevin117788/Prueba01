using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Prueba01.Models;

namespace Prueba01.Controllers
{
    public class ProductosController : Controller
    {
        ProductoDAL dal = new ProductoDAL();

        public ActionResult ListaP()
        {
            ViewBag.Categorias = new List<string> { "Computadoras", "Accesorios", "Pantallas", "Audio", "Almacenamiento", "Componentes" };
            var productosDb = dal.ObtenerProductos();
            return View(productosDb);
        }

        [HttpPost]
        public ActionResult Agregar(string Nombre, string Categoria, string Descripcion, string Precio)
        {
            var nuevoProducto = new Producto
            {
                Nombre = Nombre,
                Categoria = Categoria,
                Descripcion = Descripcion,
                Precio = Precio
            };

            dal.AgregarProducto(nuevoProducto);

            return RedirectToAction("ListaP");
        }

        [HttpPost]
        public ActionResult Eliminar(int id)
        {
            dal.EliminarProducto(id);
            return RedirectToAction("ListaP");
        }
    }
}