using Prueba01.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Prueba01.Controllers
{
    public class LoginController : Controller
    {
        UsuarioDAL dal = new UsuarioDAL();

        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string usuario, string contraseña)
        {
            var user = dal.ValidarUsuario(usuario, contraseña);

            if (user != null)
            {
                Session["Usuario"] = user.UsuarioNombre;
                return RedirectToAction("Login", "Home");
            }

            ViewBag.Error = "Usuario o contraseña incorrectos.";
            return View();
        }
    }
}