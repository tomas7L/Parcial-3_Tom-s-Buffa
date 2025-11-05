namespace FRONT
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool salir = false;

            while (!salir)
            {
                Console.Clear();
                Console.WriteLine("Menú:");
                Console.WriteLine("1- Registrar libro");
                Console.WriteLine("2- Registrar préstamo");
                Console.WriteLine("3- Mostrar información de un libro");
                Console.WriteLine("4- Mostrar todos los libros y sus estadísticas");
                Console.WriteLine("5- Salir");

                string opcion = Console.ReadLine().Trim();

                switch (opcion)
                {
                    case "1":
                        // Registrar libro
                        Console.ReadKey(true);
                        break;

                    case "2":
                        // Registrar préstamo
                        Console.ReadKey(true);
                        break;

                    case "3":
                        // Mostrar información de un libro
                        Console.ReadKey(true);
                        break;

                    case "4":
                        // Mostrar todos los libros y sus estadísticas
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

