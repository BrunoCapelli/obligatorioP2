using LogicaDeNegocio;

namespace Obligatorio_p1
{
    internal class Program
    {
        private static AdminHostel adminHostel = new AdminHostel();
        static void Main(string[] args)
        {
            int opcion= 1;
            while(opcion != 0)
            {

                MostrarMenu();
                opcion = SeleccionarOpcion();
            }




        }

        public static void MostrarActividades()
        {
            Console.WriteLine(adminHostel.ListarActividades());
        }

        public static void MostrarProveedores()
        {
            Console.WriteLine( adminHostel.ListarProveedores());
        }

        public static void AltaHuesped()
        {
            try
            {

                Console.WriteLine("--- ALTA DE USUARIO ---\n");

                Console.WriteLine("Ingrese un nombre: \n");
                string nombre = Console.ReadLine();
            
                Console.WriteLine("Ingrese un apellido: \n");
                string apellido = Console.ReadLine();
            
                Console.WriteLine("Ingrese un email: \n");
                string email = Console.ReadLine();

                Console.WriteLine("Ingrese un password: \n");
                string password = Console.ReadLine();

                Console.WriteLine("Ingrese nro de documento: \n");
                string nroDocumento = Console.ReadLine();

                Console.WriteLine("Ingrese un tipo de Documento (CI, DNI, OTRO): \n");
                string documento = Console.ReadLine();
                TipoDocumento tipoDoc = (TipoDocumento)Enum.Parse(typeof(TipoDocumento), documento);
                
                Console.WriteLine("Ingrese fecha de nacimiento: ");
                string fechaNacimiento = Console.ReadLine();
                DateTime.TryParse(fechaNacimiento, out DateTime fechaNac);
            
                Console.WriteLine("Ingrese habitacion: ");
                string habitacion = Console.ReadLine();

                Console.WriteLine("Ingrese nivel de fidelizacion: ");
                Int32.TryParse(Console.ReadLine(), out int nivelFidelizacion);

                //UsuarioHuesped usuarioAlta = new UsuarioHuesped(email, password, nombre, apellido, tipoDoc, nroDocumento, fechaNac, habitacion, nivelFidelizacion);

                adminHostel.AltaHuesped(email, password, nombre, apellido, tipoDoc, nroDocumento, fechaNac, habitacion, nivelFidelizacion);
                
            }catch(Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        public static void EstablecerDescuentoAUnProveedor()
        {
            try
            {
                Console.WriteLine("Ingrese el nombre del proveedor: ");
                string nombreProveedor = Console.ReadLine();

                Console.WriteLine("Ingrese el valor del descuento: ");
                Int32.TryParse(Console.ReadLine(), out int descuento);

                adminHostel.EstablecerDescuento(nombreProveedor, descuento);
            }catch(Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }


        public static void ListarActivFiltradas()
        {
            Console.WriteLine("Ingrese un costo en dolares: ");
            Decimal.TryParse(Console.ReadLine(), out decimal costo);

            try
            {
                Console.WriteLine("Ingrese fecha inicial: ");
                string fechaInicialInput = Console.ReadLine();

                DateTime fechaFiltradaDesde = new DateTime();
                DateTime.TryParse(fechaInicialInput, out fechaFiltradaDesde);


                Console.WriteLine("Ingrese fecha final: ");

                string fechaHastaInput = Console.ReadLine();

                DateTime fechaFiltradaHasta = new DateTime();
                DateTime.TryParse(fechaHastaInput, out fechaFiltradaHasta);

                Console.WriteLine(adminHostel.ListarActividadesFiltradas(costo, fechaFiltradaDesde, fechaFiltradaHasta));
            }
            catch
            {
                throw new Exception("Los datos de la fecha ingresada no son validos");
            }

        }
        public static void MostrarMenu()
        {
            Console.WriteLine("\n --- Bienvenido a su hostel --- \n" +
                "Elija una de las opciones:\n" +
                "1 - Mostrar listado de actividades\n" +
                "2 - Mostar listado de proveedores\n" +
                "3 - Mostrar actividades por fecha y costo\n" +
                "4 - Establecer el valor de promoción para actividades de un proveedor\n" +
                "5 - Realizar alta de un huesped\n" +
                "0 - Cerrar aplicacion");

        }
        
        public static int SeleccionarOpcion()
        {
            Int32.TryParse(Console.ReadLine(), out int opcion);

            switch(opcion)
            {
                case 0:
                    opcion = 0; 
                    break;
                case 1:
                    MostrarActividades();
                    break;
                case 2:
                    MostrarProveedores();
                    break;
                case 3:
                    ListarActivFiltradas();
                    break;
                case 4:
                    EstablecerDescuentoAUnProveedor();
                    break;
                case 5:
                    AltaHuesped();
                    break;

            }
            return opcion;
        }

        
    }
}