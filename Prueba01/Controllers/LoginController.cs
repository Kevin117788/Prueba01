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
        public ActionResult Login(string usuario, string password)
        {
            var user = dal.ValidarUsuario(usuario, password);

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
        public ActionResult Registro(Usuario model, string password, string password_repeat)
        {
            // Asignamos la contraseña recibida al modelo directamente
            model.Contraseña = password;

            if (string.IsNullOrEmpty(password) || password != password_repeat)
            {
                ViewBag.Error = "Las contraseñas no coinciden o están vacías.";
                return View(model);
            }

            // Ignoramos el error de validación base de Contraseña porque ahora lo llenamos manualmente
            ModelState.Remove("Contraseña");

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Asignar un TipoUsuario por defecto si no viene en el formulario (por ej. 2 = Usuario normal)
            if (model.TipoUsuario == 0)
            {
                model.TipoUsuario = 2; // O el valor que corresponda en tu base de datos
            }

            try
            {
                dal.RegistrarUsuario(model);
                return RedirectToAction("Login");
            }
            catch (System.Data.SqlClient.SqlException sqlEx) when (sqlEx.Number == 2627 || sqlEx.Number == 2601)
            {
                // Códigos 2627 y 2601 son errores de violación de restricción UNIQUE o Primary Key en SQL Server
                ViewBag.Error = "El usuario o correo electrónico ya está registrado.";
                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error al registrar: " + ex.Message;
                return View(model);
            }
        }

        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();

            return RedirectToAction("Login","Login");

        }
    }
}