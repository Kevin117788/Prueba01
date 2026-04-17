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
        [HttpGet]
        public ActionResult Login()
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
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Message = "Usuario o contraseña incorrectos.";
            return View();
        }

        // Registro
        [HttpGet]
        public ActionResult Registro()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registro(Usuario user, string RepetirContraseña)
        {
            if (user.Contraseña != RepetirContraseña)
            {
                ViewBag.Error = "Las contraseñas no coinciden.";
                return View();
            }

            // TODO: Agregar lógica correspondiente al DAL para guardar en BD
            // dal.RegistrarUsuario(user);

            return RedirectToAction("Login");
        }

        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();

            return RedirectToAction("Login","Login");

        }
    }
}