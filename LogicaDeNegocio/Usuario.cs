using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeNegocio
{
    internal class Usuario
    {
        private string _email;
        private string _password;
    }

    internal class UsuarioHuesped: Usuario 
    {
        private TipoDocumento _tipoDoc;
        private string _nroDocumento;
        private string _nombre;
        private string _apellido;
        private string _habitacion;
        private DateTime _fechaNacimiento;
        private int _nivel;
    }
}
