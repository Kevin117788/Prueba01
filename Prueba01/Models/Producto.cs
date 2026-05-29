using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prueba01.Models
{
    [Table("Producto")]
    public class Producto
    {
        public string ID { get; set; }
        public string Nombre { get; set; }
        public int IdCategoria { get; set; }
        [ForeignKey("IdCategoria")]
        public virtual Categoria Categoria { get; set; }
        public string Descripcion { get; set; }
        public string Precio { get; set; }
    }
}
