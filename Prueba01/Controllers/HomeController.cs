using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Prueba01.Models;

namespace Prueba01.Controllers
{
    public class HomeController : Controller
    {
        // Lista estática para almacenar alumnos
        private static List<Alumno> listaAlumnos = new List<Alumno>
        {
            new Alumno { Matricula = "2321001", Nombre = "Juan", ApellidoPaterno = "Ornelas", ApellidoMaterno = "Vadez", Carrera = "Licenciatura en Sistemas Computacionales" },
            new Alumno { Matricula = "2321002", Nombre = "María", ApellidoPaterno = "Payan", ApellidoMaterno = "Leyva", Carrera = "Licenciatura en Sistemas Computacionales" },
            new Alumno { Matricula = "2321003", Nombre = "Luis", ApellidoPaterno = "Vazquez", ApellidoMaterno = "Vadez", Carrera = "Licenciatura en Sistemas Computacionales" },
            new Alumno { Matricula = "2321004", Nombre = "Kevin", ApellidoPaterno = "Vadez", ApellidoMaterno = "Daniel", Carrera = "Licenciatura en Sistemas Computacionales" }
        };

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "me gusta leer, musica y viedeojuegos.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "informacion de contacto";

            return View();
        }
        public ActionResult Prueba()
        {
            ViewBag.Message = "Mensaje de prueba";
            return View();
        }
        public ActionResult Lista()
        {
            ViewBag.Message = "Lista de Alumnos";
            return View();
        }

        [HttpPost]
        public ActionResult Agregar(string Matricula, string Nombre, string ApellidoPaterno, string ApellidoMaterno, string Carrera)
        {
            // Crear nuevo alumno y agregarlo a la lista
            var nuevoAlumno = new Alumno
            {
                Matricula = Matricula,
                Nombre = Nombre,
                ApellidoPaterno = ApellidoPaterno,
                ApellidoMaterno = ApellidoMaterno,
                Carrera = Carrera
            };

            listaAlumnos.Add(nuevoAlumno);

            return RedirectToAction("ListaAlumnos");
        }

        public ActionResult Dinamica()
        {
            List<string> alumnos = new List<string>()
            {
                    "Ornelas Vadez",
                    "Payan Leyva",
                    "Vazquez Vadez",
                    "Vadez"
            };
            return View(alumnos);
        }

        public ActionResult Bienvenido(string nombre)
        {
            ViewBag.Nombre = nombre;
            return View();
        }

        public ActionResult ListaAlumnos()
        {
            ViewBag.Carrera = "Licenciatura en Sistemas Computacionales";
            ViewBag.Materia = "Aplicaciones Web Avanzadas";
            ViewBag.Docente = "FABIOLA CRISTINA BEYROUTY ZAMMAR";

            return View(listaAlumnos);
        }

        public ActionResult Login()
        {
            ViewBag.Message = "Iniciar Sesin";
            return View();
        }

        [HttpPost]
        public ActionResult Login(string usuario, string password)
        {
            return RedirectToAction("Index");
        }
    }
}