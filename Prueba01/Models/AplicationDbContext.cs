using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Prueba01.Models
{
    public class AplicationDbContext : System.Data.Entity.DbContext
    {
        public AplicationDbContext() : base("ConexionDB")
        {
        }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
    }
}