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
       
        public static List<Producto> listaProductos = new List<Producto>
        {
            new Producto { ID = "001", Nombre = "Laptop", Categoria = "Computadoras", Descripcion = "16 GB RAM, Ryzen 07, 512 GB SSD", Precio = "10000" },
            new Producto { ID = "002", Nombre = "Mouse", Categoria = "Accesorios", Descripcion = "Inalambrico, ergonomico", Precio = "500" },
            new Producto { ID = "003", Nombre = "Teclado", Categoria = "Accesorios", Descripcion = "Mecanico, RGB", Precio = "1200" },
            new Producto { ID = "004", Nombre = "Monitor", Categoria = "Pantallas", Descripcion = "27 pulgadas, 144Hz", Precio = "5000" },
            new Producto { ID = "005", Nombre = "Audifonos", Categoria = "Audio", Descripcion = "Inalambricos, cancelacion de ruido", Precio = "2000" }
        };

        public ActionResult ListaP()
        {
            ViewBag.Categorias = new List<string> { "Computadoras", "Accesorios", "Pantallas", "Audio", "Almacenamiento", "Componentes" };
            return View(listaProductos);
        }

        [HttpPost]
        public ActionResult Agregar(string ID, string Nombre, string Categoria, string Descripcion, string Precio)
        {
          
            var nuevoProducto = new Producto
            {
                ID = ID,
                Nombre = Nombre,
                Categoria = Categoria,
                Descripcion = Descripcion,
                Precio = Precio
            };

            listaProductos.Add(nuevoProducto);

            return RedirectToAction("ListaP");
        }

        [HttpPost]
        public ActionResult Eliminar(string id)
        {
            var producto = listaProductos.FirstOrDefault(p => p.ID == id);
            if (producto != null)
            {
                listaProductos.Remove(producto);
            }

            return RedirectToAction("ListaP");
        }
    }
}