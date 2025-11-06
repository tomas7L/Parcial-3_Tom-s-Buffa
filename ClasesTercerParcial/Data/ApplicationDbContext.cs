using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClasesTercerParcial.Models;
using Microsoft.EntityFrameworkCore;
namespace ClasesTercerParcial.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Venta> Ventas { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Server=TOMµS;Database=Parcial3DB;Trusted_Connection=True;TrustServerCertificate=True;"
            );
        }
    }
}
