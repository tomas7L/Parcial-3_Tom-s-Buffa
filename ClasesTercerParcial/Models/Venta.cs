using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClasesTercerParcial.Models
{
    public class Venta
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;
        public double Total { get; set; }
        public List<Producto> Productos { get; set; } = new List<Producto>();
    }

}
