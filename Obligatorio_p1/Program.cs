using LogicaDeNegocio;

namespace Obligatorio_p1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            AdminHostel adminHostel = new AdminHostel();

            

        }

        public static void MostrarActividades(AdminHostel admHostel)
        {
            admHostel.ListarActividades();
        }

        public static void MostrarProveedores(AdminHostel admHostel)
        {
            admHostel.ListarProveedores();
        }

        public static void AltaHuesped(AdminHostel admHostel)
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

            admHostel.AltaHuesped(usuarioAlta);
        }

        


    }
}