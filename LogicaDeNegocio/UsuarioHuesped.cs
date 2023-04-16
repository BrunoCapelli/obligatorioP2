using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicaDeNegocio;


namespace LogicaDeNegocio 
{
    public class UsuarioHuesped : Usuario {
        private TipoDocumento _tipoDoc;
        private string _nroDocumento;
        private string _nombre;
        private string _apellido;
        private string _habitacion;
        private DateTime _fechaNacimiento;
        private int _nivel;

        private string _email;
        private string _password;

    #region Propiedades
        public TipoDocumento TipoDoc{
            get { return _tipoDoc; } 
            set {  _tipoDoc = value; }
        }

        public string NroDocumento
        {
            get { return _nroDocumento; }
            set { _nombre = value; }
        }

        public string Nombre
        {
            get { return _nombre;  }
            set { _nombre = value; }
        }

        public string Apellido
        {
            get { return _apellido; }
            set { _apellido = value; }
        }

        public string Habitacion
        {
            get { return _habitacion; }
            set {  _habitacion = value;}
        }
        public DateTime FechaNacimiento
        {
            get { return _fechaNacimiento; }
            set { _fechaNacimiento = value; }
        }

        public int NivelFidelizacion
        {
            get { return _nivel; }
            set{_nivel = value;}   
        }
        #endregion

        #region Constructor
        public UsuarioHuesped(string Email, string Password,  string Nombre, string Apellido, TipoDocumento tipoDoc, string NroDocumento, DateTime FechaNacimiento, string Habitacion,int Nivel): base(Email, Password)
        {
            _email = Email;
            _password = Password;
            _nombre = Nombre;
            _apellido= Apellido;            
            _tipoDoc = tipoDoc;
            _nroDocumento = NroDocumento;
            _fechaNacimiento = FechaNacimiento;
            _habitacion= Habitacion;
            _nivel = Nivel;
        }
        #endregion

        #region Metodos

        public void ValidateHuesped()
        {
            try
            {
                // Validar que el campo Habitacion no sea vacio
                if(_habitacion.Length < 0)
                {
                    throw new Exception("El campo habitacion no puede estar vacío.");
                }
                // Validar que el Usuario no existe
                /*if ()
                {
                    
                }*/
            }
        }


        #endregion
    }
}
