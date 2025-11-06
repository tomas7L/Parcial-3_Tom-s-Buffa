using ClasesTercerParcial.Data;
using ClasesTercerParcial.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClasesTercerParcial.Repository
{
    public class ClienteRepository
    {
        public static void AgregarCliente(Cliente cliente)
        {
            using var context = new ApplicationDbContext();
            context.Clientes.Add(cliente);

            context.SaveChanges();
        }
        public static List<Cliente> ObtenerEmpleados()
        {
            using var context = new ApplicationDbContext();
            return context.Clientes.ToList();
        }
        public static void EliminarEmpleado(Cliente cliente)
        {
            using var context = new ApplicationDbContext();
            context.Clientes.Remove(cliente);

            context.SaveChanges();
        }
        public static Cliente BuscarPorEmail(string email)
        {
            using var context = new ApplicationDbContext();
            return context.Clientes.FirstOrDefault(e => e.Email == email);

        }
    }
}
