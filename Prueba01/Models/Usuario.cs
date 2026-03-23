using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Prueba01.Models
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string UsuarioNombre { get; set; }
        public string Correo { get; set; }
        public string Contraseña { get; set; }
        public int TipoUsuario { get; set; }
        public bool Estado { get; set; }
    }
}