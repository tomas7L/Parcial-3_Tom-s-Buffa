using ClasesTercerParcial.Data;
using ClasesTercerParcial.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClasesTercerParcial.Repository
{
    public class VentaRepository
    {
        public static List<Venta> ObtenerVentas()
        {
            using var context = new ApplicationDbContext();
            return context.Ventas.ToList();
        }
        public static bool AgregarVenta(Venta venta)
        {
            using var context = new ApplicationDbContext();
            context.Ventas.Add(venta);
            context.SaveChanges();
            return true;
        }
    }
}
