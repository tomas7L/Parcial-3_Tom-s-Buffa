using ClasesTercerParcial.Data;
using ClasesTercerParcial.Models;
using ClasesTercerParcial.Repository;
using Microsoft.EntityFrameworkCore;


namespace FRONT
{
    internal class Program
    {
        public static List<Cliente> clientes = ClienteRepository.ObtenerEmpleados();
        public static List<Producto> productos = ProductoRepository.ObtenerProductos();
        public static List<Venta> ventas = VentaRepository.ObtenerVentas();
        static void Main(string[] args)
        {
            bool salir = false;

            while (!salir)
            {
                Console.Clear();
                Console.WriteLine("Menú:");
                Console.WriteLine("1- Registrar producto");
                Console.WriteLine("2- Registar cliente");
                Console.WriteLine("3- Registrar nueva venta");
                Console.WriteLine("4- Mostrar reportes de ventas");
                Console.WriteLine("5- Salir");

                string opcion = Console.ReadLine().Trim();

                switch (opcion)
                {
                    case "1":
                        Console.WriteLine("Ingrese el nombre del producto");
                        string nombre = Console.ReadLine().Trim();
                        while (true)
                        {
                            if (ProductoRepository.ExisteProducto(nombre))
                            {
                                Console.WriteLine("El producto ya existe. Ingrese otro nombre:");
                                nombre = Console.ReadLine().Trim();

                            }
                            else
                            {
                                break;
                            }
                        }
                        Console.WriteLine("Ingrese el precio del producto");
                        string precio = Console.ReadLine().Trim();
                        while (true)
                        {
                            if (!decimal.TryParse(precio, out decimal precioDecimal) || precioDecimal < 0)
                            {
                                Console.WriteLine("Precio inválido. Ingrese un precio válido:");
                                precio = Console.ReadLine().Trim();
                            }
                            else
                            {
                                break;
                            }
                        }
                        Console.WriteLine("Ingrese el stock del producto");
                        string stock = Console.ReadLine().Trim();
                        while (true)
                        {
                            if (!int.TryParse(stock, out int stockInt) || stockInt < 0)
                            {
                                Console.WriteLine("Stock inválido. Ingrese un stock válido:");
                                stock = Console.ReadLine().Trim();
                            }
                            else
                            {
                                break;
                            }
                        }
                        Producto producto = new Producto(nombre, decimal.Parse(precio), int.Parse(stock));
                        ProductoRepository.AgregarProducto(producto);
                        Console.ReadKey(true);
                        break;

                    case "2":
                        Console.WriteLine("Ingrese el nombre del cliente");
                        string nombreCliente = Console.ReadLine().Trim();
                        while (true)
                        {
                            if (string.IsNullOrWhiteSpace(nombreCliente))
                            {
                                Console.WriteLine("El nombre no puede estar vacío. Ingrese otro nombre:");
                                nombreCliente = Console.ReadLine().Trim();
                            }
                            else
                            {
                                break;
                            }
                        }
                        Console.WriteLine("Ingrese el email del cliente");
                        string emailCliente = Console.ReadLine().Trim();
                        while (true)
                        {
                            if (ClienteRepository.BuscarPorEmail(emailCliente) != null)
                            {
                                Console.WriteLine("El email ya está registrado. Ingrese otro email:");
                                emailCliente = Console.ReadLine().Trim();
                            }
                            else
                            {
                                break;
                            }
                        }
                        Cliente cliente = new Cliente(nombreCliente, emailCliente);
                        ClienteRepository.AgregarCliente(cliente);

                        Console.ReadKey(true);
                        break;

                    case "3":
                        Console.WriteLine("Ingrese el email del cliente:");
                        string emailVenta = Console.ReadLine().Trim();
                        while (true)
                        {
                            var clienteBuscado = ClienteRepository.BuscarPorEmail(emailVenta);
                            if (clienteBuscado == null)
                            {
                                Console.WriteLine("El email no está registrado. Ingrese otro email:");
                                emailVenta = Console.ReadLine().Trim();
                            }
                            else
                            {
                                cliente = clienteBuscado;
                                break;
                            }
                        }

                        List<Producto> productosVenta = new List<Producto>();

                        while (true)
                        {
                            Console.WriteLine("Ingrese el nombre del producto a vender (o 'fin' para terminar):");
                            string nombreProductoVenta = Console.ReadLine().Trim();

                            if (nombreProductoVenta.ToLower() == "fin")
                            {
                                break;
                            }

                            Producto productoVenta = ProductoRepository.BuscarPorNombre(nombreProductoVenta);

                            if (productoVenta == null)
                            {
                                Console.WriteLine("El producto no existe. Ingrese otro nombre:");
                            }
                            else
                            {
                                productosVenta.Add(productoVenta);
                            }
                        }

                        if (productosVenta.Count == 0)
                        {
                            Console.WriteLine("No se han agregado productos a la venta.");
                        }
                        else
                        {
                            Venta nuevaVenta = new Venta
                            {
                                Cliente = cliente,
                                Productos = productosVenta
                            };

                            VentaRepository.AgregarVenta(nuevaVenta);
                            Console.WriteLine($"Venta registrada exitosamente. Total: ${nuevaVenta.Total}");
                        }

                        Console.ReadKey(true);

                        Console.ReadKey(true);
                        break;

                    case "4":
                        Console.WriteLine("Ingrese el email del cliente para ver el reporte de ventas:");
                        string emailReporte = Console.ReadLine().Trim();

                        using (var context = new ApplicationDbContext())
                        {                          
                            var clienteReporte = context.Clientes.FirstOrDefault(c => c.Email == emailReporte);

                            if (clienteReporte == null)
                            {
                                Console.WriteLine("El cliente no existe.");
                            }
                            else
                            {                                
                                var ventasCliente = context.Ventas
                                    .Include(v => v.Productos)
                                    .Where(v => v.ClienteId == clienteReporte.Id)
                                    .ToList();

                                if (ventasCliente.Count == 0)
                                {
                                    Console.WriteLine("El cliente no tiene ventas registradas.");
                                }
                                else
                                {
                                    Console.WriteLine($"\nReporte de ventas para {clienteReporte.Nombre} ({clienteReporte.Email})");
                                    Console.WriteLine("------------------------------------------------");

                                    foreach (var venta in ventasCliente)
                                    {
                                        Console.WriteLine($"Fecha: {venta.Fecha.ToShortDateString()}");
                                        Console.WriteLine("Productos comprados:");

                                        foreach (var productos in venta.Productos)
                                        {
                                            Console.WriteLine($"   - {productos.Nombre} (${productos.Precio})");
                                        }

                                        Console.WriteLine($"Total de la venta: ${venta.Total}");
                                        Console.WriteLine("------------------------------------------------");
                                    }

                                    double totalGastado = ventasCliente.Sum(v => v.Total);
                                    Console.WriteLine($"\nTotal gastado por el cliente: ${totalGastado}");
                                }
                            }
                        }

                        Console.ReadKey(true);
                        break;



                        Console.ReadKey(true);
                        break;
                

                    case "5":
                        salir = true;
                        break;

                    default:
                        Console.WriteLine("Opción incorrecta");
                        Console.ReadKey(true);
                        break;
                }
            }
        }
    }
}

