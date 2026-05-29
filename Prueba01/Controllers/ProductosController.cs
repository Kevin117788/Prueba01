using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Prueba01.Models;
using System.Data.SqlClient;
using System.Configuration;

namespace Prueba01.Controllers
{
    public class ProductosController : Controller
    {
        private AplicationDbContext db = new AplicationDbContext();
        ProductoDAL dal = new ProductoDAL();

        public ActionResult ListaP()
        {
            ViewBag.Categorias = new SelectList(db.Categorias.ToList(), "IdCategoria", "Nombre");
            var productosDb = db.Productos.Include("Categoria").ToList();
            return View(productosDb);
        }

        public ActionResult TablaProductos()
        {
            var productosDb = db.Productos.Include("Categoria").ToList();
            return View(productosDb);
        }

        [HttpPost]
        public ActionResult Agregar(Producto p)
        {
            if (ModelState.IsValid)
            {
                db.Productos.Add(p);
                db.SaveChanges();
            }
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