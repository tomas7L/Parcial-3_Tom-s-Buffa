using ClasesTercerParcial.Data;
using ClasesTercerParcial.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ClasesTercerParcial.Repository
{
    public class ProductoRepository
    {
        
        public static bool ExisteProducto(string nombre)
        {
            using (var context = new Data.ApplicationDbContext())
            {
                return context.Productos.Any(p => p.Nombre.ToLower() == nombre.ToLower());
            }
        }
        public static bool AgregarProducto(Producto producto)
        {
            using var context = new ApplicationDbContext();
            context.Productos.Add(producto);
            context.SaveChanges();
            return true;
        }
        public static List<Producto> ObtenerProductos()
        {
            using var context = new ApplicationDbContext();
            return context.Productos.ToList();
        }
        public static Producto BuscarPorNombre(string nombre)
        {
            using var context = new ApplicationDbContext();
            return context.Productos.FirstOrDefault(p => p.Nombre.ToLower() == nombre.ToLower());
        }
    }
}
