using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicaDeNegocio;

namespace LogicaDeNegocio
{
    internal class UsuarioOperador: Usuario, IValidate
    { 
        private DateTime _fechaAlta;

        public DateTime FechaAlta
        {
            get { return _fechaAlta; }
            set { _fechaAlta = value;}
        }

        public UsuarioOperador(string Email, string Password, string Nombre, string Apellido, DateTime fechaAlta): base(Email, Password, Nombre, Apellido)
        {
            _fechaAlta = fechaAlta;
        }

        public override string VerificarRol() 
        {
            return "Operador";
        }
    }
}
