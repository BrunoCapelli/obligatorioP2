using LogicaDeNegocio;

namespace Obligatorio_p1
{
    internal class Program
    {
        private static AdminHostel adminHostel = new AdminHostel();
        static void Main(string[] args)
        {
            

            

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
            Console.WriteLine("ALTA DE USUARIO");
            Console.WriteLine("Ingrese un nombre: ");
            string nombre = Console.ReadLine();
            
            Console.WriteLine("Ingrese un apellido: ");
            string apellido = Console.ReadLine();
            
            Console.WriteLine("Ingrese un email: ");
            string email = Console.ReadLine();

            Console.WriteLine("Ingrese un password: ");
            string password = Console.ReadLine();

            Console.WriteLine("Ingrese nro de documento: ");
            string nroDocumento = Console.ReadLine();

            Console.WriteLine("Ingrese un tipo de Documento (CI, DNI, OTRO): ");
            string documento = Console.ReadLine();
            TipoDocumento tipoDoc = (TipoDocumento)Enum.Parse(typeof(TipoDocumento), documento);

            Console.WriteLine("Ingrese fecha de nacimiento: ");
            string fechaNacimiento = Console.ReadLine();
            DateTime fechaNac = (DateTime)Enum.Parse(typeof(DateTime), fechaNacimiento);

            Console.WriteLine("Ingrse habitacion: ");
            string habitacion = Console.ReadLine();

            Console.WriteLine("Ingrese nivel de fidelizaion: ");
            Int32.TryParse(Console.ReadLine(), out int nivelFidelizacion);

            UsuarioHuesped usuarioAlta = new UsuarioHuesped(email, password, nombre, apellido, tipoDoc, nroDocumento, fechaNac, habitacion, nivelFidelizacion);

            try
            {

                adminHostel.AltaHuesped(usuarioAlta);
            }catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public void EstablecerDescuentoAUnProveedor(string nombreProveedor, int descuento)
        {
            try
            {

                adminHostel.EstablecerDescuento(nombreProveedor, descuento);
            }catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public void MostrarMenu()
        {
            Console.WriteLine(" Bienvenido a su hostel \n" +
                "");
        }

        public void ListarActividadesFiltradas(decimal costo, DateTime fechaDesde, DateTime fechaHasta)
        {
            Console.WriteLine(adminHostel.ListarActividadesFiltradas(costo, fechaDesde, fechaHasta));
        }
        


    }
}