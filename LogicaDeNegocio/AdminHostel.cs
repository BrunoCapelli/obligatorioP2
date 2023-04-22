using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeNegocio
{
    public class AdminHostel
    {
        private List<Usuario> _usuarios = new List<Usuario>();
        private List<Actividad> _actividades = new List<Actividad>();
        private List<Proveedor> _proveedores = new List<Proveedor>();

        #region Metodos
        // Validar que no exista un usuario con los datos ingresados
        public bool ExisteUsuarioHuesped(string nroDocumentoHuesped, TipoDocumento tipoDocumentoHuesped)
        {
            bool existe = false;

            // Hacerlo en UsuarioHuesped

            foreach (UsuarioHuesped user in _usuarios)
            {
                // Verifico que no exista otro huesped con este numero y tipo de documento
                if (user.NroDocumento == nroDocumentoHuesped && user.TipoDoc == tipoDocumentoHuesped)
                    {
                        existe = true;
                    }
                }
            return existe;
        }

        public List<Actividad> ListaActividades()
        {
            return _actividades;
        }

        public List<Usuario> ListaUsuarios()
        {
            return _usuarios;
        }
        public void ListarUsuarios()
        {
            foreach (UsuarioHuesped user in _usuarios)
            {
                string datosUsuario;
               // datosUsuario = user.Name.ToString;
            }
        }
        public List<Proveedor> ListaProveedor()
        {
            return _proveedores;
        }
        public void AltaHuesped(UsuarioHuesped newUser)
        {
            try
            {
                newUser.ValidateHuesped();
                UsuarioHuesped altaUsuario = new UsuarioHuesped(newUser.Email, newUser.Password, newUser.Nombre, newUser.Apellido, newUser.TipoDoc, newUser.NroDocumento, newUser.FechaNacimiento, newUser.Habitacion, newUser.NivelFidelizacion);
                _usuarios.Add(altaUsuario);

            }catch(Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        #endregion

    }
}
