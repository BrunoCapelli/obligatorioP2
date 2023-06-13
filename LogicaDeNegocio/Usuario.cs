using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LogicaDeNegocio
{
    public class Usuario: IValidate
    {
        private string _email;
        private string _password;

        #region Propiedades
        public string Email
            {
                get { return _email; }
                set { _email = value; }
            }
        public string Password
            {
                get { return _password; }
                set { _password = value; }
            }
        #endregion

        #region Constructor
        public Usuario(string Email, string Password)
        {
            //Usuario user = new Usuario(Email, Password);
            _email = Email;
            _password = Password;
        }
        #endregion

        #region Metodos
        public void Validate()
        {
            try
            {
                // Validar que el arroba este en el medio
                
                if (!Regex.IsMatch(_email, "[a-zA-Z]" + "@" + "[a-zA-Z]"))
                {
                    throw new Exception("El email ingresado no es valido");
                }
                
                // Validar largo de la contraseña
                if (_password.Length < 8)
                {
                    throw new Exception("La contraseña debe ser mayor a 8 caracteres.");
                }
            }catch
            {
                throw;
            }
        }
        public virtual string VerificarRol()
        {
            return "Operador";
        }
        #endregion
    }


}
